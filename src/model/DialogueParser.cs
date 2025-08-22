namespace DialogueSystem;

using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

// A parsing tool to translate human written script to data.
class DialogueParser
{
    private static readonly DialogueParser instance = new DialogueParser();

    const string GO_TO_FLAG = ">> ";
    const string END_NODE_FLAG = "---";
    const string LANGUAGE_PATTERN = @"^<.+>$";
    const string SCENE_NAME_FLAG = "# ";
    const string BRANCH_NAME_FLAG = "## ";
    const string CHOICE_FLAG = "- ";
    const string SPEAKER_PATTERN = @"^.+(?<!\\):"; // any test from start to : and not \?
    const string ALL_WHITE_SPACE = @"^\s*$"; // white space from start to end
    const string ANY_TEXT = ".*";

    public enum LineType
    {
        SceneName, BranchName, LanguageName,
        TextLine, ChoiceLine, SpeakerLine, GoToLine,
        EndNode,
        Blank, EndOfFile, Error
    }
    private readonly List<Fixup> _fixups = new();

    private record Fixup(DialogueEdge Edge, LineType DestinationType, string DestinationName);

    private string _language = "No Language Set";

    private string? _currLine;

    // public DialogueNode currentNode { get; set; }
    // public DialogueScene currentScene { get; set; }
    // public DialogueBranch currentBranch { get; set; }

    private StreamReader? _streamReader;
    public static DialogueParser Instance() => instance;


    // reads file from path and creates a new scene
    public DialogueScene ReadFile(string path)
    {
        ;
        DialogueScene scene;
        _streamReader = new StreamReader(path);
        _currLine = _streamReader.ReadLine();



        if (GetLineType(_currLine) == LineType.LanguageName)
        {
            // strip the surrounding < >
            var line = _currLine!.Trim();
            _language = line.Substring(1, line.Length - 2).Trim();
            _currLine = _streamReader.ReadLine();
        }


        if (LineType.SceneName == GetLineType(_currLine))
        {
            scene = ParseSceneName(_currLine);
            _currLine = _streamReader.ReadLine();

        }
        else
            scene = new DialogueScene("default");

        scene = ReadScene(scene);

        FixEdges(scene);
        _streamReader = null;


        return scene;
    }


    private DialogueScene ReadScene(DialogueScene scene)
    {

        if (GetLineType(_currLine) != LineType.BranchName && _currLine != null)
        {
            var main = ReadBranch(scene, BRANCH_NAME_FLAG + "main");
            scene.AddBranch(main);
        }

        // Read named branches until end.
        while (_currLine != null)
        {
            if (GetLineType(_currLine) != LineType.BranchName)
            {
                // skip stray blanks etc.
                _currLine = _streamReader!.ReadLine();
                continue;
            }

            var branch = ReadBranch(scene);
            scene.AddBranch(branch);
        }

        return scene;
    }


    // Requires: current line != null
    // Returns a branch with all nodes associated with it, 
    // leaves line at start of next branch or null
    private DialogueBranch ReadBranch(DialogueScene scene)
    {
        string branchName = _currLine;
        _currLine = _streamReader.ReadLine();
        return ReadBranch(scene, branchName);
    }

    private DialogueBranch ReadBranch(DialogueScene scene, string line)
    {
        int nodeIndex = 0;
        DialogueBranch branch = ParseBranchName(line);

        scene.AddBranch(branch);
        // read nodes
        DialogueNode? prevNode = null;

        while (_currLine != null)
        {
            var type = GetLineType(_currLine);
            if (type == LineType.BranchName)
                break; // next branch begins

            // Skip pure blank lines between nodes
            if (type == LineType.Blank)
            {
                _currLine = _streamReader!.ReadLine();
                continue;
            }


            DialogueNode node = ParseNode(scene, branch, nodeIndex);
            branch.AddNode(node);

            if (prevNode != null)
            {
                var edge = CreateEdge(prevNode, node);
                branch.AddEdge(edge);
            }

            prevNode = node;
            nodeIndex++;

            // ParseNode leaves _currLine at the first line that *ends* the node.
            // If that line is an explicit end-of-node '---', consume it here.
            if (GetLineType(_currLine) == LineType.EndNode)
            {
                _currLine = _streamReader!.ReadLine();
            }

        }

        return branch;


    }


    // REQUIRE: StreamReader != null
    // EFFECTS: Reads Components of the node 
    private DialogueNode ParseNode(DialogueScene scene, DialogueBranch branch, int nodeIndex)
    {
        Debug.Assert(_streamReader != null);

        int choiceIndex = 0;
        string nodeId = $"{scene.ID}_{branch.ID}_node_{nodeIndex}";
        DialogueNode currentNode = new DialogueNode(nodeId);

        // Determine line type
        LineType type = GetLineType(_currLine);

        while (_currLine != null && !IsNodeFinished(currentNode, type))
        {
            _currLine = _currLine.Trim();
            switch (type)
            {
                case LineType.Blank:
                    // skip blank lines
                    _currLine = _streamReader.ReadLine();
                    type = GetLineType(_currLine);
                    continue;

                case LineType.SpeakerLine:
                    // add speaker
                    var match = Regex.Match(_currLine!, SPEAKER_PATTERN);
                    // match.Value includes the trailing ':';
                    var speaker = match.Value.Substring(0, match.Value.Length - 1).Trim();
                    currentNode.Speaker = speaker;

                    // check remaining text              
                    _currLine = _currLine!.Substring(match.Length).TrimStart();
                    type = GetLineType(_currLine);
                    if (type == LineType.TextLine)
                        goto case LineType.TextLine;
                    break;

                case LineType.TextLine:
                    var textField = new TextComponent($"text_{nodeIndex}");
                    textField.SetText(_language, _currLine);
                    currentNode.Text = textField;
                    break;

                case LineType.ChoiceLine:
                    string text = _currLine.Substring(CHOICE_FLAG.Length);
                    var choice = new TextComponent($"choice_{choiceIndex}");
                    choice.SetText(_language, text);
                    currentNode.AddChoice(choice);
                    choiceIndex++;
                    break;

                case LineType.GoToLine:
                    string destination = _currLine!.Substring(GO_TO_FLAG.Length).Trim();
                    var condition = (currentNode.Choices.Count > 0)
                        ? currentNode.Choices[^1].ID
                        : "default";

                    var edge = CreateEdge(scene, currentNode, destination, condition);
                    // IMPORTANT: add now; will be patched later if needed
                    branch.AddEdge(edge);
                    break;

                default:
                    throw new Exception("Unexpeted line in node: " + type);
            }

            _currLine = _streamReader.ReadLine();
            type = GetLineType(_currLine);
        }
        return currentNode;
    }

    // EFFECTS: create egde connecting this to destination
    //  saves information to backpatching list if destination node isn't created yet
    private DialogueEdge CreateEdge(DialogueScene scene, DialogueNode from, string destination, string condition)
    {
        DialogueEdge edge;
        LineType type = GetLineType(destination);

        if (LineType.BranchName == type)
        {
            String branch = destination.Substring(BRANCH_NAME_FLAG.Length);
            //  attempt to find branch
            if (scene.Branches.Keys.Contains(branch))
            {
                return CreateEdge(from, scene.Branches[branch].EntryNode, condition);
            }
            // if not found pend
            else
            {
                edge = new DialogueEdge(from);
                _fixups.Add(new Fixup(edge, type, branch));
            }

        }
        else // else is node name
        {
            // TODO add node navigation support
            throw new Exception();
        }
        return edge;
    }



    // EFFECT: Determines if the current node is finished using the current line
    // Nodes follow this structure:
    // Speaker: Name
    // - choices
    // if the line read violates this structure return true
    // if the line contains a branch or end node flag, return true
    private bool IsNodeFinished(DialogueNode currentNode, LineType type)
    {
        switch (type)
        {
            case LineType.EndOfFile:
                return true;
            case LineType.EndNode:
                return true;
            case LineType.BranchName:
                return true;

            case LineType.Blank:
                return false;
            case LineType.GoToLine:
                return false;
            case LineType.ChoiceLine:
                return false;

            case LineType.SpeakerLine:
                // If we don't have a speaker yet, allow it; otherwise new speaker means next node.
                return currentNode.Speaker != null;

            case LineType.TextLine:
                // If we already have text or choices, a new text line starts the next node.
                return currentNode.Text != null || currentNode.Choices.Count != 0;

            default:
                throw new Exception("Unexpected line in node: " + type);
        }
    }


    // EFFECTS: Returns the line type of the given licurrentScenene
    public LineType GetLineType(string? line)
    {
        if (line == null)
            return LineType.EndOfFile;

        if (Regex.IsMatch(line, ALL_WHITE_SPACE))
            return LineType.Blank;
        
        line = line.TrimStart();

        if (line.Trim().StartsWith(CHOICE_FLAG))
            return LineType.ChoiceLine;

        if (Regex.IsMatch(line, SPEAKER_PATTERN))
            return LineType.SpeakerLine;

        if (line.Trim().StartsWith(GO_TO_FLAG))
            return LineType.GoToLine;

        if (line.StartsWith(END_NODE_FLAG))
            return LineType.EndNode;

        if (line.StartsWith(BRANCH_NAME_FLAG))
            return LineType.BranchName;

        if (line.StartsWith(SCENE_NAME_FLAG))
            return LineType.SceneName;

        if (Regex.IsMatch(line, LANGUAGE_PATTERN))
            return LineType.LanguageName;



        return LineType.TextLine;

    }



    // EFFECTS: Return scene with name form line
    private DialogueScene ParseSceneName(string line)
    {
        Debug.Assert(LineType.SceneName == GetLineType(line));
        return new DialogueScene(line.Substring(SCENE_NAME_FLAG.Length));
    }

    // EFFECTS: Returns a branch with name from line
    private DialogueBranch ParseBranchName(string line)
    {
        Debug.Assert(LineType.BranchName == GetLineType(line));
        return new DialogueBranch(line.Substring(BRANCH_NAME_FLAG.Length));
    }

    private void FixEdges(DialogueScene scene)
    {
        foreach (var f in _fixups)
        {
            if (f.DestinationType == LineType.BranchName &&
                scene.Branches.Keys.Contains(f.DestinationName))
            {
                f.Edge.To = scene.Branches[f.DestinationName].EntryNode;
            }
            // TODO: add fixup for nodes when direct node jumps are implemented
        }
        _fixups.Clear();
    }
    

    private DialogueEdge CreateEdge(DialogueNode from, DialogueNode to) =>
        CreateEdge(from, to, "default");

    private DialogueEdge CreateEdge(DialogueNode from, DialogueNode to, string condition)
    {
        var edge = new DialogueEdge(from)
        {
            To = to,
            condition = condition
        };
        return edge;
    }
}
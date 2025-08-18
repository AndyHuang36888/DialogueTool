namespace DialogueSystem;

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Xml.Schema;
using Newtonsoft.Json.Linq;
using Xunit.Sdk;

// A parsing tool to translate human written script to data.
class DialougeParser
{
    private static readonly DialougeParser instance = new DialougeParser();

    const string GO_TO_FLAG = ">> ";
    const string END_NODE_FLAG = "---";
    const string SCENE_NAME_FLAG = "# ";
    const string BRANCH_NAME_FLAG = "## ";
    const string CHOICE_FLAG = "- ";
    const string SPEAKER_PATTERN = @"^.+(?<!\\):"; // any test from start to : and not \?
    const string ALL_WHITE_SPACE = @"^\s*$"; // white space from start to end
    const string ANY_TEXT = ".*";

    public enum LineType
    {
        SceneName, BranchName,
        TextLine, ChoiceLine, SpeakerLine, 
        EndNode,
        Blank, EndOfFile, Error
    }
    private int NodeCount = 1;
    private int ChoiceCount = 0;

    // TODO add language reading
    private string Language;

    // public DialogueNode currentNode { get; set; }
    // public DialogueScene currentScene { get; set; }
    // public DialogueBranch currentBranch { get; set; }

    private StreamReader? StreamReader;
    private DialougeParser()
    {
        Language = "Implement Language";
    }

    public static DialougeParser Instance()
    {
        return instance;
    }

    // reads file from path and creates a new scene
    public DialogueScene ReadFile(string path)
    {

        NodeCount = 1;
        ChoiceCount = 0;

        DialogueNode currentNode;
        DialogueScene currentScene;
        DialogueBranch currentBranch;
        StreamReader = new StreamReader(path);
        string? line = StreamReader.ReadLine();
        LineType lineType = GetLineType(line);


        // TODO read line for language

        // try to read user given names for the Language, scene and branch.
        // if none are found, set to default
        // there is no text, print error
        if (LineType.SceneName == lineType)
        {
            currentScene = ParseScene(line);
            line = StreamReader.ReadLine();
            lineType = GetLineType(line);
        }
        else
            currentScene = new DialogueScene("Default");

        if (LineType.BranchName == lineType)
        {
            currentBranch = ParseBranch(line);
            currentScene.AddBranch(currentBranch);
            line = StreamReader.ReadLine();
            lineType = GetLineType(line);
        } else
            currentBranch = new DialogueBranch("Default");




        // read nodes
        currentNode = new DialogueNode(currentScene.ID + "_" + currentBranch.ID + "_" + NodeCount);
        while (line != null)
        {
            // if reads a branch, create new branch with given name
            if (line.StartsWith(BRANCH_NAME_FLAG))
            {
                currentBranch = ParseBranch(line);
                currentScene.AddBranch(currentBranch);
            }
            else
            {
                currentNode = ParseNode(currentScene, currentBranch, line);
                currentBranch.AddNode(currentNode);
            }
               

            line = StreamReader.ReadLine();

        }
        return currentScene;

    }

    // EFFECTS: Reads Components of the node 
    private DialogueNode ParseNode(DialogueScene scene, DialogueBranch branch, string line)
    {
        DialogueNode currentNode = new DialogueNode(scene.ID + "_");

        // TODO stress test Parsing system
        // TODO fix parsing  split

        // Determine line type
        LineType type = GetLineType(line);

        while (IsNodeFinished(currentNode, type))
        {
            switch (type)
            {
                case LineType.SpeakerLine:
                    // add speaker
                    var match = Regex.Match(line, SPEAKER_PATTERN);
                    currentNode.Speaker = line.Substring(match.Index);

                    // check remaining text              
                    line = line.Substring(match.Index + match.Length);
                    type = GetLineType(line);
                    if (type == LineType.TextLine)
                        goto case LineType.TextLine;

                    break;

                case LineType.TextLine:
                    TextComponent textField = new TextComponent("LINE_" + NodeCount);
                    textField.SetText(Language, line);
                    currentNode.Text = textField;
                    break;

                case LineType.ChoiceLine:
                    string text = line.Substring(CHOICE_FLAG.Length);
                    TextComponent choice = new TextComponent("CHOICE_" + ChoiceCount);
                    choice.SetText(Language, text);
                    currentNode.AddChoice(choice);
                    break;
                default:
                    throw new Exception("Unexpeted line in node: "+ type);
            }

            line = StreamReader.ReadLine();
            if (line == null)
                break;
        }
        return currentNode;
    }
    // EFFECT: Determines if the current node is finished using the current line
    private bool IsNodeFinished(DialogueNode currentNode, LineType type)
    {
        switch (type)
        {
            case LineType.Blank:
                return false;
            case LineType.EndNode:
                return true;
            case LineType.SpeakerLine:
                if (currentNode.Speaker == null)
                    goto case LineType.TextLine;
                return true;
            case LineType.TextLine:
                if (currentNode.Text != null || currentNode.Choices.Count != 0)
                    return true;
                break;
            case LineType.ChoiceLine:
                return false;
            case LineType.BranchName:
                return true;
            default:
                throw new Exception("Unexpeted line in node: " + type);
        }
        return false;
    }


    // EFFECTS: Returns the line type of the given licurrentScenene
    public LineType GetLineType(string? line)
    {
        if (line == null)
            return LineType.EndOfFile;

        if (Regex.IsMatch(line, ALL_WHITE_SPACE))
            return LineType.Blank;

        if (line.StartsWith(CHOICE_FLAG))
            return LineType.ChoiceLine;

        if (Regex.IsMatch(line, SPEAKER_PATTERN))
            return LineType.SpeakerLine;

        if (line.StartsWith(END_NODE_FLAG))
            return LineType.EndNode;

        if (line.StartsWith(BRANCH_NAME_FLAG))
            return LineType.BranchName;

        if (line.StartsWith(SCENE_NAME_FLAG))
            return LineType.SceneName;



        return LineType.TextLine;

    }


    // EFFECTS: parses edge according to the destination passed
    private void ParseEdge(String destination)
    {
        // TODO
        // if (Regex.IsMatch(destination, BRANCH_NAME_FLAG + ".+"))
        // {
        //     DialogueEdge edge = new DialogueEdge(CurrentNode, );
        // }

    }
    
    // EFFECTS: Return scene with name form line
    private DialogueScene ParseScene(string line)
    {
        Debug.Assert(LineType.SceneName == GetLineType(line));
        return new DialogueScene(line.Substring(SCENE_NAME_FLAG.Length));
    }

    // EFFECTS: Returns a branch with name from line
    private DialogueBranch ParseBranch(string line)
    {
        Debug.Assert(LineType.BranchName == GetLineType(line)); 
        return new DialogueBranch(line.Substring(BRANCH_NAME_FLAG.Length));
    }
    

}
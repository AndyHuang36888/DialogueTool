namespace DialogueSystemTest
{
    using System.Runtime.InteropServices;
    using System.Security.AccessControl;
    using DialogueSystem;
    using Newtonsoft.Json.Linq;
    using Xunit;


    public class DialougeParserTest
    {
        const string testFile = "testFiles/exampleText.txt";

        // ID tags
        private const string LANGUAGE = "english";
        private const string SCENE = "scene";
        private const string BRANCH = "main";

        // Text to parse
        private const string NAME = "Name: ";
        private const string TEXT = "plain text";
        private const string OPTION = "- this is an option";

        private DialogueParser parser = DialogueParser.Instance();

        // helper funtion to generate IDs. Keep in case of refacoring of ID logic
        private string GetID(string textID, string branchID = BRANCH, string sceneID = SCENE, string languageID = LANGUAGE)
        {
            return languageID + "_" + sceneID + "_" + branchID + "_" + textID;
        }

        [Fact]
        public void SingleBranchTest()
        {
            string language = "English";
            DialogueScene scene = parser.ReadFile("../../../src/tests/testFiles/SingleBranch.txt");
            Assert.Equal("Test Text", scene.ID);
            Assert.Contains("Test Branch", scene.Branches.Keys);

            DialogueBranch branch = scene.Branches["Test Branch"];
            DialogueNode node = branch.EntryNode;

            Assert.Equal("Name", node.Speaker);
            Assert.Equal("This is name and text", node.Text.GetText(language));

            node = branch.DialogueGraph[node][0].To;
            Assert.Equal("Name", node.Speaker);
            Assert.Equal("This is name,text and choices", node.Text.GetText(language));
            for (int i = 1; i <= 3; i++)
            {
                Assert.Equal("choice " + i, node.Choices[i-1].GetText(language));
            }

            node = branch.DialogueGraph[node][0].To;
            Assert.Null(node.Speaker);
            Assert.Equal("This Is Text without a speaker", node.Text.GetText(language));

            node = branch.DialogueGraph[node][0].To;
            Assert.Null(node.Speaker);
            Assert.Null(node.Text);
            Assert.Equal("this is a node seperate to the top text", node.Choices[0].GetText(language));
            Assert.Equal("this node only has choices" , node.Choices[1].GetText(language));
            
        }

        [Fact]
        public void ParseLines()
        {

            // // TODO test will always fail due to object reference, refactor or overload equals.

            // TextComponent testTextComp = new TextComponent("No ID set");
            // // trivial tests

            // parser.ParseScene("# scene");
            // // full node: line 1 name & text, line 2 option, line 3 option
            // parser.ParseLine(NAME + TEXT); // test adding name and text in one line
            // parser.ParseLine(OPTION); // test adding single option
            // parser.ParseLine(OPTION); // test 2 deep for adding options

            // // check speaker
            // Assert.Equal(NAME, parser.currentNode.Speaker);

            // // check text field
            // testTextComp.ID = GetID("text_0");
            // testTextComp.SetText(LANGUAGE, TEXT);
            // Assert.Equal(testTextComp, parser.currentNode.Text);

            // // check option 1
            // testTextComp.ID = GetID("choice_0_0");
            // testTextComp.SetText(LANGUAGE, OPTION);
            // Assert.Equal(testTextComp, parser.currentNode.Choices[0]);

            // // check option 2
            // testTextComp.ID = GetID("_0_1");
            // Assert.Equal(testTextComp, parser.currentNode.Choices[1]);

            // // Test End current node & move to next
            // parser.ParseLine(TEXT);
            // Assert.True(parser.currentNode.Choices.Count == 0);

            // // TODO finish tests and check other behaviors. Parsing logic yet to be decided
        }


        // tests for Private functions
        // [Fact]
        // public void ParseBranchTest()
        // {
        //     DialogueBranch branch;

        //     branch = parser.ParseBranch("## Branch name");
        //     Assert.Equal(new DialogueBranch("Branch name").ID, branch.ID);
        // }

        //         [Fact]
        // public void ParseSceneTest()
        // {
        //     DialogueScene scene;

        //     scene = parser.ParseScene("# Scene Name");
        //     Assert.Equal(new DialogueScene("Scene Name").ID, scene.ID);
        // }

        [Fact]
        public void GetLineTypeTest()
        {
            DialogueParser.LineType type;

            type = parser.GetLineType(null);
            Assert.Equal(DialogueParser.LineType.EndOfFile, type);

            type = parser.GetLineType(" \t\r\r\t   ");
            Assert.Equal(DialogueParser.LineType.Blank, type);
            type = parser.GetLineType("         ");
            Assert.Equal(DialogueParser.LineType.Blank, type);

            type = parser.GetLineType("This is some text");
            Assert.Equal(DialogueParser.LineType.TextLine, type);

            LineTypeFlagLocationTest(DialogueParser.LineType.TextLine, "text");

            type = parser.GetLineType("- This is a choice");
            Assert.Equal(DialogueParser.LineType.ChoiceLine, type);

            LineTypeFlagLocationTest(DialogueParser.LineType.ChoiceLine, "- choice");

            type = parser.GetLineType("SpeakerName:");
            Assert.Equal(DialogueParser.LineType.SpeakerLine, type);

            type = parser.GetLineType("Here: is a name with text");
            Assert.Equal(DialogueParser.LineType.SpeakerLine, type);

            type = parser.GetLineType("This is a really  @#$%^&*()\\: weird name:");
            Assert.Equal(DialogueParser.LineType.SpeakerLine, type);

            LineTypeFlagLocationTest(DialogueParser.LineType.SpeakerLine, "Name: ");

            type = parser.GetLineType("# scene");
            Assert.Equal(DialogueParser.LineType.SceneName, type);

            LineTypeFlagLocationTest(DialogueParser.LineType.SceneName, "# the scene ");

            type = parser.GetLineType("## branch");
            Assert.Equal(DialogueParser.LineType.BranchName, type);

            LineTypeFlagLocationTest(DialogueParser.LineType.BranchName, "## the branch ");

            type = parser.GetLineType("<Dog Language>");
            Assert.Equal(DialogueParser.LineType.LanguageName, type);
        

        }

        private void LineTypeFlagLocationTest(DialogueParser.LineType expectedType, string pre)
        {

            DialogueParser.LineType actualType;

            actualType = parser.GetLineType(pre + "\\: in the middle");
            Assert.Equal(expectedType, actualType);

            actualType = parser.GetLineType(pre + "- in the middle");
            Assert.Equal(expectedType, actualType);

            actualType = parser.GetLineType(pre + ">> in the middle");
            Assert.Equal(expectedType, actualType);

            actualType = parser.GetLineType(pre + " --- in the middle");
            Assert.Equal(expectedType, actualType);

            actualType = parser.GetLineType(pre + "## in the middle");
            Assert.Equal(expectedType, actualType);

            actualType = parser.GetLineType(pre + " # in the middle");
            Assert.Equal(expectedType, actualType);
            } 
    }
    }



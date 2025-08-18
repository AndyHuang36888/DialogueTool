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

        private DialougeParser parser = DialougeParser.Instance();

        // helper funtion to generate IDs. Keep in case of refacoring of ID logic
        private string GetID(string textID, string branchID = BRANCH, string sceneID = SCENE, string languageID = LANGUAGE)
        {
            return languageID + "_" + sceneID + "_" + branchID + "_" + textID;
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

        [Fact]
        public void GetLineTypeTest()
        {
            DialougeParser.LineType type;

            type = parser.GetLineType(" \t\r\r\t   ");
            Assert.Equal(DialougeParser.LineType.Blank, type);
            type = parser.GetLineType("         ");
            Assert.Equal(DialougeParser.LineType.Blank, type);

            type = parser.GetLineType("This is some text");
            Assert.Equal(DialougeParser.LineType.TextLine, type);

            LineTypeFlagLocationTest(DialougeParser.LineType.TextLine, "text");

            type = parser.GetLineType("- This is a choice");
            Assert.Equal(DialougeParser.LineType.ChoiceLine, type);

            LineTypeFlagLocationTest(DialougeParser.LineType.ChoiceLine, "- choice");

            type = parser.GetLineType("SpeakerName:");
            Assert.Equal(DialougeParser.LineType.SpeakerLine, type);

            type = parser.GetLineType("Here: is a name with text");
            Assert.Equal(DialougeParser.LineType.SpeakerLine, type);

            type = parser.GetLineType("This is a really  @#$%^&*()\\: weird name:");
            Assert.Equal(DialougeParser.LineType.SpeakerLine, type);

            LineTypeFlagLocationTest(DialougeParser.LineType.SpeakerLine, "Name: ");

            type = parser.GetLineType("# scene");
            Assert.Equal(DialougeParser.LineType.SceneName, type);

            LineTypeFlagLocationTest(DialougeParser.LineType.SceneName, "# the scene ");

            type = parser.GetLineType("## branch");
            Assert.Equal(DialougeParser.LineType.BranchName, type);

            LineTypeFlagLocationTest(DialougeParser.LineType.BranchName, "## the branch ");
        

        }

            private void LineTypeFlagLocationTest(DialougeParser.LineType expectedType, string pre)
            {

                DialougeParser.LineType actualType;

                actualType = parser.GetLineType(pre + "\\: in the middle");
                Assert.Equal(expectedType, actualType);

                actualType = parser.GetLineType(pre + "- in the middle");
                Assert.Equal(expectedType, actualType);

                actualType = parser.GetLineType( pre + ">> in the middle");
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



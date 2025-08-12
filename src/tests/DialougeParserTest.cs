namespace DialogueSystemTest
{
    using System.Security.AccessControl;
    using DialogueSystem;
    using Xunit;


    public class DialougeParserTest
    {
        const string testFile = "testFiles/exampleText.txt";

        // ID tags
        private const string LANGUAGE = "EN";
        private const string SCENE = "SCENE";
        private const string BRANCH = "DEFAULT";

        // Text to parse
        private const string NAME = "Name: ";
        private const string TEXT = "plain text";
        private const string OPTION  = "- this is an option";
 
        private DialougeParser parser = DialougeParser.Instance();

        // helper funtion to generate IDs. Keep in case of refacoring of ID logic
        private string GetID(string textID, string branchID = BRANCH, string sceneID = SCENE, string languageID = LANGUAGE)
        {
            return languageID + "_" + sceneID + "_" + branchID + "_" + textID;
        }

        [Fact]
        public void ParseLines()
        {

            // TODO test will always fail due to object reference, refactor or overload equals.

            TextComponent testTextComp = new TextComponent("No ID set");
            // trivial tests

            // full node: line 1 name & text, line 2 option, line 3 option
            parser.ParseLine(NAME + TEXT); // test adding name and text in one line
            parser.ParseLine(OPTION); // test adding single option
            parser.ParseLine(OPTION); // test 2 deep for adding options

            // check speaker
            Assert.Equal(NAME, parser.CurrentNode.Speaker);

            // check text feild
            testTextComp.ID = GetID("TEXT_0");
            testTextComp.SetText(LANGUAGE, TEXT);
            Assert.Equal(testTextComp, parser.CurrentNode.Text);

            // check option 1
            testTextComp.ID = GetID("CHOICE_0_0");
            testTextComp.SetText(LANGUAGE, OPTION);
            Assert.Equal(testTextComp, parser.CurrentNode.Choices[0]);

            // check option 2
            testTextComp.ID = GetID("CHOICE_0_1");
            Assert.Equal(testTextComp, parser.CurrentNode.Choices[1]);

            // Test End current node & move to next
            parser.ParseLine(TEXT);
            Assert.True(parser.CurrentNode.Choices.Count == 0);

            // TODO finish tests and check other behaviors. Parsing logic yet to be decided




        }
    }
}



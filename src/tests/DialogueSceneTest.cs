
namespace DialogueSystemTest
{
using Xunit;
using DialogueSystem;

    public class DialogueSceneTest
    {
        [Fact]
        public void DialogueNodeConstructorTest()
        {
            string sceneID = "scene1";
            DialogueScene testScene = new DialogueScene(sceneID);
            Assert.Equal(sceneID, testScene.ID);
        }

        [Fact]
        public void AddNodeTest()
        {
            string sceneID = "scene1";
            DialogueScene testScene = new DialogueScene(sceneID);

            DialogueNode testNode1 = new DialogueNode("testNode1");
            DialogueNode testNode2 = new DialogueNode("testNode2");

            // test add 1 node
            testScene.AddNode(testNode1);
            Assert.Contains(testNode1, testScene.DialogueGraph.Keys);
            Assert.Single(testScene.DialogueGraph.Keys);

            // test 2 deep  
            testScene.AddNode(testNode2);
            Assert.Contains(testNode2, testScene.DialogueGraph.Keys);
            Assert.Equal(2, testScene.DialogueGraph.Keys.Count);
        }

        [Fact]
        public void AddEdgeTest()
        {
            DialogueNode nd1 = new DialogueNode("node1");
            DialogueNode nd2 = new DialogueNode("node2");
            DialogueNode nd3 = new DialogueNode("node3");

            DialogueScene testScene = new DialogueScene("sceneID");

            DialogueEdge edge12 = new DialogueEdge(nd1, nd2);
            testScene.AddEdge(edge12);

            // test add missing node, adding single edge
            Assert.Contains(nd1, testScene.DialogueGraph.Keys);
            Assert.Contains(nd2, testScene.DialogueGraph.Keys);
            Assert.Contains(edge12, testScene.DialogueGraph[nd1]);
            Assert.Empty(testScene.DialogueGraph[nd2]);

            DialogueEdge edge31 = new DialogueEdge(nd3, nd1);
            testScene.AddEdge(edge31);

            // test add existing, adding single edge
            Assert.Contains(nd1, testScene.DialogueGraph.Keys);
            Assert.Contains(nd3, testScene.DialogueGraph.Keys);
            Assert.Contains(edge31, testScene.DialogueGraph[nd3]);
        }
    }
}
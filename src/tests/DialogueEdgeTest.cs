using Xunit;
using DialogueSystem;

namespace DialogueSystemTest
{
    
    public class DialogueEdgeTest
    {
        [Fact]
        public void ConstrutorTest()
        {
            DialogueNode from = new DialogueNode("node1");
            DialogueNode to = new DialogueNode("node2");

            DialogueEdge edge1 = new DialogueEdge(from, to);
            Assert.Equal(from, edge1.From);
            Assert.Equal(to, edge1.To);
        }
    }
}
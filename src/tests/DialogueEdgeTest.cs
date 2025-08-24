using Xunit;
using DialogueSystem;

namespace DialogueSystemTest
{

    public class DialogueEdgeTest
    {
        private static DialogueNode Node(string id) => new DialogueNode(id);


        [Fact]
        public void DefaultEdgeTest()
        {
            var from = Node("A");
            var edge = new DialogueEdge(from, EdgeTrigger.Default());

            Assert.Same(from, edge.From);
            Assert.Null(edge.To);
            Assert.Equal(EdgeTriggerKind.Default, edge.Trigger.Kind);
            Assert.Equal(-1, edge.Trigger.ChoiceIndex);
            Assert.Null(edge.Trigger.PredicateExpr);
        }

        [Fact]
        public void ChoiceEdgeTest()
        {
            var from = Node("A");
            var edge = new DialogueEdge(from, EdgeTrigger.Choice(1));

            Assert.Equal(EdgeTriggerKind.Choice, edge.Trigger.Kind);
            Assert.Equal(1, edge.Trigger.ChoiceIndex);
            Assert.Null(edge.Trigger.PredicateExpr);
        }

        [Fact]
        public void PredicateEdgeTest()
        {
            var from = Node("A");
            var expr = "hasKey && !doorLocked";
            var edge = new DialogueEdge(from, EdgeTrigger.Predicate(expr));

            Assert.Equal(EdgeTriggerKind.Predicate, edge.Trigger.Kind);
            Assert.Equal(-1, edge.Trigger.ChoiceIndex);
            Assert.Equal(expr, edge.Trigger.PredicateExpr);
        }
    }
}
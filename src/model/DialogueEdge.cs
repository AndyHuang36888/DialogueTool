namespace DialogueSystem
{
    // Represents an edge connecting DialougeNodes
    public enum EdgeTriggerKind { Default, Choice, Predicate}

    public readonly struct EdgeTrigger
    {
        public EdgeTriggerKind Kind { get; init; }
        public int ChoiceIndex { get; init; }        // valid if Kind == Choice
        public string? PredicateExpr { get; init; }  // valid if Kind == Predicate
        private EdgeTrigger(EdgeTriggerKind kind, int index, string? expr)
        { Kind = kind; ChoiceIndex = index; PredicateExpr = expr; }

        public static EdgeTrigger Default() => new EdgeTrigger(EdgeTriggerKind.Default, -1, null);
        public static EdgeTrigger Choice(int index) => new EdgeTrigger(EdgeTriggerKind.Choice, index, null);
        // TODO: Support predicate logic
        public static EdgeTrigger Predicate(string expr) => new EdgeTrigger(EdgeTriggerKind.Predicate, -1, expr);
    }
    

    public sealed class DialogueEdge
    {
        public  DialogueNode From { get; init; }
        public DialogueNode? To { get; set; }                 // patched by fixups
        public  EdgeTrigger Trigger { get; set; }
        public int Priority { get; init; } = 0;               // first match wins

        public DialogueEdge(DialogueNode from, EdgeTrigger trigger)
        {
            From = from;
            Trigger = trigger;
        }
    }





}
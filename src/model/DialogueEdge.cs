namespace DialogueSystem
{
    // Represents an edge connecting DialougeNodes
    public class DialogueEdge
    {
        public DialogueNode From { get; set; }
        public DialogueNode To { get; set; }

        public DialogueEdge(DialogueNode from, DialogueNode to)
        {
            this.From = from;
            this.To = to;
        } 
    }
}
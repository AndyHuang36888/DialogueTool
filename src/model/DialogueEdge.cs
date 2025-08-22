namespace DialogueSystem
{
    // Represents an edge connecting DialougeNodes
    public class DialogueEdge
    {
        public DialogueNode From { get; set; }
        public DialogueNode? To { get; set; }

        // condition
        public string condition { get; set; }

        public DialogueEdge(DialogueNode from)
        {
            this.From = from;
        }

        public DialogueEdge(DialogueNode from, DialogueNode to)
        {
            this.From = from;
            this.To = to;
        }

        // EFFECTS: check if edge is valid
        public void IsValid()
        {

        }

    }
}
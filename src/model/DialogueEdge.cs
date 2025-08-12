namespace DialogueSystem
{
    // Represents an edge connecting DialougeNodes
    public class DialogueEdge
    {
        DialogueNode CurrentNode { get; set; }
        DialogueNode NextNode { get; set; }

        public DialogueEdge(DialogueNode curr, DialogueNode next)
        {
            this.CurrentNode = curr;
            this.NextNode = next;
        }
    }
}
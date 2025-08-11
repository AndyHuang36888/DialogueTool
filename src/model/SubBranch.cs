namespace DialogueSystem
{
    // Represents a branch of dialouge nodes in a dialogue scene.
    // The branch is used as a organization tool for dialogue nodes.
    // It can be used to group nodes that belong to a differernt branch of the dialouge.
    public class SubBranch
    {
        // Unique identifier for this sub-branch.
        public string ID { get; set; }

        // List of dialogue nodes in this sub-branch.
        public List<DialogueNode> Nodes { get; set; }

        public SubBranch(string id)
        {
            this.ID = id;
            this.Nodes = new List<DialogueNode>();
        }

        public void AddNode(DialogueNode node)
        {
        }
    }
}       
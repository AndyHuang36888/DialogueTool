namespace DialogueSystem

{
    // Represents a scene made of dialogue nodes. These nodes are arranged in a graph structure.
    public class DialogueScene
    {
        // A unique identifier for this scene.
        public string ID { get; set; }

        // The first node of the dialogue scene.
        public DialogueNode EntryNode { get; set; }

        // A collection of all nodes in the dialogue scene.
        public List<DialogueNode> Nodes { get; set; } = new List<DialogueNode>();

        // Constructor for the DialogueScene.
        public DialogueScene(string id)
        {
            this.ID = id;
        }

        // EFFECTS: Executes the current dialogue node.
        // TODO: figure out what to do with current node.
        public void ExecuteCurrentNode()
        {
            // get logic comps
            // run in order?

        }

        // EFFECTS: Adds a new dialogue node to the scene.
        // Throws ArgumentException if a node with the same ID already exists.
        public void AddNode(DialogueNode node)
        {
        }


    }
} 
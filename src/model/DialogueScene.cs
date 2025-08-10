namespace DialogueSystem

{
    // Represents a scene made of dialogue nodes. These nodes are arranged in a graph structure.
    public class DialogueScene
    {
        // A unique identifier for this scene.
        public string ID { get; set; }

        // The root node of the dialogue scene.
        public DialogueNode RootNode { get; set; }

        // Constructor for the DialogueScene.
        public DialogueScene(string id, DialogueNode rootNode)
        {
            this.ID = id;
            RootNode = rootNode;
        }
    }
} 
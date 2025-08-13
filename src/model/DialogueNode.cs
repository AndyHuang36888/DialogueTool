using System;

namespace DialogueSystem
{
    // Represents a node in the dialogue system, which can be composed of different dialogue components.
    // Each node must at least have one component.
    // The components
    public class DialogueNode
    {

        // Optional speaker name for this dialogue line or component.
        // Can be empty for narration.
        public string Speaker { get; set; }

        // Unique identifier for this dialogue node.
        // !!! do I need this
        public string ID { get; set; }

        // Set of choices available in this dialogue node.
        public List<TextComponent> Choices { get; }

        // Text displayed in this dialogue node.
        public TextComponent Text { get; set; }

        // TODO: figure out logic component
        // public LogicComponent Logic { get; set; }


        public DialogueNode(string id, string speaker = "")
        {
            this.ID = id;
            this.Speaker = speaker;
            this.Choices = new List<TextComponent>();
        }
        
        // EFFECTS: Checks if the node is valid.
        public bool IsValid()
        {
            return false; // TODO: Implement
        }
    }
}
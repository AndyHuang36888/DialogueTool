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
        public string Id { get; set; }

        // Set of components that make up this dialogue node.
        public hashset<DialogueComponent> Components { get; set; }

        // Excutes the components in this dialogue node in order.
        public void Execute()
        {
            // TODO
        }

    }
}
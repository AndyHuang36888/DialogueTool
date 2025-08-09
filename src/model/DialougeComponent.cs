using System;

namespace DialogueSystem
{
    // Base class for all dialogue components.
    public abstract class DialogueComponent
    {
        // A unique identifier for this component..
        public string Id { get; set; }

        

        // Order in the dialogue Node
        public int Order { get; set; }

        public list<DialogueComponent> NextComponents { get; set; }

        // Constructor for the DialogueComponent.
        protected DialogueComponent(string id, int order = 0)
        {
            this.Id = id;
            this.Order = order;
        }
    }
}

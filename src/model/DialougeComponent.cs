using System;
namespace DialogueSystem
{
    // Base class for all dialogue components.
    public abstract class DialogueComponent
    {
        // A unique identifier for this component..
        public string Id { get; set; }
        // Constructor for the DialogueComponent.
        protected DialogueComponent(string id)
        {
            this.Id = id;
        }
    }
}

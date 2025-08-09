namespace DialogueSystem

{
    // Represents a text field in the dialogue node
    public class TextComponent : DialogueSystem.DialogueComponent
    {

        // The text content of this dialogue component. Usually a section of dialogue or narration.
        public string Text { get; set; }

        // EFFECTS: Creates a new TextComponent with the specified id, text, speaker, and order.
        public TextComponent(string id, string text = "")
            : base(id)
        {
            this.Text = text;
        }
    }
}
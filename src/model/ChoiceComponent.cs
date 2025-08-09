namespace DialogueSystem

{
    public class TextComponent : DialogueSystem.DialogueComponent
    {

        // The text content of this dialogue component. Usually a section of dialogue or narration.
        public string Text { get; set; }
        
        // EFFECTS: Creates a new TextComponent with the specified id, text, speaker, and order.
        public TextComponent(string id, string text = "", int order = 0)
            : base(id, order)
        {
            this.Text = text;
        }
    }
}
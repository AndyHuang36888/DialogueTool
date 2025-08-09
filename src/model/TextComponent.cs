namespace DialogueSystem

{
    public class TextComponent : DialogueSystem.DialogueComponent
    {
        public string Text { get; set; }
        
        public TextComponent(string id, string text = "", int order = 0)
            : base(id, order)
        {
            Text = text;
        }
    }
}
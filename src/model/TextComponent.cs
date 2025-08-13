using System.Collections.Generic;

namespace DialogueSystem


{
    public class TextComponent
    {
        private Dictionary<string, string> Text;

        // A unique refference ID for this text component. used to reference the text component without needing to use the text itself.
        public string ID {get; set;}

        // EFFECTS: Creates a new TextComponent with the specified id and an empty Text dictionary.
        public TextComponent(string id)
        {
            this.ID = id;
            Text = new Dictionary<string, string>();
        }
        // MODIFIES: this
        // EFFECTS: Adds a new text entry to the Text dictionary. 
        // Replaces any existing entry with the same key.
        // The first value is the language the text is in. The second value is the actual text.
        public void SetText(string key, string value)
        {
            Text[key] = value;
        }

        // MODIFIES: this
        // EFFECTS: Removes the text entry with the specified key from the Text dictionary.
        // Throws KeyNotFoundException if the key does not exist.
        public void RemoveText(string key)
        {
            if (Text.ContainsKey(key))
            {
                Text.Remove(key);
            }
            else
            {
                throw new KeyNotFoundException($"Key '{key}' not found in TextComponent.");
            }
        }

        // EFFECTS: Returns the text entry with the specified key from the Text dictionary.
        public string GetText(string key)
        {
            if (Text.ContainsKey(key))
            {
                return Text[key];
            }
            else
            {
                throw new KeyNotFoundException($"Key '{key}' not found in TextComponent.");
            }

        }
    }
}
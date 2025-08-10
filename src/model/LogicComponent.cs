namespace DialogueSystem
{
    // Represents a logic component in the dialogue node. The component can be used to execute logic and navigate through the dialogue.
    public class LogicComponent
    {
        public string Id { get; set; }
        LogicComponent(string id)
        {
            this.Id = id;
        }
    }
}
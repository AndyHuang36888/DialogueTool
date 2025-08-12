namespace DialogueSystem
{
    // Represents a logic component in the dialogue node. The component can be used to execute logic and navigate through the dialogue.
    // TODO add control flow latter, focus on traversal now
    public class LogicComponent
    {
        // flag that tells the scene to end
        const string END = "$END";

        public struct Edge(string currID, string nextID = "$END")
        {
            string CurrentNodeID = currID;
            string nextNodeID = nextID;
        }

        // a conditional statement that can be checked
        public struct Condition
        {
            string Expression;
        }

        // a commanded statement that can be run
        public struct Commands
        {
            List<string> Expression;
        }


    }
}
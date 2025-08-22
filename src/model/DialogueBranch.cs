
namespace DialogueSystem

{


    // Represents a scene made of dialogue nodes. 
    public class DialogueBranch
    {
        // A unique identifier for this scene.
        public string ID { get; set; }

        public DialogueNode? EntryNode { get; set; }


        // Adjacency List that reprecents the dialogue graph
        // edges are directed.
        public Dictionary<DialogueNode, List<DialogueEdge>> DialogueGraph { get; }

        // TODO Confirm  how to add branches

        public DialogueBranch(string id)
        {
            this.ID = id;
            this.DialogueGraph = new Dictionary<DialogueNode, List<DialogueEdge>>();
        }

        // EFFECTS: Adds a new dialogue node to the scene. 
        // If the node already exists, do nothing
        public void AddNode(DialogueNode node)
        {
            if (DialogueGraph.Keys.Count == 0)
            {
                EntryNode = node;
            }
            if (!DialogueGraph.Keys.Contains(node))
            {
                DialogueGraph.Add(node, new List<DialogueEdge>());
            }
        }

        // MODIFIES: this
        // EFFECTS: adds an edge to the scene that connects the first node to the second
        // If the start of the edge isn't in this scene, throw an error

        public void AddEdge(DialogueEdge edge)
        {
            if (!DialogueGraph.Keys.Contains(edge.From))
            {
                throw new Exception("Invalid edge added, missing starting node");
            }
            DialogueGraph[edge.From].Add(edge);
        }


    }
} 
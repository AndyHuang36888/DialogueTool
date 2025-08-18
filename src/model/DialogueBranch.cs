
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
            if (!DialogueGraph.Keys.Contains(node))
            {
                DialogueGraph.Add(node, new List<DialogueEdge>());
            }
        }

        // MODIFIES: this
        // EFFECTS: adds an edge to the scene that connects the first node to the second
        // If any of the edges don't exist in this scene, add them to the graph

        public void AddEdge(DialogueEdge edge)
        {
            // add nodes, does nothing if already added
            AddNode(edge.From);
            AddNode(edge.To);

            DialogueGraph[edge.From].Add(edge);
        }


    }
} 
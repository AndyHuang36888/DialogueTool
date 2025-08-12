using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;

namespace DialogueSystem

{


    // Represents a scene made of dialogue nodes. 
    public class DialogueScene
    {
        // A unique identifier for this scene.
        public string ID { get; set; }

        public DialogueNode EntryNode;

        // Adjacency List that reprecents the dialogue graph
        public Dictionary<DialogueNode, List<DialogueEdge>> AdjacencyList;

        // TODO Confirm  how to add branches

        public DialogueScene(string id)
        {
            this.ID = id;
        }

        // EFFECTS: Executes the current dialogue node.
        // TODO: figure out what to do with current node.
        public void ExecuteCurrentNode()
        {
            // get logic comps
            // run in order?
        }

        // EFFECTS: Adds a new dialogue node to the scene. 
        public void AddNode(DialogueNode node)
        {

        }




    }
} 
namespace DialogueSystem

{
    // Represents a scene made of dialogue nodes. 
    public class DialogueScene
    {
        // A unique identifier for this scene.
        public string ID { get; set; }

        // The first node of the dialogue scene.
        public DialogueNode EntryNode { get; set; }

        // A sub-branch of dialogue nodes that can be traversed.
        // The default branch is the main dialogue branch.
        public Dictionary<string, SubBranch> SubBranchs = new Dictionary<string, SubBranch>();


        // Constructor for the DialogueScene.
        public DialogueScene(string id)
        {
            SubBranchs.Add("DEFAULT", new SubBranch("DEFAULT"));
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
        public void AddNode(DialogueNode node, string subBranchId = "DEFAULT")
        {
        }

        //EFFECTS: Adds a new sub-branch to the scene.
        public void AddSubBranch(SubBranch subBranch)
        {
            if (!SubBranchs.ContainsKey(subBranch.ID))
            {
                SubBranchs[subBranch.ID] = subBranch;
            }
            else
            {
                throw new ArgumentException($"Sub-branch with ID '{subBranch.ID}' already exists.");
            }
        }

        // EFFECTS: Gets the sub-branch with the specified ID.
        // If the sub-branch does not exist, throws KeyNotFoundException.
        public SubBranch GetSubBranch(string id)
        {
            return null; // TODO: Implement
        }

        // EFFECTS: Removes the sub-branch with the specified ID. 
        // if the sub-branch does not exist, does nothing.
        public void RemoveSubBranch(string id)
        {

        }
    }
} 
using System.Reflection;
using Microsoft.VisualBasic;

namespace DialogueSystem;

class DialogueScene
{
    public string ID;
    public Dictionary<string, DialogueBranch> Branches;

    public DialogueBranch? MainBranch = null;

    public DialogueScene(string id)
    {
        this.ID = id;
        this.Branches = new Dictionary<string, DialogueBranch>();
    }

    // REQUIRES: branch.ID is unique in this scene 
    // adds branch to scene, does nothing if branch is already addec
    public void AddBranch(DialogueBranch branch)
    {
        if (MainBranch == null)
        {
            MainBranch = branch;
        }
        if (!Branches.ContainsKey(branch.ID))
        {
            Branches.Add(branch.ID, branch);
        }
        // TODO Throws error
    }

    // REQUIRES: the branch id exists in this scene
    // EFFECTS: returns the branch with the given name.
    public DialogueBranch GetBranch(string branch)
    {
        return Branches[branch];
    }
}
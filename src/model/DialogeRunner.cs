using System.Net.Http.Headers;

namespace DialogueSystem
{
    // runs a scene
    public class DialogueRunner
    {
        private static readonly DialogueRunner instance = new DialogueRunner();
        private DialogueNode _currNode;

        private DialogueBranch _currBranch;
        private DialogueScene _currScene;
        private string _language;

        private DialogueRunner()
        {

        }

        // MODIFIES: this
        // EFFECTS: Enters the given scene and changes the current node
        // public void EnterScene(DialogueScene scene, string Language)
        // {
        //     _currScene = scene;
        //     _currBranch = scene.MainBranch;
        //     _currNode = _currBranch.EntryNode;
        //     _language = Language;

        //     // TODO implement
        // }
        public DialogueNode GetNode()
        {
            return _currNode;
        }

        // MODIFIES: this
        // EFFECTS: Enters the next node accoriding to the dialogue scene graph
        public void Next(string condition)
        {

        }

        private void JumpBranch() {
            
        }


    }
}
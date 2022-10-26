using System.Collections;

namespace StablerCharacter
{
    public sealed class Chapter
    {
        public static string mainBranchName = "main";

        public string chapterName;
        
        Dictionary<string, Branch> branches = new();
        Branch currentBranch;

        public Chapter(string chapterName, Dictionary<string, Branch> branches)
        {
            this.chapterName = chapterName;
            if (!branches.TryGetValue(mainBranchName, out Branch? branch))
                throw new InvalidDataException("The main branch does not exist. Maybe you forgot to set the mainBranchName?");
            this.branches = branches;
            currentBranch = branch;
        }

        public Dialog GetCurrentDialog() => currentBranch.GetCurrentDialog();
        
        public Dialog GetNextDialog() => currentBranch.GetNextDialog();
        
        public void SwitchBranch(string branchName)
        {
            if (!branches.TryGetValue(branchName, out Branch? branch))
                throw new KeyNotFoundException($"The branch \"{branchName}\" does not exists.");
            currentBranch = branch;
        }

        public override string ToString()
        {
            return "";
        }
    }
}

using System.Linq;

namespace StablerCharacter
{
    public sealed class StoryManager
    {
        public Chapter[] chapters;
        ushort chapterIndex;

        public StoryManager(Chapter[] chapters)
        {
            this.chapters = chapters;
        }

        public Dialog GetCurrentDialog() => chapters[chapterIndex].GetCurrentDialog();
        public Dialog GetNextDialog() => chapters[chapterIndex].GetNextDialog();
        public void SwitchBranch(string branchName) => chapters[chapterIndex].SwitchBranch(branchName);
        public void SwitchChapter(ushort chapterIndex) => this.chapterIndex = chapterIndex;
        /// <summary>
        /// This method switches to the chapter specified by searching using its name.
        /// 
        /// Note that using this method, you must make sure that all of the chapters you created
        /// has a unique name. Because well, This method will only use the first search result it found.
        /// </summary>
        /// <param name="chapterName">The chapter name you'd like to switch to.</param>
        public void SwitchChapter(string chapterName)
        {
            for(ushort i = 0; i < chapters.Length; i++)
            {
                if (chapters[i].chapterName == chapterName)
                {
                    chapterIndex = i;
                    break;
                }
            }
        }

        public override string ToString()
        {
            return "";
        }
    }
}

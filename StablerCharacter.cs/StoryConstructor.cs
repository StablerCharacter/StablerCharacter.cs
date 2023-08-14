using lizzie;
using StablerCharacter.Events;

namespace StablerCharacter
{
    /// <summary>
    /// A class to compile Lizzie (a scripting language, <see cref="https://github.com/polterguy/lizzie"/>)
    /// with some extensions method to make Lizzie usable for creating stories.
    /// </summary>
    public sealed partial class StoryConstructor
    {
        // A list of the chapters. An instance of a Chapter class contains basically the "branches" variable
        // defined below, and a chapter also has a string variable.
        List<Chapter> chapters = new();
        // A dictionary which the key is a string and the value is of type Branch.
        // An instance of a Branch contains an array of the Dialog class, and a ushort variable.
        Dictionary<string, Branch> branches = new();
        // a list of a class called Dialog, containing 2 string variable and an event
        List<Dialog> dialogs = new();
        string currentChapterName = string.Empty;
        string currentBranchName = string.Empty;

        [Bind(Name = "dialog")]
        object? Dialog(Binder<StoryConstructor> _, Arguments arguments)
        {
            Dialog dialog = new()
            {
                Message = arguments.Get<string>(0),
                PersonSpeaking = arguments.Get<string>(1)
            };
            for (int i = 2; i < arguments.Count; i++)
            {
                dialog.Events += ((IDialogEvent)arguments.Get(i)).Triggered;
            }

            dialogs.Add(dialog);
            return null;
        }

        [Bind(Name = "branch")]
        object? Branch(Binder<StoryConstructor> _, Arguments arguments)
        {
            if (!string.IsNullOrEmpty(currentBranchName))
            {
                branches.Add(currentBranchName, new(dialogs.ToArray()));
                dialogs.Clear();
            }
            currentBranchName = arguments.Get<string>(0);
            return null;
        }

        [Bind(Name = "chapter")]
        object? Chapter(Binder<StoryConstructor> _, Arguments arguments)
        {
            if(!string.IsNullOrEmpty(currentChapterName))
            {
                chapters.Add(new(currentChapterName, branches));
                branches.Clear();
            }
            currentChapterName = arguments.Get<string>(0);
            return null;
        }

        public StoryManager FinalizeStory()
        {
            if (!string.IsNullOrEmpty(currentBranchName))
            {
                branches.Add(currentBranchName, new(dialogs.ToArray()));
                dialogs.Clear();
            }
            if (!string.IsNullOrEmpty(currentChapterName))
            {
                chapters.Add(new(currentChapterName, branches));
                branches.Clear();
            }
            return new(chapters.ToArray());
        }

        private StoryConstructor() {}

        public static StoryManager compileFromString(string code)
        {
            StoryConstructor storyConstructor = new();
            var lambda = LambdaCompiler.Compile(storyConstructor, code);
            lambda();
            return storyConstructor.FinalizeStory();
        }

        /// <summary>
        /// Reads a file and compile it.
        /// Note that this uses File.ReadAllText which *might* cause memory issue if your file is big.
        /// Or, you can also seperate a file for each chapter and use the compileFromFiles method instead.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static StoryManager compileFromFile(string path) =>
            compileFromString(File.ReadAllText(path));

        public static StoryManager compileFromFiles(params string[] paths)
        {
            StoryConstructor storyConstructor = new();
            foreach (string path in paths)
            {
                LambdaCompiler.Compile(storyConstructor, File.ReadAllText(path))();
            }
            return storyConstructor.FinalizeStory();
        }
    }
}

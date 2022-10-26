using StablerCharacter;
using StablerCharacter.Scenes;

class Program
{
    public static void Main(string[] args)
    {
        System.Console.WriteLine("Hello, world!");
        GameManager.gameConfig.gameName = "TestGame";
        GameManager.Run(StoryConstructor.compileFromString(@"
chapter(""Chapter 1"")
branch(""main"")
dialog(""Hello, world."")
dialog(""This is just a test game. nothing much."")
"));
    }
}
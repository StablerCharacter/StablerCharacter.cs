using StablerCharacter;
using StablerCharacter.Scenes;

class Program
{
    public static void Main()
    {
        Console.WriteLine("Hello, world!");
        GameManager.GameConfig.GameName = "TestGame";
        GameManager.GameConfig.WindowWidth = 1280;
        GameManager.GameConfig.WindowHeight = 720;
        GameManager.Run(StoryConstructor.compileFromString(@"
chapter(""Chapter 1"")
branch(""main"")
dialog(""Hello, world."", """", information(""Hello from lizzy""))
dialog(""This is just a test game. nothing much."")
"), new MainMenuScene(), new GameScene(new()));
    }
}
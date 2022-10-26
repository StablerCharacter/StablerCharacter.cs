using Raylib_cs;
using StablerCharacter.Scenes;

namespace StablerCharacter
{
    public struct GameConfig
    {
        public ushort windowWidth;
        public ushort windowHeight;
        public string gameName = "Hello, world!";
        public Color background;

        public GameConfig(ushort windowWidth, ushort windowHeight) : this()
        {
            this.windowWidth = windowWidth;
            this.windowHeight = windowHeight;
            this.background = Color.CORNFLOWER_BLUE;
        }

        public GameConfig(ushort windowWidth, ushort windowHeight, Color background)
        {
            this.windowWidth = windowWidth;
            this.windowHeight = windowHeight;
            this.background = background;
        }
    }

    public sealed class GameManager
    {
        public static GameConfig gameConfig = new(800, 600);
        public static Dictionary<string, Font> fontManager = new Dictionary<string, Font>();
        public static Scene? currentScene;

        /// <summary>
        /// Basically the wrapper for the Raylib_cs's LoadFont method.
        /// </summary>
        /// <param name="name">The name you'd like to call the font as, in your code.</param>
        /// <param name="fileName">The file name of the font</param>
        public static void LoadFont(string name, string fileName)
        {
            fontManager.Add(name, Raylib.LoadFont(fileName));
        }

        /// <summary>
        /// Run StablerCharacter.cs
        /// </summary>
        /// <param name="scenes">The scene indexes. The first scene in this array will be the first scene that is being loaded.</param>
        public static void Run(StoryManager story, params Scene[] scenes)
        {
            if (scenes.Length == 0)
                SceneManager.scenes = new Scene[] { new GameScene(new(), story) };
            else SceneManager.scenes = scenes;
            SceneManager.LoadFirstScene();
            Raylib.InitWindow(gameConfig.windowWidth, gameConfig.windowHeight, gameConfig.gameName);

            while(!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();
                currentScene?.Render();
                Raylib.EndDrawing();
            }
        }
    }
}

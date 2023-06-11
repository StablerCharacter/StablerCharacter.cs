using Raylib_cs;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using StablerCharacter.Scenes;

namespace StablerCharacter
{
    public struct GameConfig
    {
        public ushort WindowWidth;
        public ushort WindowHeight;
        public string GameName = "Hello, world!";
        public Color Background;

        public GameConfig(ushort windowWidth, ushort windowHeight) : this()
        {
            this.WindowWidth = windowWidth;
            this.WindowHeight = windowHeight;
            this.Background = Color.CORNFLOWER_BLUE;
        }

        public GameConfig(ushort windowWidth, ushort windowHeight, Color background)
        {
            this.WindowWidth = windowWidth;
            this.WindowHeight = windowHeight;
            this.Background = background;
        }
    }

    public sealed class GameManager
    {
        public static StoryManager Story { get; private set; } = new(new Chapter[] { new("", new() { { "main", new(new Dialog[] { new() }) } }) });
        public static Dictionary<string, Font> FontManager { get; private set; } = new() { { "RaylibDefault", Raylib.GetFontDefault() } };

#pragma warning disable CA2211 // Non-constant fields should not be visible
        public static GameConfig GameConfig = new(800, 600);
#pragma warning restore CA2211 // Non-constant fields should not be visible

        public static bool RequestExit = false;

        /// <summary>
        /// Basically the wrapper for the Raylib_cs's LoadFont method.
        /// </summary>
        /// <param name="name">The name you'd like to call the font as, in your code.</param>
        /// <param name="fileName">The file name of the font</param>
        public static void LoadFont(string name, string fileName)
        {
            FontManager.Add(name, Raylib.LoadFont(fileName));
        }

        /// <summary>
        /// Run StablerCharacter.cs
        /// </summary>
        /// <param name="scenes">The scene indexes. The first scene in this array will be the first scene that is being loaded.</param>
        public static void Run(StoryManager story, params Scene[] scenes)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("log-.txt", rollingInterval: RollingInterval.Day)
                .WriteTo.Console(
#if DEBUG
                    restrictedToMinimumLevel: LogEventLevel.Debug)
#else
                    restrictedToMinimumLevel: LogEventLevel.Information)
#endif
                .CreateLogger();

            Log.Information("Starting StablerCharacter.cs...");

            Log.Debug("[GameManager] Setting a new story");
            Story = story;
            Log.Debug("[GameManager] The story: " + story.ToString());

            if (scenes.Length == 0)
                SceneManager.Scenes = new Scene[] { new GameScene(new()) };
            else SceneManager.Scenes = scenes;
            SceneManager.LoadFirstScene();

            Log.Debug("[GameManager] Initializing Audio device...");
            Raylib.InitAudioDevice();

            Log.Debug("[GameManager] Initializing a new Window with Raylib...");
            Raylib.InitWindow(GameConfig.WindowWidth, GameConfig.WindowHeight, GameConfig.GameName);

            while(!Raylib.WindowShouldClose() && !RequestExit)
            {
                Raylib.BeginDrawing();
                SceneManager.CurrentScene?.Render();
                Raylib.EndDrawing();
            }

            Log.Debug("[GameManager] Window closed. Doing cleanups.");
            SceneManager.CurrentScene?.OnUnload();
            Raylib.CloseAudioDevice();
            Log.CloseAndFlush();
        }

        public static void Run(object? data)
        {
            if (data == null) return;
            (StoryManager Story, Scene[] Scenes) resultData = ((StoryManager, Scene[]))data;
            Run(resultData.Story, resultData.Scenes);
        }

        public static Task RunAsync(StoryManager story, params Scene[] scenes) => Task.Run(() => Run(story, scenes));

        public static Thread RunThread(out Action StartThread, StoryManager story, params Scene[] scenes)
        {
            Thread t = new(Run);
            StartThread = () => t.Start((story, scenes));
            return t;
        }
    }
}

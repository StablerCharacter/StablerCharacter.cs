using Raylib_cs;
using StablerCharacter.RenderObjects;
using System.Numerics;

namespace StablerCharacter.Scenes
{
    public sealed class MainMenuScene : Scene
    {
        /// <summary>
        /// The scene to load when the play button is clicked. The default is loading the next scene.
        /// </summary>
        public ushort? nextSceneIndex = null;
        /// <summary>
        /// The background color of the main menu.
        /// </summary>
        public Color backgroundColor = Color.DARK_SLATE_BLUE;
        readonly Button playButton = new(Color.BLACK, new Vector2(30, 75), new Vector2(100, 30), new TextInfo("Play"));
        readonly Button quitButton = new(Color.BLACK, new Vector2(30, 125), new Vector2(100, 30), new TextInfo("Quit"));

        public void OnLoad()
        {
            playButton.OnClickEvent += PlayButton_OnClickEvent;
            quitButton.OnClickEvent += QuitButton_OnClickEvent;
        }

        private void QuitButton_OnClickEvent(object? sender, EventArgs e)
        {
            Raylib.CloseWindow();
        }

        private void PlayButton_OnClickEvent(object? sender, EventArgs e)
        {
            if (nextSceneIndex == null) SceneManager.LoadNextScene();
            else SceneManager.LoadScene((ushort)nextSceneIndex);
        }

        public void OnUnload() { }

        public void Render()
        {
            Raylib.ClearBackground(backgroundColor);
            Raylib.DrawText(GameManager.gameConfig.gameName, 30, 30, 22, Color.WHITE);

            playButton.Render();
            quitButton.Render();
        }
    }
}

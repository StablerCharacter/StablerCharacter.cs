using Raylib_cs;
using StablerCharacter.RenderObjects;

namespace StablerCharacter.Scenes
{
    public sealed class GameScene : Scene
    {
        public Color BackgroundColor = Color.LIGHT_GRAY;
        public Texture2D? BackgroundImage;

        public GameScene(DialogStyle dialogStyle)
        {
            RenderObjects.Add(new StoryDialog(dialogStyle, GameManager.Story));
        }

        public override void Render()
        {
            Raylib.ClearBackground(BackgroundColor);
            if (BackgroundImage != null) Raylib.DrawTexture((Texture2D)BackgroundImage, 0, 0, Color.WHITE);
            base.Render();
        }
    }
}

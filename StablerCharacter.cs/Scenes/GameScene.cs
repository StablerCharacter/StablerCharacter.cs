using Raylib_cs;
using StablerCharacter.RenderObjects;

namespace StablerCharacter.Scenes
{
    public sealed class GameScene : Scene
    {
        public List<RenderObject> renderObjects = new();
        public Color backgroundColor = Color.LIGHT_GRAY;
        public Texture2D? backgroundImage;

        public GameScene(DialogStyle dialogStyle, StoryManager storyManager)
        {
            renderObjects.Add(new StoryDialog(dialogStyle, storyManager));
        }

        public void OnLoad()
        {
            renderObjects.ForEach(x => x.OnStart());
        }

        public void OnUnload()
        {
            renderObjects.ForEach(x => x.OnRemoved());
        }

        public void Render()
        {
            Raylib.ClearBackground(backgroundColor);
            if (backgroundImage != null) Raylib.DrawTexture((Texture2D)backgroundImage, 0, 0, Color.WHITE);
            renderObjects.ForEach(x => x.Render());
        }
    }
}

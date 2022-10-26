using StablerCharacter.RenderObjects;

namespace StablerCharacter.Scenes
{
    /// <summary>
    /// An empty scene, with a basic render object list.
    /// </summary>
    public class Scene
    {
        public List<RenderObject> renderObjects = new();

        public void Render()
        {
            renderObjects.ForEach(x => x.Render());
        }

        public void OnLoad()
        {
            renderObjects.ForEach(x => x.OnStart());
        }

        public void OnUnload()
        {
            renderObjects.ForEach(x => x.OnRemoved());
        }
    }
}

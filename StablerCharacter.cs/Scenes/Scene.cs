using StablerCharacter.RenderObjects;

namespace StablerCharacter.Scenes
{
    /// <summary>
    /// An empty scene, with a basic render object list.
    /// </summary>
    public class Scene
    {
        public List<RenderObject> RenderObjects = new();
        public bool HasBeenLoaded = false;

        public virtual void Render()
        {
            RenderObjects.ForEach(x => x.Render());
        }

        public virtual void OnLoad()
        {
            RenderObjects.ForEach(x => x.OnStart());
            HasBeenLoaded = true;
        }

        public virtual void OnUnload()
        {
            RenderObjects.ForEach(x => x.OnRemoved());
        }
    }
}

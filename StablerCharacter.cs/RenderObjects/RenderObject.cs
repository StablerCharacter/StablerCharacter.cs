using Raylib_cs;
using StablerCharacter.Scenes;
using System.Numerics;

namespace StablerCharacter.RenderObjects
{
    public abstract class RenderObject
    {
        /// <summary>
        /// Triggered when the object is clicked in the clickable area.
        /// </summary>
        public event EventHandler? OnClickEvent;
        /// <summary>
        /// When the mouse enters the area of the object
        /// </summary>
        public event EventHandler? OnHoverStart;
        /// <summary>
        /// Called every frame when the mouse is hovered on the object
        /// </summary>
        public event EventHandler? OnHover;
        /// <summary>
        /// When the mouse exited the area of the object
        /// </summary>
        public event EventHandler? OnHoverEnd;
        Scene? _currentScene;
        protected Scene CurrentScene
        {
            get
            {
                if (_currentScene == null && SceneManager.CurrentScene != null) _currentScene = SceneManager.CurrentScene;
#pragma warning disable CS8603 // Possible null reference return.
                return _currentScene;
#pragma warning restore CS8603 // Possible null reference return.
            }
            set
            {
                _currentScene = value;
            }
        }
        protected RectangleBounds ClickableArea;
        protected HashSet<string> Tags = new();
        bool isHovered;

        public void CheckClickEvents()
        {
            Vector2 mousePos = Raylib.GetMousePosition();
            // Check if the mouse position is NOT in the clickable area set
            if (!(mousePos.X >= ClickableArea.topleft.X && mousePos.X <= ClickableArea.topright.X &&
                mousePos.Y >= ClickableArea.topleft.Y && mousePos.Y <= ClickableArea.bottomleft.Y))
            {
                // If it *was* being hovered, trigger the on hover end event and set isHovered to false
                if (isHovered)
                {
                    OnHoverEnd?.Invoke(this, EventArgs.Empty);
                    isHovered = false;
                }
                return;
            }


            if (!isHovered)
            {
                OnHoverStart?.Invoke(this, EventArgs.Empty);
                isHovered = true;
            }

            OnHover?.Invoke(this, EventArgs.Empty);

            if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
            {
                OnClickEvent?.Invoke(this, EventArgs.Empty);
            }
        }

        public void PollEvents()
        {
            if(OnHoverStart != null || OnHover != null || OnHoverEnd != null || OnClickEvent != null)
                CheckClickEvents();
        }

        /// <summary>
        /// Remove an object from the current scene the object is in.
        /// </summary>
        /// <param name="renderObject"></param>
        public void Remove(RenderObject renderObject)
        {
            CurrentScene.RenderObjects.Remove(renderObject);
        }

        /// <summary>
        /// Get an object that *EXACTLY* matches the tags of an object.
        /// </summary>
        /// <param name="tags"></param>
        public RenderObject[] GetObjectsFromTags(params string[] tags)
        {
            return CurrentScene.RenderObjects.Where(x => x.Tags.SequenceEqual(tags)).ToArray();
        }

        public abstract void OnStart();
        public abstract void Render();
        public abstract void OnRemoved();
    }
}

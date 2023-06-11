using Raylib_cs;
using System.Numerics;

namespace StablerCharacter.RenderObjects
{
    /// <summary>
    /// A button.
    /// 
    /// This class ignores the TextInfo.Position field.
    /// </summary>
    public class Button : TextObject
    {
        public Color BackgroundColor;
        public Vector2 Position;
        public Vector2 Size;
        public int TextLeftMargin = 10;

        /// <summary>
        /// Initialize a new Button RenderObject.
        /// </summary>
        /// <param name="backgroundColor">The background color of the button.</param>
        /// <param name="position">The position of the button.</param>
        /// <param name="size">The size of the button.</param>
        /// <param name="textInfo">The information for the text inside the button.</param>
        public Button(Color backgroundColor, Vector2 position, Vector2 size, TextInfo textInfo)
        {
            this.BackgroundColor = backgroundColor;
            this.Position = position;
            this.Size = size;
            this.TextInfo = textInfo;
            ClickableArea = new Rectangle(position.X, position.Y, size.X, size.Y);
        }

        public override void OnStart() { }

        public override void Render()
        {
            PollEvents();

            Raylib.DrawRectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y, BackgroundColor);
            TextInfo.Position = new Vector2(Position.X + TextLeftMargin, Position.Y + Size.Y / 4);
            base.Render();
        }

        public override void OnRemoved() { }
    }
}

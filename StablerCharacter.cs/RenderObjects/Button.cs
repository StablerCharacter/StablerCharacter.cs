using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace StablerCharacter.RenderObjects
{
    public class Button : RenderObject
    {
        public Color backgroundColor;
        public Vector2 position;
        public Vector2 size;
        public TextInfo textInfo;
        public int textLeftMargin = 10;

        public Button(Color backgroundColor, Vector2 position, Vector2 size, TextInfo textInfo)
        {
            this.backgroundColor = backgroundColor;
            this.position = position;
            this.size = size;
            this.textInfo = textInfo;
            clickableArea = new Rectangle(position.X, position.Y, size.X, size.Y);
        }

        public override void OnStart() { }

        public override void Render()
        {
            PollEvents();

            Raylib.DrawRectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y, backgroundColor);
            Vector2 textPosition = new Vector2(position.X + textLeftMargin, position.Y + size.Y / 4);
            Raylib.DrawTextEx(textInfo.font, textInfo.text, textPosition, textInfo.fontSize, textInfo.textSpacing, textInfo.textColor);
        }

        public override void OnRemoved() { }
    }
}

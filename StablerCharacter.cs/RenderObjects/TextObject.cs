using Raylib_cs;

namespace StablerCharacter.RenderObjects
{
    public class TextObject : RenderObject
    {
        public TextInfo TextInfo;

        public TextObject() { TextInfo = new(); }

        public TextObject(string initialText)
        {
            TextInfo = new(initialText);
        }

        public TextObject(TextInfo initialTextInfo)
        {
            TextInfo = initialTextInfo;
        }

        public override void OnRemoved()
        {}

        public override void OnStart()
        {}

        public override void Render()
        {
            Raylib.DrawTextPro(
                TextInfo.Font,
                TextInfo.Text,
                TextInfo.Position,
                TextInfo.Origin,
                TextInfo.Rotation,
                TextInfo.FontSize,
                TextInfo.TextSpacing,
                TextInfo.TextColor);
        }
    }
}

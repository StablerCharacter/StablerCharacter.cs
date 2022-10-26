using Raylib_cs;

namespace StablerCharacter
{
    public class TextInfo
    {
        public string text = "Text";
        public Color textColor = Color.WHITE;
        public ushort fontSize = 18;
        public Font font = Raylib.GetFontDefault();
        public float textSpacing = 1.5f;

        public TextInfo() { }

        public TextInfo(string text)
        {
            this.text = text;
        }
    }
}

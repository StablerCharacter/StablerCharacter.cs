using Raylib_cs;
using Serilog;
using System.Numerics;

namespace StablerCharacter
{
    public sealed class TextInfo
    {
        string _fontName = "RaylibDefault";
        public Font Font = GameManager.FontManager["RaylibDefault"];
        public string FontName
        {
            get => _fontName;
            set
            {
                if (GameManager.FontManager.TryGetValue(value, out Font newFont))
                {
                    Font = newFont;
                    _fontName = value;
                }
                else Log.Warning($"[TextInfo] Unable to change a font of a text from {_fontName} to {value}.");
            }
        }
        public Vector2 Position = Vector2.Zero;
        public Vector2 Origin = Vector2.Zero;
        public string Text = "Text";
        public Color TextColor = Color.WHITE;
        public ushort FontSize = 18;
        public float TextSpacing = 1.5f;
        public float Rotation = 0f;

        public TextInfo() { }

        public TextInfo(string text)
        {
            this.Text = text;
        }
    }
}

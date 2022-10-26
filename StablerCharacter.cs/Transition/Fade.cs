using Raylib_cs;
using StablerCharacter.RenderObjects;

namespace StablerCharacter.Transition
{
    /// <summary>
    /// A Fade transition.
    /// </summary>
    public sealed class Fade : ITransition
    {
        public enum FadeType
        {
            FadeIn,
            FadeOut
        }

        public sealed class FadeSquare : RenderObject
        {
            FadeType fadeType;
            Color color;
            float duration;
            float time;

            public FadeSquare(float duration, Color fadeColor, FadeType fadeType)
            {
                this.duration = duration;
                this.fadeType = fadeType;
                color = fadeColor;
            }

            public override void OnRemoved()
            {
            }

            public override void OnStart()
            {
            }

            public override void Render()
            {
                if (fadeType == FadeType.FadeIn) time += Raylib.GetFrameTime() / duration;
                if (fadeType == FadeType.FadeOut) time -= Raylib.GetFrameTime() / duration;
                if (time > 1f) time = 1f;
                if (time < 0f) time = 0f;
                Raylib.DrawRectangle(0, 0, Raylib.GetRenderWidth(), Raylib.GetRenderHeight(), Raylib.Fade(color, time));
                if (time == 0f || time == 1f) Remove(this);
            }
        }

        /// <summary>
        /// The duration of the fade, in seconds.
        /// </summary>
        public float duration = 2;
        public Color fadeColor = Color.BLACK;

        /// <summary>
        /// Fade the fadeColor in.
        /// </summary>
        public void StartTransition()
        {

        }
        
        /// <summary>
        /// Fade out from the fadeColor
        /// </summary>
        public void EndTransition()
        {
        }
    }
}

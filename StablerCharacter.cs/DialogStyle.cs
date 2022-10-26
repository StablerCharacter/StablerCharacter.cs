using Raylib_cs;
using System.Drawing;
using System.Numerics;
using System.Reflection;

namespace StablerCharacter
{
    public sealed class DialogStyle
    {
        /// <summary>
        /// A vertical alignment
        /// </summary>
        public enum VAlignment
        {
            Top,
            Center,
            Bottom
        }

        public enum HAlignment
        {
            Left,
            Center,
            Right
        }

        public enum Relativeness
        {
            None,
            RelativeX,
            RelativeY,
            RelativeXY
        }

        public struct Position
        {
            // Simple positioning
            /// <summary>
            /// The vertical alignment of the object.
            /// 
            /// This is under the simple positioning mode.
            /// </summary>
            public VAlignment vAlignment;
            /// <summary>
            /// The horizontal alignment of the object.
            /// 
            /// This is under the simple positioning mode.
            /// </summary>
            public HAlignment hAlignment;
            /// <summary>
            /// The extra offset to add to the position calculated from vAlignment and hAlignment.
            /// 
            /// This is under the simple positioning mode.
            /// </summary>
            public Vector2 extraPositionOffset;

            // Precise positioning
            /// <summary>
            /// This is under precise positioning mode.
            /// 
            /// Set a custom position value. This is basically the same as setting
            /// VAlignment to Top and hAlignment to Left and set the offset to something.
            /// 
            /// If this is set, the variables under the section "Simple positioning" will be ignored.
            /// </summary>
            public Vector2? customPosition;

            /// <summary>
            /// Relativeness of the position. Works with both mode of positioning.
            /// </summary>
            public Relativeness relativeness;

            float GetPositionValueFromAlignment(string wantedAxis, Vector2 parentSize, Vector2 screenSize)
            {
                if ((wantedAxis == "y" && vAlignment == VAlignment.Top) || (wantedAxis == "x" && hAlignment == HAlignment.Left)) return 0f;
                if ((wantedAxis == "y" && vAlignment == VAlignment.Center) || (wantedAxis == "x" && hAlignment == HAlignment.Center))
                {
                    if (wantedAxis == "x")
                    {
                        if (relativeness == Relativeness.RelativeX || relativeness == Relativeness.RelativeXY)
                            return parentSize.X / 2;
                        return screenSize.X / 2;
                    }
                    if (relativeness == Relativeness.RelativeY || relativeness == Relativeness.RelativeXY)
                        return parentSize.Y / 2;
                    return screenSize.Y / 2;
                }
                if ((wantedAxis == "y" && vAlignment == VAlignment.Bottom) || (wantedAxis == "x" && hAlignment == HAlignment.Right))
                {
                    if (wantedAxis == "x")
                    {
                        if (relativeness == Relativeness.RelativeX || relativeness == Relativeness.RelativeXY)
                            return parentSize.X;
                        return screenSize.X;
                    }
                    if (relativeness == Relativeness.RelativeY || relativeness == Relativeness.RelativeXY)
                        return parentSize.Y;
                    return screenSize.Y;
                }
                return 0f;
            }

            /// <summary>
            /// Calculates the position of this object.
            /// 
            /// Note: This method does *NOT* take the object size into account, So the position returned is not accurate, except if the alignment is Top/Left.
            /// 
            /// This method will ignore the simple positioning mode if the customPosition variable is set.
            /// </summary>
            /// <param name="windowSize">The size of the window</param>
            /// <param name="hasParent">If this object has a parent or not</param>
            /// <param name="parentSize">The size of the parent (if the object does have a parent)</param>
            /// <param name="parentPosition">The position of the parent (if the object does have a parent)</param>
            /// <returns>The position of the object calculated from the simple positioning mode or precise positioning mode.</returns>
            public Vector2 GetPosition(Vector2 windowSize, bool hasParent, Vector2 parentSize, Vector2 parentPosition)
            {
                Vector2 finalPosition = new();
                if(customPosition != null)
                {
                    finalPosition = (Vector2)customPosition;
                    if(hasParent)
                    {
                        switch(relativeness)
                        {
                            case Relativeness.RelativeX:
                                return new(parentPosition.X + finalPosition.X, finalPosition.Y);
                            case Relativeness.RelativeY:
                                return new(finalPosition.X, parentPosition.Y + finalPosition.Y);
                            case Relativeness.RelativeXY:
                                return new(parentPosition.X + finalPosition.X, parentPosition.Y + finalPosition.Y);
                            case Relativeness.None:
                            default:
                                return finalPosition;
                        }
                    }
                    return finalPosition;
                }

                finalPosition = new(
                    GetPositionValueFromAlignment("x", parentSize, windowSize) + extraPositionOffset.X,
                    GetPositionValueFromAlignment("y", parentSize, windowSize) + extraPositionOffset.Y
                );

                switch (relativeness)
                {
                    case Relativeness.RelativeX:
                        return new Vector2(parentPosition.X + finalPosition.X, finalPosition.Y);
                    case Relativeness.RelativeY:
                        return new Vector2(parentPosition.X, parentPosition.Y + finalPosition.Y);
                    case Relativeness.RelativeXY:
                        return new Vector2(parentPosition.X + finalPosition.X, parentPosition.Y + finalPosition.Y);
                    case Relativeness.None:
                    default:
                        return finalPosition;
                }
            }

            /// <summary>
            /// Get a position of this object. Using this is the same as GetPosition(windowSize, false, new(), new())
            /// So, Basically get a position of this object without any parent.
            /// </summary>
            /// <param name="windowSize">The size of the window.</param>
            /// <returns>The position</returns>
            public Vector2 GetPosition(Vector2 windowSize)
            {
                return GetPosition(windowSize, false, new(), new());
            }
        }

        public Color dialogBoxBackgroundColor = new(0, 0, 0, 128);
        /// <summary>
        /// The position of the whole dialog box
        /// </summary>
        public Position dialogPosition = new()
        {
            vAlignment = VAlignment.Bottom,
            hAlignment = HAlignment.Center,
            extraPositionOffset = new(0, -50)
        };
        /// <summary>
        /// The size of the whole dialog box
        /// </summary>
        public Vector2 dialogBoxSize = new(350, 100);

        // Elements *inside* the dialog box
        public TextInfo personSpeakingTextInfo = new()
        {
            fontSize = 22
        };
        public TextInfo messageTextInfo = new();

        public Position personSpeakingTextPosition = new()
        {
            vAlignment = VAlignment.Top,
            hAlignment = HAlignment.Left,
            extraPositionOffset = new(50, 50),
            relativeness = Relativeness.RelativeXY
        };
        public Position messagePosition = new()
        {
            vAlignment = VAlignment.Center,
            hAlignment = HAlignment.Left,
            extraPositionOffset = new(50, 0),
            relativeness = Relativeness.RelativeXY
        };
    }
}

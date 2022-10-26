using Raylib_cs;
using System.Numerics;

namespace StablerCharacter
{
    public struct RectangleBounds
    {
        public Vector2 topleft;
        public Vector2 topright;
        public Vector2 bottomleft;
        public Vector2 bottomright;

        public RectangleBounds(Vector2 topleft, Vector2 topright, Vector2 bottomleft, Vector2 bottomright)
        {
            this.topleft = topleft;
            this.topright = topright;
            this.bottomleft = bottomleft;
            this.bottomright = bottomright;
        }

        public override string ToString()
        {
            return $"tl: {topleft.ToString()}\ntr: {topright.ToString()}\nbl: {bottomleft.ToString()}\nbr: {bottomright.ToString()}";
        }

        public static implicit operator RectangleBounds(Rectangle rectangle)
        {
            return new RectangleBounds(
                new Vector2(rectangle.x, rectangle.y),
                new Vector2(rectangle.x + rectangle.width, rectangle.y),
                new Vector2(rectangle.x, rectangle.y + rectangle.height),
                new Vector2(rectangle.x + rectangle.width, rectangle.y + rectangle.height)
            );
        }

        public static implicit operator Rectangle(RectangleBounds rectangleBounds)
        {
            return new Rectangle(
                rectangleBounds.topleft.X,
                rectangleBounds.topleft.Y,
                rectangleBounds.bottomright.X - rectangleBounds.topleft.X,
                rectangleBounds.bottomright.Y - rectangleBounds.topleft.Y
            );
        }

        public Vector2 this[byte index]
        {
            get {
                return index switch
                {
                    0 => topleft,
                    1 => topright,
                    2 => bottomleft,
                    3 => bottomright,
                    _ => throw new ArgumentOutOfRangeException(nameof(index), index, "Only a number in range of 0-3 is allowed to access the corresponding rectangle bound.")
                };
            }
            set
            {
                switch(index)
                {
                    case 0:
                        topleft = value;
                        break;
                    case 1:
                        topright = value;
                        break;
                    case 2:
                        bottomleft = value;
                        break;
                    case 3:
                        bottomright = value;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(index), index, "Only a number in range of 0-3 is allowed to access the corresponding rectangle bound.");
                };
            }
        }
    }
}

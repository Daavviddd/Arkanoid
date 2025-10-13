using System.Drawing.Drawing2D;

namespace Arkanoid.Clases
{
    public class RoundedPlatform
    {
        public Point Location { get; set; }
        public Size Size { get; set; }
        public Color Color { get; set; } = Color.Black;
        public int CornerRadius { get; set; } = 15;
        public int X => Location.X;
        public int Y => Location.Y;
        public int Width => Size.Width;
        public int Height => Size.Height;
        public Rectangle Bounds => new Rectangle(Location, Size);
        public RoundedPlatform() { }

        public RoundedPlatform(int x, int y, int width, int height)
        {
            Location = new Point(x, y);
            Size = new Size(width, height);
        }

        public void SetLocation(int x, int y)
        {
            Location = new Point(x, y);
        }

    }
}
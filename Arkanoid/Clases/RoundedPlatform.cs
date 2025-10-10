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

        /// <summary>
        /// Отрисовка платформы
        /// </summary>
        public void Draw(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;

            using (GraphicsPath path = CreateRoundedRectanglePath(0, 0, Width, Height, CornerRadius))
            using (var brush = new SolidBrush(Color))
            {
                g.TranslateTransform(Location.X, Location.Y);
                g.FillPath(brush, path);
                g.ResetTransform();
            }
        }

        /// <summary>
        /// Создание скруглкнной платформы
        /// </summary>
        private GraphicsPath CreateRoundedRectanglePath(int x, int y, int width, int height, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            Rectangle rect = new Rectangle(x, y, width - 1, height - 1);

            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
            path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
            path.CloseAllFigures();

            return path;
        }
    }
}
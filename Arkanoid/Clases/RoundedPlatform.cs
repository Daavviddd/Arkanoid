using System.Drawing.Drawing2D;

namespace Arkanoid.Clases
{
    public class RoundedPlatform
    {
        /// <summary>
        ///  позиция платформы на форме
        /// </summary>
        public Point Location { get; set; }

        /// <summary>
        /// размер
        /// </summary>
        public Size Size { get; set; }

        /// <summary>
        /// цвет платформы
        /// </summary>
        public Color Color { get; set; } = Color.Black;

        /// <summary>
        /// радус скругления платформы
        /// </summary>
        public int CornerRadius { get; set; } = 15;

        /// <summary>
        /// X-координата платформы
        /// </summary>
        public int X => Location.X;

        /// <summary>
        /// Y-координата платформы
        /// </summary>
        public int Y => Location.Y;

        /// <summary>
        /// ширина платформы
        /// </summary>
        public int Width => Size.Width;

        /// <summary>
        /// высота платформы
        /// </summary>
        public int Height => Size.Height;

        /// <summary>
        /// граница платформы
        /// </summary>
        public Rectangle Bounds => new Rectangle(Location, Size);

        /// <summary>
        /// конструктор по умолчанию
        /// </summary>
        public RoundedPlatform() { }

        /// <summary>
        /// конструктор с координатами
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public RoundedPlatform(int x, int y, int width, int height)
        {
            Location = new Point(x, y);
            Size = new Size(width, height);
        }

        /// <summary>
        /// установка новой позиции
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void SetLocation(int x, int y)
        {
            Location = new Point(x, y);
        }

    }
}


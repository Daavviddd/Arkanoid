using System.Drawing.Drawing2D;

namespace Arkanoid.Clases
{
    public class RoundedPlatform
    {
        /// <summary>
        ///  Позиция платформы на форме
        /// </summary>
        public Point Location { get; set; }

        /// <summary>
        /// Размер
        /// </summary>
        public Size Size { get; set; }

        /// <summary>
        /// Цвет платформы
        /// </summary>
        public Color Color { get; set; } = Color.Black;

        /// <summary>
        /// Радус скругления платформы
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
        /// Ширина платформы
        /// </summary>
        public int Width => Size.Width;

        /// <summary>
        /// Высота платформы
        /// </summary>
        public int Height => Size.Height;

        /// <summary>
        /// Граница платформы
        /// </summary>
        public Rectangle Bounds => new Rectangle(Location, Size);

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public RoundedPlatform() { }

        /// <summary>
        /// Конструктор с координатами
        /// </summary>
        public RoundedPlatform(int x, int y, int width, int height)
        {
            Location = new Point(x, y);
            Size = new Size(width, height);
        }

        /// <summary>
        /// Установка новой позиции
        /// </summary>
        public void SetLocation(int x, int y)
        {
            Location = new Point(x, y);
        }

    }
}

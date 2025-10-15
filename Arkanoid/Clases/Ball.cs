using System.Drawing.Drawing2D;

namespace Arkanoid.Clases
{
    internal class Ball
    {
        /// <summary>
        /// позиция мяча
        /// </summary>
        public Point Location { get; set; }

        /// <summary>
        /// рахмер мяча
        /// </summary>
        public Size Size { get; set; }

        /// <summary>
        /// скорость мяча по X
        /// </summary>
        public int VelocityX { get; set; }

        /// <summary>
        /// скорость мяча по Y
        /// </summary>
        public int VelocityY { get; set; }

        /// <summary>
        /// X-координата левого верхнего угла мяча
        /// </summary>
        public int X => Location.X;

        /// <summary>
        /// Y-координата левого верхнего угла мяча
        /// </summary>
        public int Y => Location.Y;

        /// <summary>
        /// Ширина мяча
        /// </summary>
        public int Width => Size.Width;

        /// <summary>
        /// высота мяча
        /// </summary>
        public int Height => Size.Height;

        /// <summary>
        /// X-координата левой границы мяча
        /// </summary>
        public int Left => Location.X;

        /// <summary>
        /// X-координата правой границы мяча
        /// </summary>
        public int Right => Location.X + Width;

        /// <summary>
        /// Y-координата верхней границы мяча
        /// </summary>
        public int Top => Location.Y;

        /// <summary>
        /// Y-координата нижней границы мяча
        /// </summary>
        public int Bottom => Location.Y + Height;

        /// <summary>
        /// границы мяча
        /// </summary>
        public Rectangle Bounds => new Rectangle(Location, Size);

        /// <summary>
        /// конструктор мяча с заданными параметрами
        /// </summary>
        /// <param name="startX"></param>
        /// <param name="startY"></param>
        /// <param name="velocityX"></param>
        /// <param name="velocityY"></param>
        /// <param name="size"></param>
        public Ball(int startX, int startY, int velocityX, int velocityY, int size)
        {
            this.Location = new Point(startX, startY);
            this.Size = new Size(size, size);
            this.VelocityX = velocityX;
            this.VelocityY = velocityY;
        }

        /// <summary>
        /// перемещение мяча 
        /// </summary>
        public void BallMove()
        {
            int newX = this.Location.X + VelocityX;
            int newY = this.Location.Y + VelocityY;
            this.Location = new Point(newX, newY);
        }

        /// <summary>
        /// отскок от горизонтальных поверхностей
        /// </summary>
        public void BounceHorizontal()
        {
            VelocityY = -VelocityY;
        }

        /// <summary>
        /// отскок от вертикальных поверхностей
        /// </summary>
        public void BounceVertical()
        {
            VelocityX = -VelocityX;
        }

    }
}

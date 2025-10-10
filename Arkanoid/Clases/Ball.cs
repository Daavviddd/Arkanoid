using System.Drawing.Drawing2D;

namespace Arkanoid.Clases
{
    internal class Ball
    {
        public Point Location { get; set; }
        public Size Size { get; set; }
        public int VelocityX { get; set; }
        public int VelocityY { get; set; }
        public int X => Location.X;
        public int Y => Location.Y;
        public int Width => Size.Width;
        public int Height => Size.Height;
        public int Left => Location.X;
        public int Right => Location.X + Width;
        public int Top => Location.Y;
        public int Bottom => Location.Y + Height;
        public Rectangle Bounds => new Rectangle(Location, Size);

        public Ball(int startX, int startY, int velocityX, int velocityY, int size)
        {
            this.Location = new Point(startX, startY);
            this.Size = new Size(size, size);
            this.VelocityX = velocityX;
            this.VelocityY = velocityY;
        }

        public void BallMove()
        {
            int newX = this.Location.X + VelocityX;
            int newY = this.Location.Y + VelocityY;
            this.Location = new Point(newX, newY);
        }

        public void BounceHorizontal()
        {
            VelocityY = -VelocityY;
        }

        public void BounceVertical()
        {
            VelocityX = -VelocityX;
        }

    }
}

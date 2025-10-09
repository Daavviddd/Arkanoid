using System.Drawing.Drawing2D;

namespace Arkanoid.Clases
{
    internal class Ball: PictureBox
    {
        public int VelocityX { get; set; }
        public int VelocityY { get; set; }
        public Ball(int startX, int startY, int velocityX, int velocityY, int size)
        {
            this.Location = new Point(startX, startY);
            this.Size = new Size(size, size);
            this.VelocityX = velocityX;
            this.VelocityY = velocityY;

            this.BackColor = Color.Transparent;

            CreateBallImage();
        }
        private void CreateBallImage()
        {
            Bitmap bmp = new Bitmap(this.Width, this.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Color.Transparent);

                using (SolidBrush brush = new SolidBrush(Color.White))
                {
                    g.FillEllipse(brush, 0, 0, this.Width - 1, this.Height - 1);
                }

                using (Pen pen = new Pen(Color.Black, 2))
                {
                    g.DrawEllipse(pen, 0, 0, this.Width - 1, this.Height - 1);
                }
            }

            this.Image = bmp;
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
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddEllipse(0, 0, this.Width - 1, this.Height - 1);

                using (SolidBrush brush = new SolidBrush(Color.White))
                {
                    e.Graphics.FillPath(brush, path);
                }

                using (Pen pen = new Pen(Color.Black, 2))
                {
                    e.Graphics.DrawPath(pen, path);
                }
            }
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate();
        }
    }
}

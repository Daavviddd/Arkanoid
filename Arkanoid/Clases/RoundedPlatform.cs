using System.Drawing.Drawing2D;

namespace Arkanoid.Clases
{
    public class RoundedPlatform : Control
    {
        private int cornerRadius = 15;

        public RoundedPlatform()
        {
            BackColor = Color.Black;
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                  ControlStyles.UserPaint |
                  ControlStyles.ResizeRedraw, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            using (GraphicsPath path = CreateRoundedRectanglePath(0, 0, Width, Height, cornerRadius))
            {
                Region = new Region(path);

                using (SolidBrush brush = new SolidBrush(BackColor))
                {
                    e.Graphics.FillPath(brush, path);
                }
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


        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate();
        }
    }
}
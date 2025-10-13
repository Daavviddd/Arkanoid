using System.Drawing.Drawing2D;
using Arkanoid.Clases;

namespace Arkanoid
{
    public partial class GameForm : Form
    {   
        int ballSize = 20;
        int ballOffset = 150;

        List<PictureBox> bricksPoint = new List<PictureBox>();
        RoundedPlatform platform;
        bool gameStarted = false;
        Ball ball;
        System.Windows.Forms.Timer gameTimer;

        public GameForm()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.KeyPreview = true;

            CreateControlsLabel();

            ball = new Ball(
                startX: this.Width / 2 - ballSize / 2,
                startY: this.Height - ballOffset,
                velocityX: 0,
                velocityY: 0,
                size: ballSize
            );

            var timerInterval = 16;

            gameTimer = new System.Windows.Forms.Timer();
            gameTimer.Interval = timerInterval;
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();
        }

        /// <summary>
        /// Создание кирпичей
        /// </summary>
        private void MakingBricks()
        {
            var bricksPerRow = 12;
            var brickRows = 5;
            var brickHeight = 50;
            var brickSpacing = 5;
            var brickMargin = 2;
            var availableWidth = this.Width - (2 * brickMargin) - ((bricksPerRow - 1) * brickSpacing);
            var brickWidth = availableWidth / bricksPerRow;
            

            Color[] colors = { Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.DarkBlue };

            for (int i = 0; i < brickRows; i++)
            {
                for (int j = 0; j < bricksPerRow; j++)
                {
                    float brickX = brickMargin + j * (brickWidth + brickSpacing);
                    float brickY = brickMargin + i * (brickHeight + brickSpacing);

                    PictureBox brick = new PictureBox();
                    brick.Location = new Point((int)brickX, (int)brickY);
                    brick.Height = brickHeight;
                    brick.Width = (int)brickWidth;
                    brick.BackColor = colors[i];
                    brick.BorderStyle = BorderStyle.FixedSingle;

                    this.Controls.Add(brick);
                    bricksPoint.Add(brick);
                }
            }
        }

        private void Game_form_Load(object sender, EventArgs e)
        {
            MakingBricks();
            CreatePlatform();
            ball.Location = new Point(this.Width / 2 - ballSize / 2, this.Height - ballOffset);
        }

        /// <summary>
        /// генерация платформы
        /// </summary>
        private void CreatePlatform()
        {
            var platformWidth = 150;
            var platformHeight = 30;
            var platformOffset = 100;

            var bottomX = (this.Width - platformWidth) / 2;
            var bottomY = this.Height - platformHeight - platformOffset;

            platform = new RoundedPlatform(bottomX, bottomY, platformWidth, platformHeight);
        }

        private void CheckCollisions()
        {
            if (ball.Left <= 0 || ball.Right >= this.Width)
            {
                ball.BounceVertical();
            }

            if (ball.Top <= 0)
            {
                ball.BounceHorizontal();
            }

            if (ball.Bounds.IntersectsWith(platform.Bounds) && ball.VelocityY > 0)
            {
                ball.BounceHorizontal();
            }

            for (int i = bricksPoint.Count - 1; i >= 0; i--)
            {
                var brick = bricksPoint[i];
                if (ball.Bounds.IntersectsWith(brick.Bounds))
                {
                    ball.BounceHorizontal();
                    this.Controls.Remove(brick);
                    bricksPoint.RemoveAt(i);
                    break;
                }
            }

            if (ball.Top >= this.Height)
            {
                gameTimer.Stop();
                if (MessageBox.Show("Game Over!") == DialogResult.OK)
                {
                    this.Close();
                }
            }

            if (bricksPoint.Count == 0)
            {
                gameTimer.Stop();
                MessageBox.Show("YOU WIN!", "Победа!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            if (gameStarted)
            {
                ball.BallMove();
                CheckCollisions();
            }
            this.Invalidate();
        }

        private void Game_form_MouseMove(object sender, MouseEventArgs e)
        {
            var newX = e.X - platform.Width / 2;

            if (newX < 0)
            {
                newX = 0;
            }
            if (newX > this.Width - platform.Width)
            {
                newX = this.Width - platform.Width;
            }

            platform.SetLocation(newX, platform.Location.Y);
            if (!gameStarted)
            {
                ball.Location = new Point(newX + platform.Width / 2 - ball.Width / 2, ball.Location.Y);
            }
        }

        /// <summary>
        /// Обработка пробела и esc
        /// </summary>
        private void Game_form_KeyDown(object sender, KeyEventArgs e)
        {
            var ballSpeed = 5;
            switch (e.KeyCode)
            {
                case Keys.Space:
                    if (!gameStarted)
                    {
                        gameStarted = true;
                        ball.VelocityX = ballSpeed;
                        ball.VelocityY = -ballSpeed;
                    }
                    break;

                case Keys.Escape:
                    gameTimer.Stop();
                    if (MessageBox.Show("Выйти из игры?", "Завершение игры", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        gameTimer.Start();
                    }
                    else
                    {
                        this.Close();
                    }
                    break;
            }
        }

        private void DrawBall(Graphics g, Ball ball)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.FillEllipse(Brushes.White, ball.Bounds);
            g.DrawEllipse(Pens.Black, ball.Bounds);
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
        public void DrawPlatform(Graphics g, RoundedPlatform platform)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            using (GraphicsPath path = CreateRoundedRectanglePath(
                platform.X,
                platform.Y,
                platform.Width,
                platform.Height,
                platform.CornerRadius))
            using (var brush = new SolidBrush(platform.Color))
            {
                g.FillPath(brush, path);
            }
        }
        private void GameForm_Paint(object sender, PaintEventArgs e)
        {
            DrawPlatform(e.Graphics, platform);
            DrawBall(e.Graphics, ball);
        }

        private void GameForm_ResizeEnd(object sender, EventArgs e)
        {
            var controlsLabel = this.Controls.Find("controlsLabel", false).FirstOrDefault();
            if (controlsLabel != null)
            {
                controlsLabel.Location = new Point(10, this.Height - 30);
            }
        }

        private void CreateControlsLabel()
        {
            Label controlsLabel = new Label
            {
                Text = "Управление: мышь - двигать платформу, ПРОБЕЛ - начать, ESC - выход",
                AutoSize = true,
                BackColor = Color.Transparent,
                Font = new Font("Arial", 9, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                Location = new Point(10, this.ClientSize.Height - 30),
                Name = "controlsLabel",
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left
            };
            this.Controls.Add(controlsLabel);
            controlsLabel.BringToFront();
        }
    }
}
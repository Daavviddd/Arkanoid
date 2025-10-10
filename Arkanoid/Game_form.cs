using Arkanoid.Clases;

namespace Arkanoid
{
    public partial class Game_form : Form
    {
        List<PictureBox> bricksPoint = new List<PictureBox>();
        RoundedPlatform platform;
        int numberOfBricks = 12;
        float brickWidth = 0;
        int brickHeight = 50;
        int platformWidth = 150;
        int platformHeight = 30;
        float brickX = 0, brickY = 0;
        /// <summary>
        /// флаг начала игры
        /// </summary>
        private bool gameStarted = false;
        private Ball ball;
        private System.Windows.Forms.Timer gameTimer;
        public Game_form()
        {
            InitializeComponent();
            this.KeyPreview = true;

            this.MouseMove += Game_form_MouseMove;
            this.KeyDown += Game_form_KeyDown;

            CreateControlsLabel();

            ball = new Ball(
                startX: this.Width / 2 - 10,
                startY: this.Height - 150,
                velocityX: 0,
                velocityY: 0,
                size: 20
            );

            this.Controls.Add(ball);
            ball.BringToFront();

            gameTimer = new System.Windows.Forms.Timer();
            gameTimer.Interval = 16;
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start(); 
        }
        /// <summary>
        /// Создание кирпичей
        /// </summary>
        private void MakingBricks()
        {
            brickWidth = this.Width / 13;
            Color[] colors = { Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.DarkBlue };
            for (int i = 0; i <= 4; i++)
            {
                for (int j = 0; j <= numberOfBricks; j++)
                {
                    brickX = 2 + j * (brickWidth + 5);
                    brickY = 2 + i * (brickHeight + 5);
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
            Platform();
            ball.Location = new Point(this.Width / 2 - 10, this.Height - 150);
        }

        /// <summary>
        /// генерация платформы
        /// </summary>
        private void Platform()
        {
            int bottomX, bottomY;
            bottomX = (this.Width - platformWidth) / 2;
            bottomY = this.Height - platformHeight - 100;
            platform = new RoundedPlatform();
            platform.Location = new Point(bottomX, bottomY);
            platform.Height = platformHeight;
            platform.Width = platformWidth;
            this.Controls.Add(platform);
            platform.BringToFront();
        }

        private void CheckCollisions()
        {
            if (ball.Left <= 0 || ball.Right >= this.Width)
                ball.BounceVertical();

            if (ball.Top <= 0)
                ball.BounceHorizontal();

            if (ball.Bounds.IntersectsWith(platform.Bounds))
                ball.BounceHorizontal();

            foreach (var brick in bricksPoint.ToList())
            {
                if (ball.Bounds.IntersectsWith(brick.Bounds))
                {
                    ball.BounceHorizontal();
                    this.Controls.Remove(brick);
                    bricksPoint.Remove(brick);
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
            if (platform != null)
            {
                int newX = e.X - platform.Width / 2;

                if (newX < 0) { newX = 0; }
                if (newX > this.Width - platform.Width)
                {
                    newX = this.Width - platform.Width;
                }


                platform.Location = new Point(newX, platform.Location.Y);
                if (!gameStarted && ball != null)
                {
                    ball.Location = new Point(newX + platform.Width / 2 - ball.Width / 2,
                                            ball.Location.Y);
                }
            }
        }
        /// <summary>
        /// Обработка пробела и esc
        /// </summary>
        private void Game_form_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    if (!gameStarted)
                    {
                        gameStarted = true;
                        ball.VelocityX = 5;
                        ball.VelocityY = -5;
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
        private void CreateControlsLabel()
        {
            Label controlsLabel = new Label
            {
                Text = "Управление: мышь - двигать платформу, ПРОБЕЛ - начать, ESC - выход",
                AutoSize = true,
                BackColor = Color.Transparent,
                Font = new Font("Arial", 9, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                Location = new Point(10, this.Height - 30),
                Name = "controlsLabel"
            };
            this.Controls.Add(controlsLabel);
            controlsLabel.BringToFront();
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            var controlsLabel = this.Controls.Find("controlsLabel", false).FirstOrDefault();
            if (controlsLabel != null)
            {
                controlsLabel.Location = new Point(10, this.Height - 30);
            }
        }
    }
}

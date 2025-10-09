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
        int speedPlatform = 10;
        private Ball ball;
        private System.Windows.Forms.Timer gameTimer;
        public Game_form()
        {
            InitializeComponent();
            this.BackColor = Color.SkyBlue;
            this.KeyPreview = true;

            CreateControlsLabel();

            ball = new Ball(
            startX: this.Width / 2 - 10,
            startY: this.Height - 150,
            velocityX: 5,
            velocityY: -5,
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

        private void Game_form_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void Game_form_Paint(object sender, PaintEventArgs e)
        {


        }

        private void Game_form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A || e.KeyCode == Keys.D)
            {
                int newX = platform.Location.X;
                switch (e.KeyCode)
                {
                    case Keys.A:
                        newX = newX - speedPlatform;
                        break;
                    case Keys.D:
                        newX = newX + speedPlatform;
                        break;
                }
                if(newX>this.Width-platform.Width)
                {
                    newX = this.Width - platform.Width;
                }
                if (newX < 0)
                {
                    newX = 0;
                }
                platform.Location = new Point(newX, platform.Location.Y);
            }
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
            }
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            ball.BallMove();
            CheckCollisions();
            this.Invalidate();
        }
        
    }
}

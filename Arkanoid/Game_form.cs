using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace Arkanoid
{
    public partial class Game_form : Form
    {
        List<PictureBox> bricksPoint = new List<PictureBox>();
        int numberOfBricks = 12;
        float brickWidth = 0;
        int brickHeight = 50;
        int platformWidth = 150;
        int platformHeight = 30;
        float brickX = 0, brickY = 0;
        public Game_form()
        {
            InitializeComponent();
            this.BackColor = Color.SkyBlue;

        }
        /// <summary>
        /// Создание кирпичей
        /// </summary>
        private void makingBricks()
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
            makingBricks();
            platform();
        }
        /// <summary>
        /// генерация платформы
        /// </summary>
        private void platform()
        {
            int bottomX, bottomY;
            bottomX = (this.Width - platformWidth) / 2;
            bottomY = this.Height - platformHeight - 100;
            PictureBox platform = new PictureBox();
            platform.Location = new Point(bottomX, bottomY);
            platform.BackColor = Color.Black;
            platform.Height = platformHeight;
            platform.Width = platformWidth;
            platform.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(platform);
        }

        private void Game_form_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }
    }
}

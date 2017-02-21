using System;
using System.Windows.Forms;


namespace Snake
{
    public partial class Form1 : Form
    {
        Snake snake = new Snake(70);
        public Form1()
        {
            InitializeComponent();
            
            
            snake.SnakeInit(gamePanel);
            
        }

        

        

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //xHelp.Text = snake.snakeLenght.ToString();
            snake.SnakeKeys(e.KeyCode);
        }

        

        private void mnuStart_Click(object sender, EventArgs e)
        {
            snake.StartGame(xHelp);
        }

        private void mnuCollision_Click(object sender, EventArgs e)
        {
            snake.Collisions = snake.Collisions == true ? false : true;


        }

        
    }
}

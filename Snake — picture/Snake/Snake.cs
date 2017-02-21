using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows.Forms;

namespace Snake
{
    class Snake: SnakeFood
    {
        public static int points = 0;
        ToolStripMenuItem pointsMenu;
        public Image[] snakeImage = new Image[2];
        //public Image helpbutt1 = new PictureBox { Image = System.Drawing.Image.FromFile("snake-graph\\s3.png") };
        Timer snakeTimer = new Timer { Interval = 50, Enabled = false };
        bool _collisions = true;
        public bool Collisions
        {
            get { return _collisions; }
            set { _collisions = value;}
        }

        int last = 0;
        int _plus = 0;
        public int Plus {
            get
            {
                return _plus;
            }
            set
            {
                _plus = value;
            }
        }
        public int snakeLenght;
        Panel xPanel;
        enum SnakeDirect
        {
            left = 0,
            rigt = 1,
            up = 2,
            down = 3

        }

        SnakeDirect snakeDirect = SnakeDirect.up;
        // Snake PictureBox init
        ObservableCollection<PictureBox> xbutton = new ObservableCollection<PictureBox>();
        protected static ObservableCollection<PictureBox> XPictureBoxRef = new ObservableCollection<PictureBox>();
        
        
        private int lastDirect = 2; 
        private int sizeOfDot=20; // size of one piece of Snake

        /// <summary>
        /// It makes Snake longer
        /// </summary>
        /// <param name="snakePlus"></param>
        private void SnakeAdd(int snakePlus)
        {
           
            if (snakePlus != 0)
            {
                PictureBox btn = new PictureBox { Tag = snakeLenght - 1 };
                btn.BackColor = System.Drawing.Color.Transparent;
                btn.Location = new System.Drawing.Point(-50, -50);
                btn.Visible = true;
                //btn.BackColor = System.Drawing.Color.Indigo;

                
                btn.Image = snakeImage[0];

                btn.SizeMode = PictureBoxSizeMode.StretchImage;
                //btn.FlatStyle = FlatStyle.Flat;
                //btn.FlatAppearance.BorderSize = 0;
                btn.Text = "";
                btn.Size = new System.Drawing.Size(sizeOfDot, sizeOfDot);
                xbutton.Add(btn);
                xPanel.Controls.Add(xbutton[xbutton.Count-1]);
                snakeLenght += 1;
                Plus -= 1;
            }

        }
        /// <summary>
        /// Costructor of Snake class
        /// </summary>
        /// <param name="snakeInterval"></param>
        public Snake(int snakeInterval)
        {
            FoodInit();
            for (int ii = 0; ii < 2; ii++) snakeImage[ii] = Image.FromFile("../../snake-graph/s"+ii.ToString()+".png");
            snakeTimer.Tick += SnakeTimer_Tick;
            snakeTimer.Interval = snakeInterval;
            snakeLenght = 5;
            for (int x = 0; x < snakeLenght; x++) xbutton.Add(new PictureBox { Tag = x });
            XPictureBoxRef = xbutton;
            

        }
        /// <summary>
        /// Change direction 
        /// </summary>
        /// <param name="x"></param>
        public void SnakeKeys(Keys x)
        {
            

            switch (x)
            {
                case Keys.W:

                    if (lastDirect != 3) snakeDirect = SnakeDirect.up;
                    break;
                case Keys.S:
                    if (lastDirect != 2) snakeDirect = SnakeDirect.down;
                    break;
                case Keys.A:
                    if (lastDirect != 1) snakeDirect = SnakeDirect.left;
                    break;
                case Keys.D:
                    if (lastDirect != 0) snakeDirect = SnakeDirect.rigt;
                    break;
            }

        }

        /// <summary>
        /// Snake collision
        /// </summary>
        private void SnakeCollision()
        {
            for(int x=1;x<xbutton.Count-1;x++)
            {
                if (xbutton[x].Location == xbutton[0].Location)
                {
                    snakeTimer.Enabled = false;
                    MessageBox.Show("Koniec");
                }
            }
            if (xbutton[0].Left == xPanel.Width) xbutton[0].Left = 0;
            if (xbutton[0].Left+20 == 0) xbutton[0].Left = xPanel.Width - 20;
            if (xbutton[0].Top == xPanel.Height) xbutton[0].Top = 0;
            if (xbutton[0].Top+20== 0) xbutton[0].Top = xPanel.Height -20;

        }

        /// <summary>
        /// Snake eat :)
        /// </summary>
        private void snakeEat()
        {
            if (Food[0].Location == xbutton[0].Location)
            {
                Plus += (int)Food[0].Tag;
                points += (int)Food[0].Tag;
                pointsMenu.Text = "Punkty: " + points.ToString();
                Food[0].Dispose();
                Food.RemoveAt(0);
                FoodGenerate(xPanel,1);
            }
        }




        /// <summary>
        /// Snake initiation in Panel control
        /// </summary>
        /// <param name="sPanel"></param>
        public void SnakeInit(Panel sPanel)
        {
            int x, y;
            x = 340; y = 320;
            xPanel = sPanel;
            FoodGenerate(sPanel,1);
            foreach (var btn in xbutton)
            {
                //sPanel.Controls.Add(btn);
                xPanel.Controls.Add(btn);
                btn.Visible = true;
                //btn.BackColor = System.Drawing.Color.Indigo;

                btn.BackColor = System.Drawing.Color.Transparent;
                btn.SizeMode = PictureBoxSizeMode.StretchImage;
                btn.Image = snakeImage[0];

                //btn.FlatStyle = FlatStyle.Flat;
                //btn.FlatAppearance.BorderSize = 0;
                btn.Text = "";
                btn.Size = new System.Drawing.Size(sizeOfDot, sizeOfDot);
                btn.Location = new System.Drawing.Point(x, y);
                y += sizeOfDot;
            }
        }
        /// <summary>
        /// Game starter
        /// </summary>
        public void StartGame(ToolStripMenuItem mnuPoints)
        {
            pointsMenu = mnuPoints;
            snakeTimer.Enabled = (snakeTimer.Enabled ==true) ? false : true; 
            
        }


        /// <summary>
        /// Snake timer for moving
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SnakeTimer_Tick(object sender, EventArgs e)
        {
            SnakeAdd(Plus);
            last = xbutton.Count - 1;
            xbutton[last].Visible = false;
            switch (snakeDirect)
            {
                case SnakeDirect.left:
                    if (lastDirect != 1)
                    {
                        
                        xbutton[last].Left = xbutton[0].Left - sizeOfDot;
                        xbutton[last].Top = xbutton[0].Top;
                        xbutton[last].Visible = true;
                        xbutton.Move(last, 0); 

                        lastDirect = 0;
                    }
                    break;
                case SnakeDirect.rigt:
                    if (lastDirect != 0)
                    {
                        //xbutton[last].Visible = false;
                        xbutton[last].Left = xbutton[0].Left + sizeOfDot;
                        xbutton[last].Top = xbutton[0].Top;
                        xbutton[last].Visible = true;
                        xbutton.Move(last, 0);
                        lastDirect = 1; 
                    }
                    break;
                case SnakeDirect.up:
                    if (lastDirect != 3)
                    {
                       // xbutton[last].Visible = false;
                        xbutton[last].Left = xbutton[0].Left;
                        xbutton[last].Top = xbutton[0].Top - sizeOfDot;
                        xbutton[last].Visible = true;
                        xbutton.Move(last, 0); 
                        lastDirect = 2;
                    }
                    break;
                case SnakeDirect.down:
                    if (lastDirect != 2)
                    {
                        //xbutton[last].Visible = false;
                        xbutton[last].Left = xbutton[0].Left;
                        xbutton[last].Top = xbutton[0].Top + sizeOfDot;
                        xbutton[last].Visible = true;
                        xbutton.Move(last, 0);
                        lastDirect = 3;
                    }
                    break;
                
            }
            // xbutton[1].BackColor = System.Drawing.Color.Indigo;
            //  xbutton[0].BackColor = System.Drawing.Color.Green;
            xbutton[1].Image = snakeImage[0];
            xbutton[0].Image = snakeImage[1]; 
            xbutton[0].BringToFront();
            snakeEat();
           if(Collisions) SnakeCollision();
        }
    }
}

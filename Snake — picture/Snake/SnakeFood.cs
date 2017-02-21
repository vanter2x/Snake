using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Snake
{
    class SnakeFood
    {
        Image[] foodImage = new Image[3];
        List<PictureBox> _food = new List<PictureBox>();
        //bool _foodOnSnake = true;
        Random rndPosition = new Random();
        //private int Plus = 0;

        //public bool FoodOnSnake
        //{
        //    get
        //    {
        //        return _foodOnSnake;
        //    }
        //    set
        //    {
        //        _foodOnSnake = value;
        //    }
        //}
        protected List<PictureBox> Food
        {
            get { return _food; }
            set { _food = value; }
        }

        protected void FoodInit()
        {
            for (int ii = 0; ii < 3; ii++) foodImage[ii] = Image.FromFile("../../snake-graph/food" + ii + ".png");
        }


        private void foodSetup(PictureBox picFood)
        {
            picFood.Visible = true;

            picFood.Size = new Size(20, 20);
            picFood.SizeMode = PictureBoxSizeMode.StretchImage;
            picFood.BackColor = Color.Transparent;
            if (Snake.points > 0 && Snake.points % 15 == 0)
            {
                picFood.Image = foodImage[1];
                picFood.Tag = 5;
            }
            else
            {
                picFood.Image = foodImage[0];
                picFood.Tag = 1;
            }
        }
        
        protected void FoodGenerate(Panel snakePanel,int typeOfFood)
        {

           
            Point snakeFood;

            
            snakeFood = new Point((rndPosition.Next(1, 39))*20, (rndPosition.Next(1, 29))*20);

            Food.Add(new PictureBox());
            Food[0].Location = snakeFood;

            foodSetup(Food[0]);
            snakePanel.Controls.Add(Food[0]);

                
            

        }
    }
}

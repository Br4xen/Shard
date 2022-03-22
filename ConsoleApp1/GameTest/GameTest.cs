using GameTest;
using System;
using System.Drawing;

namespace Shard
{
    class GameTest : Game, InputListener
    {
        GameObject background;
        public override void update()
        {
            //            Bootstrap.getDisplay().addToDraw(background);

            Bootstrap.getDisplay().showText("FPS: " + Bootstrap.getFPS(), 10, 10, 12, 255, 255, 255);


        }

        public void createShip()
        {
            GameObject ship = new Spaceship();
            GameObject car = new Car();
            Random rand = new Random();
            int offsetx = 0, offsety = 0;

            //GameObject asteroid;


            //asteroid = new Asteroid();
            //asteroid.Transform.Z = 1;
            //asteroid.Transform.translate(500 + 100, 500);
//            asteroid.MyBody.Kinematic = true;
     

            /*
            background = new GameObject();
            background.Transform.SpritePath = "background2.jpg";
            background.Transform.X = 0;
            background.Transform.Y = 0;
            */
            
            GameObject wall = new Wall();
            wall.Transform.SpritePath = "wall.png";
            wall.Transform.X = 0;
            wall.Transform.Y = 0;
            wall.Transform.translate(976, 0);
            wall.Transform.IsBackground = false;

        }

        public override void initialize()
        {
            City c = new City();
            //Car car = new Car();
            Bootstrap.getInput().addListener(this);
            createShip();
        }

        public void handleInput(InputEvent inp, string eventType)
        {


            if (eventType == "MouseDown" && inp.Button == 1)
            {
                Asteroid asteroid = new Asteroid();
                asteroid.Transform.X = inp.X;
                asteroid.Transform.Y = inp.Y;
            }



        }
    }
}

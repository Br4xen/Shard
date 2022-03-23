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
            ColliderHolder c1 = new ColliderHolder();
            c1.addCollider(0, 230, 632, 632);
            ColliderHolder c2 = new ColliderHolder();
            c2.addCollider(245, 400, 632, 450);
            ColliderHolder c3 = new ColliderHolder();
            c3.addCollider(400, 586, 450, 450);
            ColliderHolder c4 = new ColliderHolder();
            c4.addCollider(586, 740, 450, 263);
            ColliderHolder c5 = new ColliderHolder();
            c5.addCollider(740, 1225, 263, 263);
            ColliderHolder c6 = new ColliderHolder();
            c6.addCollider(1225, 977, 263, 756);
            ColliderHolder c7 = new ColliderHolder();
            c7.addCollider(977, 1300, 756, 756);

            ColliderHolder worldborder1 = new ColliderHolder();
            worldborder1.addCollider(0, 0, 637, 863);
            ColliderHolder worldborder2 = new ColliderHolder();
            worldborder2.addCollider(0, 1276, 863, 863);
            ColliderHolder worldborder3 = new ColliderHolder();
            worldborder3.addCollider(1276, 1276, 863, 761);

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
                Debug.getInstance().log("X,Y: " + inp.X + "," + inp.Y);
            }



        }
    }
}

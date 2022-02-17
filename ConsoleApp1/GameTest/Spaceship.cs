using SDL2;
using Shard;
using System.Drawing;

namespace GameTest
{
    class Spaceship : GameObject, InputListener, CollisionHandler
    {
        bool up, down, turnLeft, turnRight;
        private double speed;


        public override void initialize()
        {

            this.Transform.X = 500.0f;
            this.Transform.Y = 500.0f;
            this.Transform.Z = 2.0f;
            this.Transform.SpritePath = "car.png";


            Bootstrap.getInput().addListener(this);

            up = false;
            down = false;
            speed = 1;

            setPhysicsEnabled();

            MyBody.Mass = 4;
            MyBody.MaxForce = 10;
            MyBody.AngularDrag = 0.01f;
            MyBody.Drag = 0f;
            MyBody.UsesGravity = false;
            MyBody.StopOnCollision = false;
            MyBody.ReflectOnCollision = false;
            MyBody.ImpartForce = true;


            MyBody.PassThrough = true;
            MyBody.addCircleCollider(0, 0, 5);
            MyBody.addCircleCollider(0, 34, 5);
            MyBody.addCircleCollider(60, 18, 5);
       //     MyBody.addCircleCollider();

            addTag("Spaceship");


        }

        public void fireBullet()
        {
            Bullet b = new Bullet();

            b.setupBullet(this, this.Transform.Centre.X, this.Transform.Centre.Y);

            b.Transform.rotate(this.Transform.Rotz);
        }

        public void handleInput(InputEvent inp, string eventType)
        {



            if (eventType == "KeyDown")
            {
                if (inp.Key == (int)SDL.SDL_Scancode.SDL_SCANCODE_W)
                {
                    up = true;
                }

                if (inp.Key == (int)SDL.SDL_Scancode.SDL_SCANCODE_S)
                {
                    down = true;
                }

                if (inp.Key == (int)SDL.SDL_Scancode.SDL_SCANCODE_D)
                {
                    turnRight = true;
                }

                if (inp.Key == (int)SDL.SDL_Scancode.SDL_SCANCODE_A)
                {
                    turnLeft = true;
                }

            }
            else if (eventType == "KeyUp")
            {
                if (inp.Key == (int)SDL.SDL_Scancode.SDL_SCANCODE_W)
                {
                    up = false;
                }

                if (inp.Key == (int)SDL.SDL_Scancode.SDL_SCANCODE_S)
                {
                    down = false;
                }

                if (inp.Key == (int)SDL.SDL_Scancode.SDL_SCANCODE_D)
                {
                    turnRight = false;
                }

                if (inp.Key == (int)SDL.SDL_Scancode.SDL_SCANCODE_A)
                {
                    turnLeft = false;
                }


            }



            if (eventType == "KeyUp")
            {
                if (inp.Key == (int)SDL.SDL_Scancode.SDL_SCANCODE_SPACE)
                {
                    fireBullet();
                }
            }
        }

        public override void physicsUpdate()
        {

            if (turnLeft)
            {
                this.Transform.translate(-1 * 3 * (1 + 3 * (float)this.Transform.Y / Bootstrap.getDisplay().getHeight()), 0);
            }

            if (turnRight)
            {
                this.Transform.translate(1 * 3 * (1 + 3 * (float)this.Transform.Y / Bootstrap.getDisplay().getHeight()), 0);
            }

            if (up)
            {

                this.Transform.translate(0, -1 * 3 * (1 + 3 * (float)this.Transform.Y / Bootstrap.getDisplay().getHeight()));
            }

            if (down)
            {
                this.Transform.translate(0, 1 * 3 * (1 + 3 * (float)this.Transform.Y / Bootstrap.getDisplay().getHeight()));
            }


        }

        public override void update()
        {
            Bootstrap.getDisplay().addToDraw(this);
        }

        public void onCollisionEnter(PhysicsBody x)
        {
            if (x.Parent.checkTag("Bullet") == false)
            {
                MyBody.DebugColor = Color.Red;
            }
        }

        public void onCollisionExit(PhysicsBody x)
        {

            MyBody.DebugColor = Color.Green;
        }

        public void onCollisionStay(PhysicsBody x)
        {
            MyBody.DebugColor = Color.Blue;
        }

        public override string ToString()
        {
            return "Spaceship: [" + Transform.X + ", " + Transform.Y + ", " + Transform.Wid + ", " + Transform.Ht + "]";
        }

    }
}

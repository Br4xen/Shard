using Shard;
using System.Drawing;

namespace GameTest
{
    class Car : GameObject, CollisionHandler
    {

        public override void initialize()
        {
            this.Transform.X = 200.0f;
            this.Transform.Y = 500.0f;
            this.Transform.Z = 2.0f;
            this.Transform.SpritePath = "car.png";

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
            MyBody.addRectCollider(0, 50, 102, 74);

            //MyBody.addCircleCollider(64, 64, 64);

            //addTag("Background");

            //MyBody.Kinematic = true;

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
            return "Car: [" + Transform.X + ", " + Transform.Y + "]";
        }


    }
}

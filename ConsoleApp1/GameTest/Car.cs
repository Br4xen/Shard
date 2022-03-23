using Shard;
using System.Drawing;

namespace GameTest
{
    class Car : GameObject, CollisionHandler
    {

        public override void initialize()
        {
            this.Transform.X = 300.0f;
            this.Transform.Y = 500.0f;
            this.Transform.Z = 2.0f;
            this.Transform.SpritePath = "car.png";

            setPhysicsEnabled();

            MyBody.MaxTorque = 100;
            MyBody.Mass = 4;
            MyBody.AngularDrag = 0.1f;
            MyBody.MaxForce = 100;
            MyBody.StopOnCollision = true;
            MyBody.Kinematic = false;
            MyBody.ImpartForce = true;
            MyBody.ReflectOnCollision = false;
            MyBody.addRectCollider(0, 53, 85, 10);

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

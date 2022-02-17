using Shard;

namespace GameTest
{
    class Car : GameObject, CollisionHandler
    {

        public override void initialize()
        {
            this.Transform.SpritePath = "car.png";

            //setPhysicsEnabled();

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

        }

        public void onCollisionExit(PhysicsBody x)
        {
        }

        public void onCollisionStay(PhysicsBody x)
        {
        }

        public override string ToString()
        {
            return "Car: [" + Transform.X + ", " + Transform.Y + "]";
        }


    }
}

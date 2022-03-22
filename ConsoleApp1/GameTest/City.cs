using Shard;

namespace GameTest
{
    class City : GameObject, CollisionHandler
    {

        public override void initialize()
        {
            this.Transform.SpritePath = "perspective_colour-without-wall.png";
            this.Transform.IsBackground = true;

            setPhysicsEnabled();

            MyBody.Kinematic = true;

            MyBody.addLineCollider(586, 740, 450, 263);

            //MyBody.addCircleCollider(64, 64, 64);

            addTag("Background");

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
            return "City: [" + Transform.X + ", " + Transform.Y + "]";
        }


    }
}

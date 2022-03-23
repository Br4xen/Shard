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



            //MyBody.addLineCollider(0, 230, 630, 634);
            //MyBody.addLineCollider(245, 400, 632, 450);
            //MyBody.addLineCollider(400, 586, 450, 450);
            //MyBody.addLineCollider(586, 740, 450, 263);
            //MyBody.addLineCollider(740, 1225, 263, 263);
            //MyBody.addLineCollider(1225, 977, 263, 756);

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

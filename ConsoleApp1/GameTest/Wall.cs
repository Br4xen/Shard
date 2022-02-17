using Shard;

namespace GameTest
{
    class Wall : GameObject, CollisionHandler
    {

        public override void initialize()
        {
            this.Transform.SpritePath = "wall.png";
            this.Transform.IsBackground = false;

            //setPhysicsEnabled();

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

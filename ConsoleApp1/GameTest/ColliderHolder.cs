using Shard;

namespace GameTest
{
    class ColliderHolder : GameObject, CollisionHandler
    {

        public override void initialize()
        {
            //this.Transform.SpritePath = "";
            //this.Transform.IsBackground = true;

            setPhysicsEnabled();

            MyBody.Kinematic = true;



            //MyBody.addLineCollider(0, 230, 630, 634);
            //MyBody.addLineCollider(245, 400, 632, 450);
            //MyBody.addLineCollider(400, 586, 450, 450);
            //MyBody.addLineCollider(586, 740, 450, 263);
            //MyBody.addLineCollider(740, 1225, 263, 263);
            //MyBody.addLineCollider(1225, 977, 263, 756);

            //MyBody.addCircleCollider(64, 64, 64);

            //addTag("Background");

            //MyBody.Kinematic = true;

        }

        public void addCollider(int x1, int x2, int y1, int y2)
        {
            MyBody.addLineCollider(x1, x2, y1, y2);
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
            return "ColliderHolder: [" + Transform.X + ", " + Transform.Y + "]";
        }


    }
}

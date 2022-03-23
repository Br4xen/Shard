/*
*
*   The specific collider for rectangles.   Handles rect/circle, rect/rect and rect/vector.
*   @author Michael Heron
*   @version 1.0
*   
*/

using System;
using System.Drawing;
using System.Numerics;

namespace Shard
{
    class ColliderLine : Collider
    {
        //private Transform myRect;
        //private float baseWid, baseHt;
        private float x1, x2, y1, y2, wid, ht;
        //private bool fromTrans;


        /*
        public ColliderLine(CollisionHandler gob) : base(gob)
        {

            //this.MyRect = t;
            //fromTrans = true;
            //RotateAtOffset = false;
            //calculateBoundingBox();
        }
        */

        public ColliderLine(CollisionHandler gob, float x1, float x2, float y1, float y2) : base(gob)
        {
            X1 = x1;
            X2 = x2;
            Y1 = y1;
            Y2 = y2;
            //BaseWid = wid;
            //BaseHt = ht;
            //RotateAtOffset = true;

            //this.MyRect = t;

            //fromTrans = false;

        }

        public void calculateBoundingBox()
        {
            MinAndMaxX[0] = Math.Min(X1, X2);
            MinAndMaxX[1] = Math.Max(X1, X2);
            MinAndMaxY[0] = Math.Min(Y1, Y2);
            MinAndMaxY[1] = Math.Max(Y1, Y2);
        }

        //        internal Transform MyRect { get => myRect; set => myRect = value; }
        public float X1 { get => x1; set => x1 = value; }
        public float X2 { get => x2; set => x2 = value; }
        public float Y1 { get => y1; set => y1 = value; }
        public float Y2 { get => y2; set => y2 = value; }
        /*      public float Wid { get => wid; set => wid = value; }
                public float Ht { get => ht; set => ht = value; } */
        public float Left { get => MinAndMaxX[0]; set => MinAndMaxX[0] = value; }
        public float Right { get => MinAndMaxX[1]; set => MinAndMaxX[1] = value; }
        public float Top { get => MinAndMaxY[0]; set => MinAndMaxY[0] = value; }
        public float Bottom { get => MinAndMaxY[1]; set => MinAndMaxY[1] = value; }
        /*        public float BaseWid { get => baseWid; set => baseWid = value; }
                public float BaseHt { get => baseHt; set => baseHt = value; } */

        public override void recalculate()
        {
            calculateBoundingBox();
        }

        /*
        public ColliderRect calculateMinkowskiDifference(ColliderRect other)
        {
            float left, right, top, bottom, width, height;
            ColliderRect mink = new ColliderRect(null, null);

            // A set of calculations that gives us the Minkowski difference
            // for this intersection.
            left = Left - other.Right;
            top = other.Top - Bottom;
            width = Wid + other.Wid;
            height = Ht + other.Ht;
            right = Right - other.Left;
            bottom = other.Bottom - Top;

            mink.Wid = width;
            mink.Ht = height;

            mink.MinAndMaxX = new float[2] { left, right };
            mink.MinAndMaxY = new float[2] { top, bottom };

            return mink;
        }

        public Vector2? calculatePenetration(Vector2 checkPoint)
        {
            Vector2? impulse;
            int coff = 0;

            // Check the right edge
            float min;

            min = Math.Abs(Right - checkPoint.X);
            impulse = new Vector2(-1 * min - coff, checkPoint.Y);


            // Now compare against the Left edge
            if (Math.Abs(checkPoint.X - Left) < min)
            {
                min = Math.Abs(checkPoint.X - Left);
                impulse = new Vector2(min + coff, checkPoint.Y);
            }

            // Now the bottom
            if (Math.Abs(Bottom - checkPoint.Y) < min)
            {
                min = Math.Abs(Bottom - checkPoint.Y);
                impulse = new Vector2(checkPoint.X, min + coff);
            }

            // And now the top
            if (Math.Abs(Top - checkPoint.Y) < min)
            {
                min = Math.Abs(Top - checkPoint.Y);
                impulse = new Vector2(checkPoint.X, -1 * min - coff);
            }

            return impulse;
        }
        */

        public override Vector2? checkCollision(ColliderRect other)
        {
            /*
            ColliderRect cr;

            cr = calculateMinkowskiDifference(other);

            if (cr.Left <= 0 && cr.Right >= 0 && cr.Top <= 0 && cr.Bottom >= 0)
            {
                return cr.calculatePenetration(new Vector2(0, 0));
            } */



            return other.checkCollision(this);

        }

        public override void drawMe(Color col)
        {
            Display d = Bootstrap.getDisplay();
            /*
            d.drawLine((int)MinAndMaxX[0], (int)MinAndMaxY[0], (int)MinAndMaxX[1], (int)MinAndMaxY[0], Color.Purple);
            d.drawLine((int)MinAndMaxX[0], (int)MinAndMaxY[0], (int)MinAndMaxX[0], (int)MinAndMaxY[1], Color.Purple);
            d.drawLine((int)MinAndMaxX[1], (int)MinAndMaxY[0], (int)MinAndMaxX[1], (int)MinAndMaxY[1], Color.Purple);
            d.drawLine((int)MinAndMaxX[0], (int)MinAndMaxY[1], (int)MinAndMaxX[1], (int)MinAndMaxY[1], Color.Purple);
            */

            d.drawLine((int)X1, (int)Y1, (int)X2, (int)Y2, col);
        }

        public override Vector2? checkCollision(ColliderCircle c)
        {
            Vector2? possibleV = c.checkCollision(this);

            if (possibleV is Vector2 v)
            {
                v.X *= -1;
                v.Y *= -1;
                return v;
            }

            return null;
        }

        public override float[] getMinAndMaxX()
        {
            return MinAndMaxX;
        }

        public override float[] getMinAndMaxY()
        {
            return MinAndMaxY;
        }

        public override Vector2? checkCollision(Vector2 other)
        {

            if (other.X >= Left &&
                other.X <= Right &&
                other.Y >= Top &&
                other.Y <= Bottom)
            {
                return new Vector2(0, 0);
            }

            return null;
        }

        public override Vector2? checkCollision(ColliderLine c)
        {
            return null;
        }
    }


}

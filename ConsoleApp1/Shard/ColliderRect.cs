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
    class ColliderRect : Collider
    {
        private Transform myRect;
        private float baseWid, baseHt;
        private float x, y, wid, ht, xOffset, yOffset;
        private bool fromTrans;



        public ColliderRect(CollisionHandler gob, Transform t) : base(gob)
        {

            this.MyRect = t;
            fromTrans = true;
            calculateBoundingBox();
        }

        public ColliderRect(CollisionHandler gob, Transform t, float x, float y, float wid, float ht) : base(gob)
        {
            X = x;
            Y = y;
            xOffset = x;
            yOffset = y;
            BaseWid = wid;
            BaseHt = ht;

            this.MyRect = t;

            fromTrans = false;

        }

        private float getScaleFactor()
        {
            return 1 + 3 * (float)myRect.Y / Bootstrap.getDisplay().getHeight();
        }

        public void calculateBoundingBox()
        {
            float nwid, nht, angle, x1, x2, y1, y2;
            double cos, sin;
            if (myRect == null)
            {
                return;
            }

            if (fromTrans)
            {
                Wid = (float)(BaseWid * getScaleFactor());
                Ht = (float)(BaseHt * getScaleFactor());
            }
            else
            {
                Wid = (float)(BaseWid * getScaleFactor());
                Ht = (float)(BaseHt * getScaleFactor());
            }
            /*else
            {
                Wid = (float)(BaseWid * MyRect.Scalex);
                Ht = (float)(BaseHt * MyRect.Scaley);
            }
            */
            angle = (float)(Math.PI * MyRect.Rotz / 180.0f);


            cos = Math.Cos(angle);
            sin = Math.Sin(angle);

            // Bit of trig here to calculate the new height and width
            nwid = (float)(Math.Abs(Wid * cos) + Math.Abs(Ht * sin));
            nht = (float)(Math.Abs(Wid * sin) + Math.Abs(Ht * cos));

            X = (float)MyRect.X + (Wid / 2) + xOffset * getScaleFactor();
            Y = (float)MyRect.Y + (Ht / 2) + yOffset * getScaleFactor();

            Wid = nwid;
            Ht = nht;


            // Now we work out the X and Y based on the rotation of the body to 
            // which this belongs,.
            x1 = X - MyRect.Centre.X;
            y1 = Y - MyRect.Centre.Y;

            x2 = (float)(x1 * Math.Cos(angle) - y1 * Math.Sin(angle));
            y2 = (float)(x1 * Math.Sin(angle) + y1 * Math.Cos(angle));

            X = x2 + (float)MyRect.Centre.X;
            Y = y2 + (float)MyRect.Centre.Y;


            MinAndMaxX[0] = X - Wid / 2;
            MinAndMaxX[1] = X + Wid / 2;
            MinAndMaxY[0] = Y - Ht / 2;
            MinAndMaxY[1] = Y + Ht / 2;


        }

        internal Transform MyRect { get => myRect; set => myRect = value; }
        public float X { get => x; set => x = value; }
        public float Y { get => y; set => y = value; }
        public float Wid { get => wid; set => wid = value; }
        public float Ht { get => ht; set => ht = value; }
        public float Left { get => MinAndMaxX[0]; set => MinAndMaxX[0] = value; }
        public float Right { get => MinAndMaxX[1]; set => MinAndMaxX[1] = value; }
        public float Top { get => MinAndMaxY[0]; set => MinAndMaxY[0] = value; }
        public float Bottom { get => MinAndMaxY[1]; set => MinAndMaxY[1] = value; }
        public float BaseWid { get => baseWid; set => baseWid = value; }
        public float BaseHt { get => baseHt; set => baseHt = value; }

        public override void recalculate()
        {
            calculateBoundingBox();
        }

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

        public override Vector2? checkCollision(ColliderRect other)
        {
            ColliderRect cr;

            cr = calculateMinkowskiDifference(other);

            if (cr.Left <= 0 && cr.Right >= 0 && cr.Top <= 0 && cr.Bottom >= 0)
            {
                return cr.calculatePenetration(new Vector2(0, 0));
            }



            return null;

        }

        public override void drawMe(Color col)
        {
            Display d = Bootstrap.getDisplay();

            d.drawLine((int)MinAndMaxX[0], (int)MinAndMaxY[0], (int)MinAndMaxX[1], (int)MinAndMaxY[0], col);
            d.drawLine((int)MinAndMaxX[0], (int)MinAndMaxY[0], (int)MinAndMaxX[0], (int)MinAndMaxY[1], col);
            d.drawLine((int)MinAndMaxX[1], (int)MinAndMaxY[0], (int)MinAndMaxX[1], (int)MinAndMaxY[1], col);
            d.drawLine((int)MinAndMaxX[0], (int)MinAndMaxY[1], (int)MinAndMaxX[1], (int)MinAndMaxY[1], col);

            d.drawCircle((int)X, (int)Y, 2, col);
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

        private Vector2 PerpendicularClockwise(Vector2 vector2)
        {
            return new Vector2(vector2.Y, -vector2.X);
        }

        private Vector2 PerpendicularCounterClockwise(Vector2 vector2)
        {
            return new Vector2(-vector2.Y, vector2.X);
        }

        public static float GetY(Vector2 point1, Vector2 point2, float x)
        {
            var m = (point2.Y - point1.Y) / (point2.X - point1.X);
            var b = point1.Y - (m * point1.X);

            return m * x + b;
        }

        private Vector2? CalculatePenetration(ColliderLine c, float firstX, float firstY, float secondX, float secondY)
        {
            Vector2? impulse;

            var m = (c.Y2 - c.Y1) / (c.X2 - c.X1 - 0.00000001f);
            var b = c.Y1 - (m * c.X1);

            var mPerpendicular = -1 / m;

            var firstB = firstY - mPerpendicular * firstX;

            var firstIntersectionX = (firstB - b) / (m - mPerpendicular + 0.00000001f);
            var firstIntersectionY = m * firstIntersectionX + b;

            var firstDistance = Math.Sqrt(Math.Pow(firstX - firstIntersectionX, 2) + Math.Pow(firstY - firstIntersectionY, 2));
            Debug.getInstance().log("Intersection: (" + firstIntersectionX + ", " + firstIntersectionY + ")" + " Right: (" + Right + ", " + Top + ") Distance: " + firstDistance);

            var secondB = secondY - mPerpendicular * secondX;

            var secondIntersectionX = (secondB - b) / (m - mPerpendicular + 0.00000001f);
            var secondIntersectionY = m * secondIntersectionX + b;

            var secondDistance = Math.Sqrt(Math.Pow(secondX - secondIntersectionX, 2) + Math.Pow(secondY - secondIntersectionY, 2));
            Debug.getInstance().log("Intersection: (" + secondIntersectionX + ", " + secondIntersectionY + ")" + " Right: (" + Right + ", " + Top + ") Distance: " + secondDistance);

            if (Math.Max(firstDistance, secondDistance) == firstDistance)
            {
                impulse = new Vector2(firstIntersectionX - firstX, firstIntersectionY - firstY);
            }
            else
            {
                impulse = new Vector2(secondIntersectionX - secondX, secondIntersectionY - secondY);
            }

            return impulse;
        }
        private Vector2? CalculatePenetration(ColliderLine c, float firstX, float firstY)
        {
            Vector2? impulse;

            var m = (c.Y2 - c.Y1) / (c.X2 - c.X1 + 0.000000001f);
            var b = c.Y1 - (m * c.X1);

            var mPerpendicular = -1 / m;

            var firstB = firstY - mPerpendicular * firstX;

            var firstIntersectionX = (firstB - b) / (m - mPerpendicular + 0.00000001f);
            var firstIntersectionY = m * firstIntersectionX + b;

            var firstDistance = Math.Sqrt(Math.Pow(firstX - firstIntersectionX, 2) + Math.Pow(firstY - firstIntersectionY, 2));
            //Debug.getInstance().log("Intersection: (" + firstIntersectionX + ", " + firstIntersectionY + ")" + " Right: (" + Right + ", " + Top + ") Distance: " + firstDistance);

            impulse = new Vector2(firstIntersectionX - firstX, firstIntersectionY - firstY);

            return impulse;
        }

        public override Vector2? checkCollision(ColliderLine c)
        {

            if (!(Bottom > c.Top && Top < c.Bottom))
            {
                return null;
            }

            if (c.Y1 == c.Y2)
            {
                float[] distances = new float[4];
                float rightDistance = Math.Abs(c.Right - Left);
                float leftDistance = Math.Abs(c.Left - Right);
                float topDistance = Math.Abs(c.Top - Bottom);
                float bottomDistance = Math.Abs(c.Bottom - Top);

                distances[0] = rightDistance;
                distances[1] = leftDistance;
                distances[2] = topDistance;
                distances[3] = bottomDistance;

                float min = float.MaxValue;
                foreach (float f in distances)
                {
                    if (f < min)
                    {
                        min = f;
                    }
                }
                //Debug.getInstance().log("                              " + c.X1 + ", " + c.X2 + " .  " + c.Y1 + ", " + c.Y2);
                //Debug.getInstance().log("Min" + min);
                if (min == topDistance)
                {
                    return new Vector2(0, -topDistance);
                }
                else if (min == bottomDistance)
                {
                    return new Vector2(0, bottomDistance);
                }



            }
            if (c.X1 == c.X2)
            {
                float[] distances = new float[4];
                float rightDistance = Math.Abs(c.Right - Left);
                float leftDistance = Math.Abs(c.Left - Right);
                float topDistance = Math.Abs(c.Top - Bottom);
                float bottomDistance = Math.Abs(c.Bottom - Top);

                distances[0] = rightDistance;
                distances[1] = leftDistance;
                distances[2] = topDistance;
                distances[3] = bottomDistance;

                float min = float.MaxValue;
                foreach (float f in distances)
                {
                    if (f < min)
                    {
                        min = f;
                    }
                }

                if (min == rightDistance)
                {
                    return new Vector2(+rightDistance, 0);
                }
                else if (min == leftDistance)
                {
                    return new Vector2(-leftDistance, 0);
                }



            }

            Vector2 LineVector = new Vector2(c.X2 - c.X1, c.Y2 - c.Y1);

            Vector2 TRVector = new Vector2(c.X2 - Right, c.Y2 - Top);
            Vector2 BRVector = new Vector2(c.X2 - Right, c.Y2 - Bottom);
            Vector2 TLVector = new Vector2(c.X2 - Left, c.Y2 - Top);
            Vector2 BLVector = new Vector2(c.X2 - Left, c.Y2 - Bottom);
            Vector2 CVector = new Vector2(c.X2 - X, c.Y2 - Y);

            float CTR = (LineVector.X * TRVector.Y) - (LineVector.Y * TRVector.X);
            float CBR = (LineVector.X * BRVector.Y) - (LineVector.Y * BRVector.X);
            float CTL = (LineVector.X * TLVector.Y) - (LineVector.Y * TLVector.X);
            float CBL = (LineVector.X * BLVector.Y) - (LineVector.Y * BLVector.X);
            float CC = (LineVector.X * CVector.Y) - (LineVector.Y * CVector.X);

            //Debug.getInstance().log("CTR: " + CTR);

            Boolean topEdgeIsIntersecting = CTL * CTR <= 0 && Top >= c.Top;
            Boolean bottomEdgeIsIntersecting = CBL * CBR <= 0 && Bottom <= c.Bottom;
            Boolean rightEdgeIsIntersecting = CTR * CBR <= 0 && Right <= c.Right;
            Boolean leftEdgeIsIntersecting = CTL * CBL <= 0 && Left >= c.Left;
            Boolean leftLeaning = CTR * CC <= 0;
            Boolean topLeaning = CBR * CC <= 0;


            //Debug.getInstance().log("y = " + m + "x + " + b);
            //return m * x + b;

            if (topEdgeIsIntersecting && bottomEdgeIsIntersecting && !rightEdgeIsIntersecting && !leftEdgeIsIntersecting)
            {
                //if (CC)
                if (leftLeaning)
                {

                    var topRightX = Right;
                    var topRightY = Top;

                    var bottomRightX = Right;
                    var bottomRightY = Bottom;

                    return CalculatePenetration(c, topRightX, topRightY, bottomRightX, bottomRightY);


                    // Find furthest of topright and bottomright to line
                    // Push that much in the direction perpendicular to the line
                    //Debug.getInstance().log("Collision top");
                }
                else
                {
                    var topLeftX = Left;
                    var topLeftY = Top;

                    var bottomLeftX = Left;
                    var bottomLeftY = Bottom;

                    return CalculatePenetration(c, topLeftX, topLeftY, bottomLeftX, bottomLeftY);
                }
            }
            else if (!topEdgeIsIntersecting && !bottomEdgeIsIntersecting && rightEdgeIsIntersecting && leftEdgeIsIntersecting)
            {
                //if (CC)
                if (topLeaning)
                {
                    // Find furthest of bottomright and bottomleft to line
                    // Push that much in the direction perpendicular to the line
                    return CalculatePenetration(c, Left, Bottom, Right, Bottom);
                    //Debug.getInstance().log("Collision top");
                }
                else
                {
                    //Other way around
                    return CalculatePenetration(c, Left, Top, Right, Top);

                }
            }
            if (bottomEdgeIsIntersecting && rightEdgeIsIntersecting)
            {
                // Collision at bottomright corner
                return CalculatePenetration(c, Right, Bottom);
            }

            if (bottomEdgeIsIntersecting && leftEdgeIsIntersecting)
            {
                // Collision at bottomleft corner
                return CalculatePenetration(c, Left, Bottom);
            }
            if (topEdgeIsIntersecting && rightEdgeIsIntersecting)
            {
                // Collision at bottomleft corner
                return CalculatePenetration(c, Right, Top);
            }
            if (topEdgeIsIntersecting && leftEdgeIsIntersecting)
            {
                // Collision at bottomleft corner
                return CalculatePenetration(c, Left, Top);
            }
            if (topEdgeIsIntersecting)
            {
                // Collision at bottomleft corner
                return new Vector2(0, c.Bottom - Top);
            }
            if (bottomEdgeIsIntersecting)
            {
                // Collision at bottomleft corner
                return new Vector2(0, c.Top - Bottom);
            }
            if (rightEdgeIsIntersecting)
            {
                // Collision at bottomleft corner
                return new Vector2(c.Left - Right, 0);
            }
            if (leftEdgeIsIntersecting)
            {
                // Collision at bottomleft corner
                return new Vector2(c.Right - Left, 0);
            }
            return null;
        }
    }


}

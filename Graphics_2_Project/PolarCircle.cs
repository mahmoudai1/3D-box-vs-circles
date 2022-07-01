using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Graphics_2_Project
{
    public class PolarCircle
    {
        public float xc, yc, r;

        public PolarCircle(float xcent, float ycent, float radius)
        {
            xc = xcent;
            yc = ycent;
            r = radius;
        }

        public void drawCircle(Graphics g, int gap, int startAngle)
        {
            float angle = startAngle;
            while (angle < 360) // smaller value to draw arcs
            {
                float thRadian = (float)(angle * Math.PI / 180);
                float x = (float)(r * Math.Cos(thRadian)) + xc;
                float y = (float)(r * Math.Sin(thRadian)) + yc;
                g.FillEllipse(Brushes.Red, x, y, 3, 3);
                angle += gap;    // for dashed circle
            }
            //g.FillEllipse(Brushes.Red, xc, yc, 5, 5);
        }

        public PointF getNextPoint(float angle)
        {
            float thRadian = (float)(angle * Math.PI / 180);
            float x = (float)(r * Math.Cos(thRadian)) + xc;
            float y = (float)(r * Math.Sin(thRadian)) + yc;
            PointF pnn = new PointF(x, y);
            return pnn;
        }
    }
}

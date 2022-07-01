using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Graphics_2_Project
{
    class Parmetric_Model : _3D_Model
    {
        public float cX, cY;
        public void Design()
        {
            int R = 35;
            double x = 0, y = 0, z = 0;
            int iP = 0;


            for (float th = 0; th < 360; th += 1)
            {
                float thRadian = (float)(th * Math.PI / 180);
                x = (float)(R * Math.Cos(thRadian)) + cX;
                y = (float)(R * Math.Sin(thRadian)) + cY;
                AddPoint(new _3D_Point((float)x, (float)y + 4, (float)z));
                if (iP > 0)
                    AddEdge(iP, iP - 1, Color.Yellow);
                iP++;

            }
        }

    }
}

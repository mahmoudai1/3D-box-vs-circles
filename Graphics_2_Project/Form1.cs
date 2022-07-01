using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graphics_2_Project
{
    public partial class Form1 : Form
    {
        Bitmap off;

        class Rec
        {
            public int X, Y, W, H;
        }

        List<List<_3D_Model>> LHiddenBoard = new List<List<_3D_Model>>();
        List<List<Rec>> LBoard = new List<List<Rec>>();
        _3D_Model myCube = new _3D_Model();
        List<PolarCircle> LCircles = new List<PolarCircle>();
        List<List<int>> circlesPos = new List<List<int>>();

        Camera cam = new Camera();

        Timer t = new Timer();
        int ctMoveV = 18000, ctMoveH = 18000;
        int whichEdgeV = 5, whichEdgeH = 10;
        int dir = 1;
        int whichRow = 3, whichCol = 4;

        bool gameOver = false, stop = false, firstMoveV = true, firstMoveH = true, baseState = true;

        public Form1()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.Paint += Form1_Paint;
            this.KeyDown += Form1_KeyDown;
            t.Tick += T_Tick;
            t.Start();
        }

        private void T_Tick(object sender, EventArgs e)
        {
            if(!gameOver && stop)
            {
                Transformation.TranslateZ(myCube.L_3D_Pts, 10);
                if (myCube.L_3D_Pts[0].Z > 500)
                {
                    gameOver = true;
                    MessageBox.Show("Game has finished! :D");
                }
            }
            drawDouble(this.CreateGraphics());
        }

        public void scroll(int dir)
        {
            for (int i = 0; i < LHiddenBoard.Count; i++)
            {
                for (int j = 0; j < LHiddenBoard[i].Count; j++)
                {
                    for (int k = 0; k < LHiddenBoard[i][j].L_3D_Pts.Count; k++)
                    {
                        LHiddenBoard[i][j].L_3D_Pts[k].Y += (4.0f * dir);
                    }
                    //LBoard[i][j].Y += 2 * dir;
                }
            }

            for (int i = 0; i < LCircles.Count; i++)
            {
                LCircles[i].yc += 2.84f * dir;
            }
        }


        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            _3D_Point p1, p2;
            if (!gameOver)
            {
                switch (e.KeyCode)
                {
                    case Keys.Up:
                        if (whichRow < 50 && !stop)
                        {
                            baseState = false;
                            if (ctMoveV % 18 == 0 && !firstMoveV)
                            {
                                whichRow++;
                                whichEdgeV = 5;
                                baseState = true;
                                if (circlesPos[whichRow][whichCol] == 1)
                                {
                                    stop = true;
                                }
                            }
                            else
                            {
                                //stop = false;
                            }
                            ctMoveV++;

                            //p1 = new _3D_Point(myCube.L_3D_Pts[myCube.L_Edges[whichEdge].i]);
                            p1 = new _3D_Point(LHiddenBoard[whichRow][whichCol].L_3D_Pts[LHiddenBoard[whichRow][whichCol].L_Edges[whichEdgeV].i]);
                            p2 = new _3D_Point(LHiddenBoard[whichRow][whichCol].L_3D_Pts[LHiddenBoard[whichRow][whichCol].L_Edges[whichEdgeV].j]);

                            Transformation.RotateArbitrary(myCube.L_3D_Pts, p1, p2, 5.0f * dir);
                            Transformation.TranslateY(myCube.L_3D_Pts, 4);
                            scroll(1);
                            //cam.ceneterY += 4;
                            //cam2.ceneterY += 3;

                            if (firstMoveV)
                                firstMoveV = false;
                        }
                        break;

                    case Keys.Down:
                        if (whichRow > 0 && !stop)
                        {
                            baseState = false;
                            if (ctMoveV % 18 == 0 && !firstMoveV)
                            {
                                whichRow--;
                                whichEdgeV = 1;
                                baseState = true;
                                if (circlesPos[whichRow][whichCol] == 1)
                                {
                                    stop = true;
                                }
                            }
                            ctMoveV--;

                            p1 = new _3D_Point(LHiddenBoard[whichRow][whichCol].L_3D_Pts[LHiddenBoard[whichRow][whichCol].L_Edges[whichEdgeV].i]);
                            p2 = new _3D_Point(LHiddenBoard[whichRow][whichCol].L_3D_Pts[LHiddenBoard[whichRow][whichCol].L_Edges[whichEdgeV].j]);

                            Transformation.RotateArbitrary(myCube.L_3D_Pts, p1, p2, -5 * dir);
                            Transformation.TranslateY(myCube.L_3D_Pts, -4);
                            scroll(-1);

                            if (firstMoveV)
                                firstMoveV = false;
                        }
                        break;

                    case Keys.Left:
                        if (whichCol > 0 && !stop)
                        {
                            baseState = false;
                            if (ctMoveH % 18 == 0 && !firstMoveH)
                            {
                                whichCol--;
                                whichEdgeH = 10;
                                baseState = true;
                                if (circlesPos[whichRow][whichCol] == 1)
                                {
                                    stop = true;
                                }
                            }

                            ctMoveH--;

                            p1 = new _3D_Point(LHiddenBoard[whichRow][whichCol].L_3D_Pts[LHiddenBoard[whichRow][whichCol].L_Edges[whichEdgeH].i]);
                            p2 = new _3D_Point(LHiddenBoard[whichRow][whichCol].L_3D_Pts[LHiddenBoard[whichRow][whichCol].L_Edges[whichEdgeH].j]);

                            Transformation.RotateArbitrary(myCube.L_3D_Pts, p1, p2, -5 * dir);

                            if (firstMoveH)
                                firstMoveH = false;
                        }
                        break;

                    case Keys.Right:
                        if (whichCol < LBoard[0].Count - 1 && !stop)
                        {
                            baseState = false;
                            if (ctMoveH % 18 == 0 && !firstMoveH)
                            {
                                whichCol++;
                                whichEdgeH = 11;
                                baseState = true;
                                if (circlesPos[whichRow][whichCol] == 1)
                                {
                                    stop = true;
                                }
                            }

                            ctMoveH++;

                            p1 = new _3D_Point(LHiddenBoard[whichRow][whichCol].L_3D_Pts[LHiddenBoard[whichRow][whichCol].L_Edges[whichEdgeH].i]);
                            p2 = new _3D_Point(LHiddenBoard[whichRow][whichCol].L_3D_Pts[LHiddenBoard[whichRow][whichCol].L_Edges[whichEdgeH].j]);

                            Transformation.RotateArbitrary(myCube.L_3D_Pts, p1, p2, 5 * dir);

                            if (firstMoveH)
                                firstMoveH = false;
                        }
                        break;

                    case Keys.ShiftKey:
                        if(whichEdgeH == 10)
                        {
                            whichEdgeH = 11;
                            label1.Text = "Horizontal Moving: Right (SHIFT)";
                        }
                        else
                        {
                            whichEdgeH = 10;
                            label1.Text = "Horizontal Moving: Left (SHIFT)";
                        }
                        break;

                }
                cam.BuildNewSystem();
                drawDouble(this.CreateGraphics());
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            drawDouble(e.Graphics);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            off = new Bitmap(this.Width, this.Height);

            int cx = 0;
            int cy = 0;
            cam.ceneterX = (this.Width / 2);
            cam.ceneterY = (this.Height / 2);
            cam.cxScreen = cx;
            cam.cyScreen = cy;

            int vX = 589;
            int vY = 655;
            int vSize = 57;
            for (int i = 0; i < 50; i++)
            {
                List<Rec> board = new List<Rec>();
                for (int j = 0; j < 10; j++)
                {
                    Rec cube = new Rec();
                    cube.X = vX;
                    cube.Y = vY;
                    cube.W = vSize;
                    cube.H = vSize;
                    board.Add(cube);
                    vX += vSize;
                }
                LBoard.Add(board);
                vY -= vSize; vX = 589;
            }

            vX = -322;
            vY = 240;

            for (int i = 0; i < 50; i++)
            {
                List<_3D_Model> board = new List<_3D_Model>();
                for(int j = 0; j < 10; j++)
                {
                    _3D_Model cube = new _3D_Model();
                    cube.CreateTheObject("board", vX, vY, 0, Color.Blue);
                    cube.cam = cam;
                    vX += 80;
                    board.Add(cube);
                }
                vX = -322;
                vY -= 80;
                LHiddenBoard.Add(board);
            }


            Random R = new Random();

            int cP = 0;
            int cCt = 0;

            vY = 655;
            for (int i = 0; i < 50; i++)
            {
                for(int t = 0; t < 9999; t++)
                    cCt = R.Next(0, 4);

                List<int> cPosTemp = new List<int>();
                List<int> cPos = new List<int>();

                for (int j = 0; j < cCt; j++)
                {
                    vX = 589;
                    for (int t = 0; t < 9999; t++)
                        cP = R.Next(1, 9);
                    cPosTemp.Add(cP);
                    vX += (57 * cP);
                    PolarCircle circle = new PolarCircle(vX + 28, vY + 27, 20);
                    LCircles.Add(circle);
                }

                for(int k = 0; k < 10; k++)
                {
                    int value = 0;
                    for(int kt = 0; kt < cPosTemp.Count; kt++)
                    {
                        if(cPosTemp[kt] == k)
                        {
                            value = 1;
                            break;
                        }
                    }
                    cPos.Add(value);
                }

                circlesPos.Add(cPos);
                vY -= 57;
            }

            myCube.CreateTheObject("HeroCube", 0, 0, 0, Color.White); // -6
            myCube.cam = cam;

            cam.BuildNewSystem();
        }

        public void drawScene(Graphics g)
        {
            g.Clear(Color.Black);

            for (int i = 0; i < LHiddenBoard.Count; i++)
            {
                for (int j = 0; j < LHiddenBoard[i].Count; j++)
                {
                    Pen Pn = new Pen(Color.Cyan, 3);
                    //g.DrawRectangle(Pn, LBoard[i][j].X, LBoard[i][j].Y, LBoard[i][j].W, LBoard[i][j].H);
                    LHiddenBoard[i][j].DrawYourSelf(g, "board", baseState);
                }
            }

            for (int i = 0; i < LCircles.Count; i++)
            {
                LCircles[i].drawCircle(g, 1, 0);
                //g.DrawEllipse(Pens.Yellow, LCircles[i].cX, LCircles[i].cY + 100, 8, 8);
            }

            myCube.DrawYourSelf(g, "cube", baseState);
        }

        public void drawDouble(Graphics g)
        {
            Graphics g2 = Graphics.FromImage(off);
            drawScene(g2);
            g.DrawImage(off, 0, 0);
        }

    }
}



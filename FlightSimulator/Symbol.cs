// FrontEnd Plus GUI for JAD
// DeCompiled : Symbol.class

// --------------------------------------------------------------------------------------------------
// This file was automatically generated by J2CS Translator (http://j2cstranslator.sourceforge.net/). 
// Version 1.3.6.20110331_01     
// 11/05/19 19:45    
// ${CustomMessageForDisclaimer}                                                                             
// --------------------------------------------------------------------------------------------------
namespace Jp.Maker1.Util
{

    using Jp.Maker1.Vsys3.Tools;
    using System;
    using System.Drawing;
    using System.Collections;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class Symbol
    {

        private static double[] triAngle_x = { 0, -0.5D, 0.5D };
        private static double[] triAngle_y = { 1.0D, 0, 0 };
        private static double[] triAngle_z;
        private static Polygon3D triangle;
        private static Matrix44 tmat = new Matrix44();
        private static Matrix44 smat = new Matrix44();
        private static Matrix44 rmat = new Matrix44();

        public Symbol()
        {
        }

        public static void DrawArrow(Graphics g, int x0, int y0, int x1, int y1, double h, double w)
        {
            Segment3D seg = new Segment3D(x0, y0, 0.0D, x1, y1, 0.0D);
            double len = seg.SegLength();
            double x = seg.p1.x - seg.p0.x;
            double y = seg.p1.y - seg.p0.y;
            double angle;
            Vector3D p2;
            if (len > 0.0D)
            {
                angle = -System.Math.Atan2(x, y);
                p2 = seg.LinearIntarp((len - h) / len);
            }
            else
            {
                angle = 0.0D;
                p2 = new Vector3D(seg.p0);
                p2.y += h;
            }
            // g.DrawLine((int)(seg.p0.x + 0.5D), (int)(seg.p0.y + 0.5D), (int)(p2.x + 0.5D), (int)(p2.y + 0.5D));
            smat.SetSMat(w, h, 0.0D);
            rmat.SetRzMat(angle);
            tmat.SetTMat(p2.x, p2.y, p2.z);
            Matrix44 mat = smat.MultMat(rmat).MultMat(tmat);
            Polygon3D arw = triangle.Transform(mat);
            // g.DrawPolygon(arw.IxArray(), arw.IyArray(), 3);
        }

        public static void FillArrow(Graphics g, int x0, int y0, int x1, int y1, double h, double w)
        {
            Segment3D seg = new Segment3D(x0, y0, 0.0D, x1, y1, 0.0D);
            double len = seg.SegLength();
            double x = seg.p1.x - seg.p0.x;
            double y = seg.p1.y - seg.p0.y;
            double angle;
            Vector3D p2;
            if (len > 0.0D)
            {
                angle = -System.Math.Atan2(x, y);
                p2 = seg.LinearIntarp((len - h) / len);
            }
            else
            {
                angle = 0.0D;
                p2 = new Vector3D(seg.p0);
                p2.y += h;
            }
            //g.DrawLine((int)(seg.p0.x + 0.5D), (int)(seg.p0.y + 0.5D),                    (int)(p2.x + 0.5D), (int)(p2.y + 0.5D));
            smat.SetSMat(w, h, 0.0D);
            rmat.SetRzMat(angle);
            tmat.SetTMat(p2.x, p2.y, p2.z);
            Matrix44 mat = smat.MultMat(rmat).MultMat(tmat);
            Polygon3D arw = triangle.Transform(mat);
            //g.FillPolygon(new SolidBrush(arw.IxArray(), arw.IyArray(), 3);
        }

        public static void Draw_triangle_symbol(Graphics g, int x, int y, int size, int direction)
        {
            int[,] xd = { { -1, 1, 0 }, { 0, 0, 1 }, { -1, 1, 0 }, { 0, 0, -1 } };
            int[,] yd = { { 0, 0, 1 }, { 1, -1, 0 }, { 0, 0, -1 }, { 1, -1, 0 } };
            int[] xp = new int[3];
            int[] yp = new int[3];
            for (int i = 0; i < 3; i++)
            {
                xp[i] = x + xd[direction, i] * size;
                yp[i] = y + yd[direction, i] * size;
            }

            //g.FillPolygon(xp, yp, 3);
        }

        static Symbol()
        {
            triAngle_z = new double[3];
            triangle = new Polygon3D(3, triAngle_x, triAngle_y, triAngle_z);
        }
    }
}
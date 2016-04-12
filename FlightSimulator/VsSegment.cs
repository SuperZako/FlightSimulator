// --------------------------------------------------------------------------------------------------
// This file was automatically generated by J2CS Translator (http://j2cstranslator.sourceforge.net/). 
// Version 1.3.6.20110331_01     
// 11/05/19 19:45    
// ${CustomMessageForDisclaimer}                                                                             
// --------------------------------------------------------------------------------------------------
namespace Jp.Maker1.Vsys3.Tools
{

    using System;
    using System.Drawing;
    using System.Collections;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class VsSegment : VsElement
    {
        public String type;
        public Color col;
        public Pen colPen;
        public Segment3D seg;

        public VsSegment()
        {
            type = "VsSegment";
            col = Color.Black;
            colPen = new Pen(col);
            seg = new Segment3D();
        }

        public VsSegment(double x0, double y0, double z0, double x1, double y1,
                double z1, int r, int g, int b)
        {
            type = "VsSegment";
            col = Color.FromArgb(r, g, b);
            colPen = new Pen(col);
            seg = new Segment3D(x0, y0, z0, x1, y1, z1);
        }

        public VsSegment(double x0, double y0, double z0, double x1, double y1,
                double z1, Color c)
        {
            type = "VsSegment";
            col = c;
            colPen = new Pen(col);
            seg = new Segment3D(x0, y0, z0, x1, y1, z1);
        }

        public VsSegment(Vector3D p0, Vector3D p1, Color c)
        {
            type = "VsSegment";
            col = c;
            colPen = new Pen(col);
            seg = new Segment3D(p0, p1);
        }

        public VsSegment(Segment3D s, Color c)
        {
            type = "VsSegment";
            col = c;
            colPen = new Pen(col);
            seg = s;
        }

        public VsSegment(VsSegment s)
        {
            type = "VsSegment";
            col = s.col;
            colPen = new Pen(col);
            if (s.seg != null)
                seg = new Segment3D(s.seg);
            else
                seg = null;
        }

        public virtual String ElemType()
        {
            return type;
        }

        public virtual VsElement Clip2D(Clipper clip)
        {
            VsSegment ret = new VsSegment(this);
            if (ret.seg != null)
            {
                ret.seg = clip.Vs_line_clip_2d(ret.seg);
            }
            return ret;
        }

        public virtual VsElement Clip3DF(Clipper clip)
        {
            VsSegment ret = new VsSegment(this);
            if (ret.seg != null)
            {
                ret.seg = clip.Vs_line_clip_3df(ret.seg);
            }
            return ret;
        }

        public virtual double Depth()
        {
            if (seg != null)
            {
                double ret = seg.p0.z;
                if (seg.p1.z > ret)
                {
                    ret = seg.p1.z;
                }
                return ret;
            }
            return 0.0D;
        }

        public virtual void Draw(Graphics g)
        {
            if (seg != null)
            {
                //g.SetColor(col);
                int ix0 = seg.Ix0();
                int iy0 = seg.Iy0();
                int ix1 = seg.Ix1();
                int iy1 = seg.Iy1();

                g.DrawLine(colPen, ix0, iy0, ix1, iy1);
                //g.DrawLine(ix0, iy0, ix1, iy1);
            }
        }

        public virtual void Complete_draw(Graphics g)
        {
            Draw(g);
        }

        public void DrawWireframe(Graphics g)
        {
            if (seg != null)
            {
                //g.SetColor(Java.Awt.Color.black);
                int ix0 = seg.Ix0();
                int iy0 = seg.Iy0();
                int ix1 = seg.Ix1();
                int iy1 = seg.Iy1();

                //g.DrawLine(ix0, iy0, ix1, iy1);
            }
        }

        public virtual void Print()
        {
            System.Console.Out.Write(type + "::");
            if (seg != null)
            {
                System.Console.Out.Write(col + "::");
                seg.Print();
            }
            else
            {
                System.Console.Out.WriteLine("null");
            }
        }

        public virtual VsElement Project(Projector proj)
        {
            VsSegment ret = new VsSegment(this);
            if (ret.seg != null)
            {
                ret.seg = ret.seg.Project(proj);
            }
            return ret;
        }

        public virtual VsElement Transform(Matrix44 mat)
        {
            VsSegment ret = new VsSegment(this);

            if (ret.seg != null)
            {
                ret.seg = ret.seg.Transform(mat);
            }

            return ret;
        }

        public virtual BoundingBox BoundingBox()
        {
            if (seg != null)
            {
                return seg.BoundingBox();
            }
            return new BoundingBox();
        }

        public virtual void SetColor(Color colIn)
        {
            col = colIn;
        }

        public virtual void SetMaterial(Material mate)
        {
        }
    }
}
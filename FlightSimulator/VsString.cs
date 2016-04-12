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

    public class VsString : VsElement
    {
        public String type;
        public Color col;
        public Font fon;
        public String data;
        public Vector3D pos;

        public VsString()
        {
            type = "VsString";
            col = Color.Black;
            fon = new Font("SansSerif", 15);
            pos = new Vector3D();
            data = "";
        }

        public VsString(String str, double x, double y, double z, int r, int g,
                int b, String fontName, int fontStyle, int fontSize)
        {
            type = "VsString";
            col = Color.FromArgb(r, g, b);
            fon = new Font(fontName, fontSize);
            pos = new Vector3D(x, y, z);
            data = str;
        }

        public VsString(String str, double x, double y, double z, Color c, Font f)
        {
            type = "VsString";
            col = c;
            fon = f;
            pos = new Vector3D(x, y, z);
            data = str;
        }

        public VsString(String str, Vector3D v, Color c, Font f)
        {
            type = "VsString";
            col = c;
            fon = f;
            pos = v;
            data = str;
        }

        public VsString(VsString s)
        {
            type = "VsString";
            col = s.col;
            fon = s.fon;
            data = s.data;
            if (s.pos != null)
                pos = new Vector3D(s.pos);
            else
                pos = null;
        }

        public virtual String ElemType()
        {
            return type;
        }

        public virtual VsElement Clip2D(Clipper clip)
        {
            VsString ret = new VsString(this);
            return ret;
        }

        public virtual VsElement Clip3DF(Clipper clip)
        {
            VsString ret = new VsString(this);
            if (ret.pos != null)
            {
                ret.pos = clip.Vs_point_clip_3df(ret.pos);
            }
            return ret;
        }

        public virtual double Depth()
        {
            if (pos != null)
            {
                return pos.z;
            }
            return 0.0D;
        }

        public virtual void Draw(Graphics g)
        {
            if (pos != null)
            {
                //g.SetColor(col);
                //g.SetFont(fon);
                int ix = (int)(pos.x + 0.5D);
                int iy = (int)(pos.y + 0.5D);
        
               // g.DrawString(data, ix, iy);
            }
        }

        public virtual void Complete_draw(Graphics g)
        {
            Draw(g);
        }

        public void DrawWireframe(Graphics g)
        {
            if (pos != null)
            {
               // g.SetColor(Java.Awt.Color.black);
               // g.SetFont(fon);
                int ix = (int)(pos.x + 0.5D);
                int iy = (int)(pos.y + 0.5D);

              //  g.DrawString(data, ix, iy);
            }
        }

        public virtual void Print()
        {
            System.Console.Out.Write(type + "::");
            System.Console.Out.Write("'" + data + "'");
            System.Console.Out.Write(" " + col + "::");
            if (pos != null)
                pos.PrintPos();
            else
            {
                System.Console.Out.Write("null");
            }
            System.Console.Out.WriteLine("");
        }

        public virtual VsElement Project(Projector proj)
        {
            VsString ret = new VsString(this);
            if (ret.pos != null)
            {
                ret.pos = proj.Project(ret.pos);
            }
            return ret;
        }

        public void SetData(String str)
        {
            data = str;
        }

        public virtual VsElement Transform(Matrix44 mat)
        {
            VsString ret = new VsString(this);

            if (ret.pos != null)
            {
                ret.pos = ret.pos.MultMat(mat);
            }
            return ret;
        }

        public virtual BoundingBox BoundingBox()
        {
            return new BoundingBox(pos, pos);
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

// FrontEnd Plus GUI for JAD
// DeCompiled : VsString2.class

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

    // Referenced classes of package jp.maker1.vsys3.tools:
    //            VsElement, Vector3D, Clipper, Projector,
    //            BoundingBox, Matrix44, Material

    public class VsString2 : VsElement
    {

        public String type;
        public Color col;
        public String fontName;
        public int fontStyle;
        public int fontSize;
        public double fontBaseSize;
        public String data;
        public Vector3D pos;

        public VsString2()
        {
            type = "VsString2";
            col = Color.Black;
            fontName = "SansSerif";
            fontStyle = 1;
            fontSize = 15;
            fontBaseSize = 1.0D;
            pos = new Vector3D();
            data = "";
        }

        public VsString2(String str, double x, double y, double z, int r, int g, int b, String fn, int fs, int fsz, double fbsz)
        {
            type = "VsString2";
            col = Color.FromArgb(r, g, b);
            fontName = fn;
            fontStyle = fs;
            fontSize = fsz;
            fontBaseSize = fbsz;
            pos = new Vector3D(x, y, z);
            data = str;
        }

        public VsString2(String str, Vector3D v, Color c, String fn, int fs, int fsz, double fbsz)
        {
            type = "VsString2";
            col = c;
            fontName = fn;
            fontStyle = fs;
            fontSize = fsz;
            fontBaseSize = fbsz;
            pos = v;
            data = str;
        }

        public VsString2(VsString2 s)
        {
            type = "VsString2";
            col = s.col;
            fontName = s.fontName;
            fontStyle = s.fontStyle;
            fontSize = s.fontSize;
            fontBaseSize = s.fontBaseSize;
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
            VsString2 ret = new VsString2(this);
            return ret;
        }

        public virtual VsElement Clip3DF(Clipper clip)
        {
            VsString2 ret = new VsString2(this);
            if (ret.pos != null)
                ret.pos = clip.Vs_point_clip_3df(ret.pos);
            return ret;
        }

        public virtual double Depth()
        {
            if (pos != null)
                return pos.z;
            else
                return 0.0D;
        }

        public virtual void Draw(Graphics g)
        {
            if (pos != null)
            {
               // g.SetColor(col);
               // g.SetFont(new Font(fontName, fontStyle, (int)((double)fontSize * fontBaseSize + 0.5D)));
                int ix = (int)(pos.x + 0.5D);
                int iy = (int)(pos.y + 0.5D);
              //  g.DrawString(data, ix, iy);
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
                //g.SetColor(Java.Awt.Color.black);
              //  g.SetFont(new Font(fontName, fontStyle, (int)((double)fontSize * fontBaseSize + 0.5D)));
                int ix = (int)(pos.x + 0.5D);
                int iy = (int)(pos.y + 0.5D);
               // g.DrawString(data, ix, iy);
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
                System.Console.Out.Write("null");
            System.Console.Out.Write(" " + fontBaseSize);
            System.Console.Out.WriteLine("");
        }

        public virtual VsElement Project(Projector proj)
        {
            VsString2 ret = new VsString2(this);
            if (ret.pos != null)
            {
                ret.pos = proj.Project(ret.pos);
                ret.fontBaseSize = proj.ProjectRaitio(fontBaseSize, pos);
            }
            return ret;
        }

        public void SetData(String str)
        {
            data = str;
        }

        public virtual VsElement Transform(Matrix44 mat)
        {
            VsString2 ret = new VsString2(this);
            if (ret.pos != null)
                ret.pos = ret.pos.MultMat(mat);
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

        public virtual void SetMaterial(Material material)
        {
        }
    }
}
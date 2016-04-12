

    using Jp.Maker1.Vsys3.Tools;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.CompilerServices;

public class InertiaBlock
{
    public String name;
    public Vector3D cg;
    public double m;
    public double ixx_m0;
    public double iyy_m0;
    public double izz_m0;
    public double ixy_m0;
    public double iyz_m0;
    public double izx_m0;

    public InertiaBlock(String nameIn, Vector3D cgIn, double mIn, double ixx_m0In, double iyy_m0In, double izz_m0In, double ixy_m0In, double iyz_m0In, double izx_m0In)
    {
        name = nameIn;
        m = mIn;
        cg = cgIn;
        ixx_m0 = ixx_m0In;
        iyy_m0 = iyy_m0In;
        izz_m0 = izz_m0In;
        ixy_m0 = ixy_m0In;
        iyz_m0 = iyz_m0In;
        izx_m0 = izx_m0In;
    }

    public Vector3D M_cg()
    {
        return cg.SclProd(m);
    }

    public Vector3D CgDash(Vector3D p)
    {
        return cg.Sub(p);
    }

    public double Ixx(Vector3D p)
    {
        Vector3D g = CgDash(p);
        return m * (ixx_m0 + (g.y * g.y + g.z * g.z));
    }

    public double Iyy(Vector3D p)
    {
        Vector3D g = CgDash(p);
        return m * (iyy_m0 + (g.x * g.x + g.z * g.z));
    }

    public double Izz(Vector3D p)
    {
        Vector3D g = CgDash(p);
        return m * (izz_m0 + (g.x * g.x + g.y * g.y));
    }

    public double Ixy(Vector3D p)
    {
        Vector3D g = CgDash(p);
        return m * (ixy_m0 + g.x * g.y);
    }

    public double Iyz(Vector3D p)
    {
        Vector3D g = CgDash(p);
        return m * (iyz_m0 + g.y * g.z);
    }

    public double Izx(Vector3D p)
    {
        Vector3D g = CgDash(p);
        return m * (izx_m0 + g.z * g.x);
    }

    public override String ToString()
    {
        return "äµê´ÉuÉçÉbÉNñº:" + name + " " + "C.G.=" + cg.ToString()
                + "[m] " + "m=" + m + "[kg] " + "Ixx/m0=" + ixx_m0
                + "[m2] " + "Iyy/m0=" + iyy_m0 + "[m2] " + "Izz/m0="
                + izz_m0 + "[m2] " + "Ixy/m0=" + ixy_m0 + "[m2] "
                + "Iyz/m0=" + iyz_m0 + "[m2] " + "Izx/m0=" + izx_m0
                + "[m2] ";
    }
}
using Jp.Maker1.Sim.Tools;
using Jp.Maker1.Vsys3.Tools;
using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;


public class Fuselage
{
    public Fuselage()
    {
        flag = 0;
        ac = new Vector3D();
        vd = new Vector3D();
        du = new Vector3D();
        fv = new Vector3D();
        tv = new Vector3D();
    }

    internal String block_name;
    internal int flag;
    internal Vector3D ac_base;
    internal double vfus;
    internal double cd_s;
    internal double s_pi;
    internal double lfus;
    internal double d2;
    internal double s_side;
    internal Vector3D ac;
    internal Vector3D vd;
    internal double v;
    internal double angle;
    internal double sfus;
    internal double q;
    internal double d;
    internal Vector3D du;
    internal double mfus;
    internal Vector3D fv;
    internal Vector3D tv;

    public void Init()
    {
        d2 = Math.Sqrt(4.0D * (vfus / lfus) / Math.PI);

        s_side = (lfus * d2);
    }

    public void Print()
    {
        if (flag == 1)
        {
            System.Console.Out.WriteLine("ƒuƒƒbƒN–¼: " + block_name);
            System.Console.Out.WriteLine("‹ó—Í’†S [m]: " + ac_base.ToString());
            System.Console.Out.WriteLine("“·‘Ì’· [m]: " + lfus);
            System.Console.Out.WriteLine("“·‘Ì‘ÌÏ [m3]: " + vfus);
            System.Console.Out.WriteLine("’ïRŒW”*‘O•û“Š‰e–ÊÏ CDpmin*SƒÎ [m2]: " + cd_s);
            System.Console.Out.WriteLine("‘O•û“Š‰e–ÊÏ SƒÎ [m2]: " + s_pi);
        }
    }

    internal void Calc_dynamics(int lr, AirPlane ap, Vector3D dv)
    {
        fv.SetVec(0.0D, 0.0D, 0.0D);
        tv.SetVec(0.0D, 0.0D, 0.0D);
        if (flag == 0)
            return;

        ac = AirPlane.Get_point(ac_base, lr).Sub(ap.inp.cg);
        vd = Dynamics.VWithRot(ap.pMotion.vc, ap.pMotion.omega, ac);
        vd = vd.Add(dv);
        v = vd.Length();
        q = (0.5D * v * v * ap.atmos.rho);
        Bearing3 br = new Bearing3(ap.pMotion.vc.R2l());
        angle = br.pitch.GetValue();

        sfus = (s_pi * Math.Cos(angle) + s_side * Math.Sin(angle));

        d = (q * cd_s / s_pi * sfus);
        du = vd.SclProd(-1.0D).NmlVec();
        fv = du.SclProd(d);

        mfus = (q * vfus * Math.Sin(-2.0D * angle));

        tv.SetVec(0.0D, mfus, 0.0D);
        Matrix44 mat = new Matrix44();
        mat.SetRxMat(-br.roll.GetValue());
        tv = tv.MultMat(mat);
    }
}
    using Jp.Maker1.Vsys3.Tools;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.CompilerServices;

public class LandingGear
{
    public LandingGear()
    {
        damper_k = 0.5D;
        flag = 0;
        sa = new Segment3D();
        t_lag_move = new double[2];
        brake = 0.0D;
        delta = 1.0D;
        p0w = new Vector3D();
        p0g = new Vector3D();
        pw = new Vector3D();
        pg = new Vector3D();
        vd = new Vector3D();
        pa = new Vector3D();
        vda = new Vector3D();
        a_fv = new Vector3D();
        a_tv = new Vector3D();
        n_fv = new Vector3D();
        n_tv = new Vector3D();
        f_fv = new Vector3D();
        f_tv = new Vector3D();
    }

    public readonly double damper_k;
    internal String block_name;
    internal int flag;
    internal Vector3D p0_base;
    internal double stroke0;
    internal double w;
    internal Tire2 tire;
    internal double cd_s;
    internal Segment3D sa;
    internal double t_move;
    internal double[] t_lag_move;
    internal double k_sus;
    internal double c_sus;
    internal double brake;
    internal double delta;

    internal Vector3D p0w;
    internal Vector3D p0g;
    internal Vector3D pw;
    internal Vector3D pg;
    internal double stroke;
    internal double stroke_b;
    internal double d_stroke;
    internal double fz;
    internal Vector3D vd;
    internal int flag_land;
    internal Vector3D pa;
    internal Vector3D vda;
    internal double vpa;
    internal double q;
    internal double d;
    internal Vector3D a_fv;
    internal Vector3D a_tv;
    internal Vector3D n_fv;
    internal Vector3D n_tv;
    internal Vector3D f_fv;
    internal Vector3D f_tv;

    public void Init()
    {
        stroke = 0.0D;

        k_sus = (w * 9.80655D / stroke0);
        c_sus = (2.0D * w * Math.Sqrt(9.80655D / stroke0) * 0.5D);
    }

    public void Print()
    {
        if (flag == 1)
        {
            System.Console.Out.WriteLine("ƒuƒƒbƒN–¼: " + block_name);
            System.Console.Out.WriteLine("Ú’nŠî€“_ˆÊ’u [m]: " + p0_base.ToString());
            System.Console.Out.WriteLine("Ã‰×dŽžƒXƒgƒ[ƒN [m]: " + stroke0);
            System.Console.Out.WriteLine("Ã‰×d [kg]: " + w);
            tire.Print();
            System.Console.Out.WriteLine("R—ÍŒW”*‘ã•\–ÊÏ [m2]: " + cd_s);
            System.Console.Out.WriteLine("‹rŠi”[Žž‚Ì‹ó—Í’…—Í“_ [m]: " + sa.p0.ToString());
            System.Console.Out.WriteLine("‹ro‚µŽž‚Ì‹ó—Í’…—Í“_ [m]: " + sa.p1.ToString());
            System.Console.Out.WriteLine("‹ro‚µ/Ši”[ŽžŠÔ [s]: " + t_move);
            System.Console.Out.WriteLine("‰E‚Ìì“®ŠJŽn’x‚êŽžŠÔ [s]: " + t_lag_move[0]);
            System.Console.Out.WriteLine("¶‚Ìì“®ŠJŽn’x‚êŽžŠÔ [s]: " + t_lag_move[1]);
            System.Console.Out.WriteLine("ŠÉÕ‘•’u‚Ìƒoƒl’è” [kg/s2]: " + DispFormat.DoubleFormat(k_sus, 1));
            System.Console.Out.WriteLine("ŠÉÕ‘•’u‚Ìƒ_ƒ“ƒp’è” [kg/s](—ÕŠE§UŽž‚Ì50.0%): " + DispFormat.DoubleFormat(c_sus, 1));
        }
        System.Console.Out.WriteLine("---------------------------------------------");
    }

    internal void Set_landling_gear_delta(int lr, CockpitInterface cif, double dt)
    {
        if (cif.landing_gear_counter > t_lag_move[lr])
            if (cif.landing_gear_sw == -1)
            {
                delta += dt / t_move;
                if (delta > 1.0D)
                    delta = 1.0D;
            }
            else
            {
                delta -= dt / t_move;
                if (delta < 0.0D)
                    delta = 0.0D;
            }
    }

    internal void Calc_dynamics(int lr, AirPlane ap, double dt)
    {
        n_fv.SetVec(0.0D, 0.0D, 0.0D);
        n_tv.SetVec(0.0D, 0.0D, 0.0D);
        f_fv.SetVec(0.0D, 0.0D, 0.0D);
        f_tv.SetVec(0.0D, 0.0D, 0.0D);
        a_fv.SetVec(0.0D, 0.0D, 0.0D);
        a_tv.SetVec(0.0D, 0.0D, 0.0D);
        if (flag == 0)
            return;

        p0g.SetVec(AirPlane.Get_point(p0_base, lr).Sub(ap.inp.cg));
        p0w.SetVec(AirPlane.Get_point(p0_base, lr).R2l().MultMat(ap.pMotion.owm).L2r());

        vd = Dynamics.VWithRot(ap.pMotion.vc, ap.pMotion.omega, p0g);

        if ((p0w.z > 0.0D) && (delta == 1.0D))
            flag_land = 1;
        else
        {
            flag_land = 0;
        }

        stroke_b = stroke;
        pw.SetVec(p0w.x, p0w.y, 0.0D);

        if (flag_land == 1)
        {
            pg = pw.R2l().MultMat(ap.pMotion.wom).L2r();
            stroke = p0w.z;
        }
        else
        {
            pg.SetVec(p0g);
            stroke = 0.0D;
        }

        d_stroke = ((stroke - stroke_b) / dt);

        if (delta == 1.0D)
        {
            if (flag_land == 1)
            {
                fz = (-stroke * k_sus - d_stroke * c_sus);
                if (fz > 0.0D)
                    fz = 0.0D;
            }
            else
            {
                fz = 0.0D;
            }
            n_fv = ap.pMotion.dg.SclProd(fz);
            n_tv = Dynamics.Torque(pg, n_fv);

            Matrix44 otm = new Matrix44();
            Matrix44 tom = new Matrix44();
            Matrix44 rxmat = new Matrix44();
            Matrix44 rymat = new Matrix44();
            rymat.SetRyMat(ap.pMotion.pitch.GetValue());
            rxmat.SetRyMat(ap.pMotion.roll.GetValue());
            tom = rxmat.MultMat(rymat);

            rymat.SetRyMat(-ap.pMotion.pitch.GetValue());
            rxmat.SetRyMat(-ap.pMotion.roll.GetValue());
            otm = rymat.MultMat(rxmat);

            tire.Update(vd.MultMat(otm), -fz, 1.0D, brake);

            if (flag_land == 1)
            {
                f_fv.SetVec(tire.fx, tire.fy, 0.0D);
                f_fv = f_fv.MultMat(tom);
                f_tv = f_tv.Add(Dynamics.Torque(pg, f_fv));
            }

        }

        pa.SetVec(AirPlane.Get_point(sa.LinearIntarp(delta), lr).Sub(ap.inp.cg));

        vd = Dynamics.VWithRot(ap.pMotion.vc, ap.pMotion.omega, pa);
        vpa = vd.Length();

        q = (0.5D * ap.atmos.rho * vpa * vpa);

        if (delta != 0.0D)
        {
            d = (q * cd_s);
            a_fv = vd.NmlVec().SclProd(-d);
            a_tv = a_tv.Add(Dynamics.Torque(pa, a_fv));
        }
    }
}
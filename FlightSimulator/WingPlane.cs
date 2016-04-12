

    using Jp.Maker1.Vsys3.Tools;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.CompilerServices;

public class WingPlane
{
    public WingPlane()
    {
        back_clmax = 0.5D;
        mss = 1.5D;
        flag = 0;
        ac = new Vector3D();
        dvv = new Vector3D();
        vdw = new Vector3D();
        lu = new Vector3D();
        du = new Vector3D();
        k_a = 1.0D;
        lf = new Vector3D();
        lt = new Vector3D();
        df = new Vector3D();
        dt = new Vector3D();
        mt = new Vector3D();
        fv = new Vector3D();
        tv = new Vector3D();
    }

    public readonly double back_clmax;
    public readonly double mss;
    public String block_name;
    public Wing wing;
    public int flag;
    public int arrangement;
    public double s2;
    public Vector3D ac_base;
    public Vector3D gc;
    public double gamma;
    public double delta;
    public double lamda;
    public double t_c;
    public double a0;
    public double alpha0;
    public double clmax;
    public double delta_clmax;
    public double delta_alphe_p;
    public double clmin;
    public double delta_clmin;
    public double delta_alphe_m;
    public double alpha_i;
    public double cdmin;
    public double k_cd;
    public double delta_cdmin;
    public double alpha_backet;
    public double cmac0;
    public double ew;
    public double mc;
    public Vector3D id;
    public Vector3D jd;
    public Vector3D kd;
    public double ar;
    public double are;
    public Vector3D ac;
    public Vector3D dvv;
    public Vector3D vd;
    public double v;
    public double m;
    public double s2_dash;
    public double q;
    public double lamda_dash;
    public Vector3D vdw;
    public double alpha;
    public double beta;
    public Vector3D lu;
    public Vector3D du;
    public double cla;
    public double clmax_dash;
    public double clmin_dash;
    public double k_lamda;
    public double mc_dash;
    public double mss_dash;
    public double alpha_s_m2;
    public double alpha_s_m;
    public double alpha_s_p;
    public double alpha_s_p2;
    public double k_stall;
    public double cl;
    public double cd;
    public double cdk;
    public double cdz;
    public double cdi;
    public double cmac;
    public double delta_cl_flap;
    public double delta_alpha;
    public double k_a;
    public double delta_cd_flap;
    public double delta_cmac_flap;
    public double l;
    public Vector3D lf;
    public Vector3D lt;
    public double d;
    public Vector3D df;
    public Vector3D dt;
    public double mac;
    public Vector3D mt;
    public Vector3D fv;
    public Vector3D tv;

    public void Init()
    {
        Init_wing_arrangement(arrangement);
    }

    public void Print()
    {
        if (flag != 0)
        {
            System.Console.Out.WriteLine("ƒuƒƒbƒN–¼: " + block_name);
            System.Console.Out.WriteLine("—ƒ”z’u: " + arrangement);
            System.Console.Out.WriteLine("—ƒ–ÊÏ[3D]: " + s2);
            System.Console.Out.WriteLine("‹ó—Í’†S Ac [m]: " + ac_base.ToStringPos());
            System.Console.Out.WriteLine("—ƒ–ÊdS Gc [m]: " + gc.ToStringPos());
            System.Console.Out.WriteLine("ã”¼Šp ƒ¡ [deg]: " + MathTool.RadToDeg(gamma));
            System.Console.Out.WriteLine("æ•tŠp ƒÂ [deg]: " + MathTool.RadToDeg(delta));
            System.Console.Out.WriteLine("Œã‘ŞŠp ƒÉ [deg]: " + MathTool.RadToDeg(lamda));
            System.Console.Out.WriteLine("—ƒŒú”ä t/c: " + t_c);

            Console.Out.WriteLine("—ƒŒ^(2ŸŒ³)—g—ÍŒXÎ a0: " + a0);
            Console.Out.WriteLine("–³—g—ÍŠp ƒ¿0 [deg]:" + MathTool.RadToDeg(alpha0));
            Console.Out.WriteLine("Å‘å—g—ÍŒW” CLmax:" + clmax);
            Console.Out.WriteLine("”—£—g—ÍŒW”Œ¸­•ª ƒ¢CLmax:" + delta_clmax);
            Console.Out.WriteLine("ŒÀŠE‚Ü‚Å‚Ì‹ÂŠp‘‘å•ª ƒ¢ƒ¿(+) [deg]:" + MathTool.RadToDeg(delta_alphe_p));
            Console.Out.WriteLine("Å¬—g—ÍŒW” CLmin:" + clmin);
            Console.Out.WriteLine("”—£—g—ÍŒW”‘‰Á•ª ƒ¢CLmin:" + delta_clmin);
            Console.Out.WriteLine("ŒÀŠE‚Ü‚Å‚Ì‹ÂŠpŒ¸­•ª ƒ¢ƒ¿(-) [deg]:" + MathTool.RadToDeg(delta_alphe_m));
            Console.Out.WriteLine("—‘z‹ÂŠp ƒ¿I [deg]:" + MathTool.RadToDeg(alpha_i));
            Console.Out.WriteLine("Å¬R—ÍŒW” Cdmin:" + cdmin);
            Console.Out.WriteLine("R—ÍŒW”‹ß—•ú•¨ü‚ÌŒW” Cdk [1/rad2]:" + k_cd);
            Console.Out.WriteLine("ƒoƒPƒbƒg‚Å‚ÌR—ÍŒW”Œ¸­•ª:" + delta_cdmin);
            Console.Out.WriteLine("ƒoƒPƒbƒg‚Ì”ÍˆÍ [deg]:" + MathTool.RadToDeg(alpha_backet));
            Console.Out.WriteLine("‹ó—Í’†Sƒ‚[ƒƒ“ƒgŒW”:" + cmac0);
            Console.Out.WriteLine("—ƒ•Œø—¦ ew:" + ew);
            Console.Out.WriteLine("—ƒŒ^—ÕŠEƒ}ƒbƒn”:" + mc);

            Console.Out.WriteLine("—ƒŠî€Œn");
            Console.Out.WriteLine("id(‘O•û): " + id.ToStringPos());
            Console.Out.WriteLine("jd(‘¤•û): " + jd.ToStringPos());
            Console.Out.WriteLine("kd(–@ü): " + kd.ToStringPos());
        }
    }

    public void Init_wing_arrangement(int arrangement_0)
    {
        double g = gamma;
        double d_1 = delta;
        Vector3D s;

        switch (arrangement_0)
        {
            case 1:
                id = new Vector3D(System.Math.Cos(d_1), 0.0D, -System.Math.Sin(d_1));
                s = new Vector3D(0.0D, System.Math.Cos(g), -System.Math.Sin(g));
                break;
            case 2:
                id = new Vector3D(System.Math.Cos(d_1), 0.0D, -System.Math.Sin(d_1));
                s = new Vector3D(0.0D, -System.Math.Cos(g), -System.Math.Sin(g));
                break;
            case 3:
                id = new Vector3D(System.Math.Cos(d_1), System.Math.Sin(d_1), 0.0D);
                s = new Vector3D(0.0D, System.Math.Sin(g), -System.Math.Cos(g));
                break;
            case 4:
                id = new Vector3D(System.Math.Cos(d_1), -System.Math.Sin(d_1), 0.0D);
                s = new Vector3D(0.0D, System.Math.Sin(g), -System.Math.Cos(g));
                break;
            default:
                id = new Vector3D(0.0D, 0.0D, 0.0D);
                s = new Vector3D(0.0D, 0.0D, 0.0D);
                break;
        }
        Vector3D n;

        if ((arrangement_0 == 1) || (arrangement_0 == 3) || (arrangement_0 == 4))
            n = id.CrsProd(s);
        else
        {
            n = s.CrsProd(id);
        }
        kd = n.NmlVec();
        jd = kd.CrsProd(id);
    }

    internal void Calc_dynamics(int lr, ControlPlane cw, AirPlane ap, Vector3D dv, double k_q, double k_S)
    {
        Matrix44 ma = new Matrix44();
        Matrix44 mtemp = new Matrix44();
        Vector3D vtemp = new Vector3D();

        fv.SetVec(0.0D, 0.0D, 0.0D);
        tv.SetVec(0.0D, 0.0D, 0.0D);

        ac = AirPlane.Get_point(ac_base, lr).Sub(ap.inp.cg);

        dvv = ap.pMotion.omega.CrsProd(ac);
        vd = ap.pMotion.vc.Add(dvv);

        vd = vd.Add(dv);
        v = vd.Length();
        vd = vd.NmlVec().SclProd(v * k_q);

        v = vd.Length();

        s2_dash = (s2 * k_S);

        m = (v / ap.atmos.a);

        q = (0.5D * v * v * ap.atmos.rho);
        Vector3D kdd = new Vector3D();
        Vector3D idd = new Vector3D();
        Vector3D jdd = new Vector3D();

        if ((cw != null) && (cw.type == 2))
        {
            ma.SetRyMat(cw.delta);
            mtemp.SetRzMat(cw.lamda_h);
            ma = ma.MultMat(mtemp);
            if (lr == 1)
                mtemp.SetRxMat(-cw.gamma_h);
            else
            {
                mtemp.SetRxMat(-cw.gamma_h);
            }
            ma = ma.MultMat(mtemp);

            idd = id.MultMat(ma);

            kdd = kd.MultMat(ma);
        }
        else
        {
            idd = new Vector3D(id);
            jdd = new Vector3D(jd);
            kdd = new Vector3D(kd);
        }

        mtemp.Set3ColVec(idd, jdd, kdd);
        vdw = vd.MultMat(mtemp);
        alpha = Jp.Maker1.Sim.Tools.Tool.CalcAlpha(vdw);
        beta = Jp.Maker1.Sim.Tools.Tool.CalcBeta(vdw);

        ar = wing.AspectRatio(beta);
        are = (ar * wing.K_ground_effect(beta,
                ap.pMotion.wpos.y));

        lamda_dash = SweepbackAngle(wing.hv_arrangement, lr,
                beta);

        Calc_cla();

        delta_cl_flap = 0.0D;
        delta_cd_flap = 0.0D;
        delta_cmac_flap = 0.0D;
        delta_alpha = 0.0D;
        k_a = 1.0D;
        if ((cw != null) && ((cw.type == 1) || (cw.type == 1)))
        {
            cw.beta_f = Flap.Beta_f(lr, wing.hv_arrangement, cw.lamda_h, beta);

            cw.delta_dash = Flap.Delta_dash(cw.delta, cw.beta_f);

            if (Math.Abs(cw.beta_f) < 1.570796326794897D)
            {
                delta_alpha = (cw.f_lamda1 * cw.delta_dash);
                delta_cmac_flap = cw.f_cmac;
            }
            else
            {
                delta_alpha = (cw.b_lamda1 * cw.delta_dash);
                delta_cmac_flap = cw.b_cmac;
            }

            k_a = Flap.K_cla(cw.delta_dash, cw.cf_c);
            delta_cl_flap = (k_a * cla * delta_alpha);

            delta_cmac_flap *= (alpha + delta_alpha - alpha0);
        }

        vtemp.SetVec(0.0D, 0.0D, -1.0D);
        ma.SetRyMat(-alpha);
        mtemp.SetRzMat(beta);
        ma = ma.MultMat(mtemp);
        mtemp.Set3RowVec(idd, jdd, kdd);
        ma = ma.MultMat(mtemp);
        lu = vtemp.MultMat(ma);

        du = vd.SclProd(-1.0D).NmlVec();

        alpha += delta_alpha;
        cla *= k_a;
        Calc_cl_cd();

        l = (q * cl * s2_dash);

        cdi = (cl * cl / (Math.PI * are * ew));
        cd += cdi;
        cd += delta_cd_flap;
        d = (q * cd * s2_dash);

        lf = Dynamics.Force(l, lu);
        lt = Dynamics.Torque(ac, lf);

        df = Dynamics.Force(d, du);
        dt = Dynamics.Torque(ac, df);

        cmac = cmac0;
        cmac += delta_cmac_flap;
        mac = (q * s2_dash * cmac);
        Vector3D mt_0 = new Vector3D(0.0D, mac, 0.0D);

        fv = lf.Add(df);
        tv = lt.Add(dt).Add(mt_0);
    }

    public void Calc_cl_cd()
    {
        double cosl = System.Math.Cos(lamda_dash);
        clmax_dash = ((clmax + delta_cl_flap) * cosl * cosl);
        clmin_dash = ((clmin + delta_cl_flap) * cosl * cosl);
        if ((beta < -1.570796326794897D)
                && (beta > 1.570796326794897D))
        {
            clmax_dash *= 0.5D;
            clmin_dash *= 0.5D;
        }

        alpha_s_m = (clmin_dash / cla + alpha0);
        alpha_s_p = (clmax_dash / cla + alpha0);
        alpha_s_m2 = (alpha_s_m + delta_alphe_m);
        alpha_s_p2 = (alpha_s_p + delta_alphe_p);

        if (alpha <= alpha_s_m2)
        {
            cl = Cl_base(alpha);
        }
        else if (alpha < alpha_s_m)
        {
            double c1 = clmin_dash;
            double c2 = Cl_base(alpha_s_m2);
            cl = Jp.Maker1.Sim.Tools.Tool.Hokan(alpha_s_m2, c2, alpha_s_m, c1,
                    alpha);
        }
        else if (alpha < alpha_s_p)
        {
            cl = Cl_normal(alpha);
        }
        else if (alpha < alpha_s_p2)
        {
            double c1_0 = clmax_dash;
            double c2_1 = Cl_base(alpha_s_p2);
            cl = Jp.Maker1.Sim.Tools.Tool.Hokan(alpha_s_p, c1_0, alpha_s_p2, c2_1,
                    alpha);
        }
        else
        {
            cl = Cl_base(alpha);
        }

        if (alpha > alpha0)
            k_stall = ((alpha - alpha0) / (alpha_s_p - alpha0));
        else
        {
            k_stall = ((alpha - alpha0) / (alpha_s_m - alpha0));
        }

        if (alpha <= alpha_s_m2)
        {
            cdk = Cd_base(alpha);
        }
        else if (alpha < alpha_s_m)
        {
            double c1_2 = Cd_normal(alpha_s_m);
            double c2_3 = Cd_base(alpha_s_m2);
            cdk = Jp.Maker1.Sim.Tools.Tool.Hokan(alpha_s_m2, c2_3, alpha_s_m, c1_2, alpha);
        }
        else if (alpha < alpha_s_p)
        {
            cdk = Cd_normal(alpha);
        }
        else if (alpha < alpha_s_p2)
        {
            double c1_4 = Cd_normal(alpha_s_p);
            double c2_5 = Cd_base(alpha_s_p2);
            cdk = Jp.Maker1.Sim.Tools.Tool.Hokan(alpha_s_p, c1_4, alpha_s_p2, c2_5, alpha);
        }
        else
        {
            cdk = Cd_base(alpha);
        }
        if (Math.Abs(alpha - alpha_i) <= alpha_backet / 2.0D)
        {
            cdk = (cdmin + delta_cdmin);
        }

        if (m <= mc_dash)
        {
            cdz = 0.0D;
        }
        else if (m < mss_dash)
        {
            double cdz1;

            if (alpha <= alpha_s_m2)
            {
                cdz1 = Calc_cdz_stall(mss_dash, cl, alpha);
            }
            else
            {
                ;
                if (alpha < alpha_s_m)
                {
                    double c1_6 = Calc_cdz_normal(mss_dash, cl,
                            alpha_s_m);
                    double c2_7 = Calc_cdz_stall(mss_dash, cl,
                            alpha_s_m2);
                    cdz1 = Jp.Maker1.Sim.Tools.Tool.Hokan(alpha_s_m2, c2_7, alpha_s_m, c1_6,
                            alpha);
                }
                else
                {

                    if (alpha < alpha_s_p)
                    {
                        cdz1 = Calc_cdz_normal(mss_dash, cl,
                                alpha);
                    }
                    else
                    {

                        if (alpha < alpha_s_p2)
                        {
                            double c1_8 = Calc_cdz_normal(mss_dash, cl,
                                    alpha_s_p);
                            double c2_9 = Calc_cdz_stall(mss_dash, cl,
                                    alpha_s_p2);
                            cdz1 = Jp.Maker1.Sim.Tools.Tool.Hokan(alpha_s_p, c1_8,
                                    alpha_s_p2, c2_9, alpha);
                        }
                        else
                        {
                            cdz1 = Calc_cdz_stall(mss_dash, cl,
                                    alpha);
                        }
                    }
                }
            }
            cdz = Jp.Maker1.Sim.Tools.Tool.Hokan(mc_dash, 0.0D, mss_dash, cdz1,
                    m);
        }
        else if (alpha <= alpha_s_m2)
        {
            cdz = Calc_cdz_stall(m, cl, alpha);
        }
        else if (alpha < alpha_s_m)
        {
            double c1_10 = Calc_cdz_normal(m, cl, alpha_s_m);
            double c2_11 = Calc_cdz_stall(m, cl, alpha_s_m2);
            cdz = Jp.Maker1.Sim.Tools.Tool.Hokan(alpha_s_m2, c2_11, alpha_s_m, c1_10,
                    alpha);
        }
        else if (alpha < alpha_s_p)
        {
            cdz = Calc_cdz_normal(m, cl, alpha);
        }
        else if (alpha < alpha_s_p2)
        {
            double c1_12 = Calc_cdz_normal(m, cl, alpha_s_p);
            double c2_13 = Calc_cdz_stall(m, cl, alpha_s_p2);
            cdz = Jp.Maker1.Sim.Tools.Tool.Hokan(alpha_s_p, c1_12, alpha_s_p2, c2_13,
                    alpha);
        }
        else
        {
            cdz = Calc_cdz_stall(m, cl, alpha);
        }

        cd = (cdk + cdz);
    }

    public void Calc_cla()
    {
        k_lamda = K_sweepBackAngle(lamda_dash, are);
        double cosl = System.Math.Cos(lamda_dash);

        mc_dash = (mc / k_lamda);

        mss_dash = Math.Sqrt(1.5D / k_lamda / k_lamda);

        if (m <= mc_dash)
        {
            cla = Calc_cla_subsonic(a0, m, are,
                    lamda_dash);
        }
        else if (m < mss_dash)
        {
            double cla_s = Calc_cla_subsonic(a0, mc_dash, are,
                    lamda_dash);
            double cla_h = Calc_cla_hypersonic(mss_dash, are,
                    lamda_dash);
            cla = Jp.Maker1.Sim.Tools.Tool.Hokan(mc_dash, cla_s, mss_dash, cla_h,
                    m);
        }
        else
        {
            cla = Calc_cla_hypersonic(m, are, lamda_dash);
        }
    }

    internal double Cl_normal(double alpha_0)
    {
        return cla * (alpha_0 - alpha0);
    }

    internal double Cd_normal(double alpha_0)
    {
        double x = alpha_0 - alpha_i;
        double ret = cdmin + k_cd * x * x;

        return ret;
    }

    internal double Cl_base(double alpha_0)
    {
        return 2.0D * System.Math.Sin(alpha_0) * System.Math.Cos(alpha_0);
    }

    internal double Cd_base(double alpha_0)
    {
        return 2.0D * System.Math.Sin(alpha_0) * System.Math.Sin(alpha_0);
    }

    internal double SweepbackAngle(int hv, int lr, double beta_0)
    {
        double ret;

        if (hv == 0)
        {
            if (lr == 0)
                ret = lamda - beta_0;
            else
                ret = lamda + beta_0;
        }
        else
        {
            ret = lamda - beta_0;
        }
        if (ret > 1.22D)
            ret = 1.22D;
        if (ret < -1.22D)
            ret = -1.22D;
        return ret;
    }

    public static double Calc_cla_subsonic(double a0_0, double m_1, double ar_2, double lamda_3)
    {
        double cosl = System.Math.Cos(lamda_3);
        double f = Math.PI * ar_2 / a0_0 / cosl;
        return Math.PI * ar_2 / (1.0D + Math.Sqrt(1.0D + f * f * (1.0D - m_1 * m_1 * cosl * cosl)));
    }

    internal double Calc_cla_hypersonic(double m_0, double ar_1, double lamda_2)
    {
        double cosl = System.Math.Cos(lamda_2);
        return 4.0D / Math.Sqrt(m_0 * m_0 - 1.0D) * (1.0D - 0.5D / ar_1 / Math.Sqrt(m_0 * m_0 - 1.0D)) * cosl;
    }

    internal double K_sweepBackAngle(double lamda_0, double ar_1)
    {
        return Math.Pow(System.Math.Cos(lamda_0), 1.0D - 4.0D / (ar_1 + 4.0D));
    }

    internal double Calc_cdz_normal(double m_0, double cl_1, double alpha_2)
    {
        double delta1_cdw = cl_1 * (alpha_2 - alpha_i);
        double b = m_0 * m_0 - 1.0D;
        double delta2_cdw;

        if (b >= 0.0D)
            delta2_cdw = 1.0D * t_c * t_c * 4.0D / Math.Sqrt(b);
        else
        {
            delta2_cdw = 0.0D;
        }
        return delta1_cdw + delta2_cdw;
    }

    internal double Calc_cdz_stall(double m_0, double cl_1, double alpha_2)
    {
        double delta1_cdw = cl_1 * (alpha_2 - alpha_i);
        double b = m_0 * m_0 - 1.0D;
        double delta2_cdw;

        if (b >= 0.0D)
            delta2_cdw = Cd_base(alpha_2 - alpha_i) * 4.0D / Math.Sqrt(b);
        else
        {
            delta2_cdw = 0.0D;
        }
        return delta1_cdw + delta2_cdw;
    }
}
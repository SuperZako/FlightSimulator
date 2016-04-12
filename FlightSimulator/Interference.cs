
    using Jp.Maker1.Sim.Tools;
    using Jp.Maker1.Util;
    using Jp.Maker1.Vsys3.Tools;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.CompilerServices;

public class Interference
{
    public Interference()
    {
        angle_t = new Angle(1);
        wf_theta1 = 0.52D;
        wf_theta2 = 1.0D;
        wf_epsilon = 0.0D;
        wf_epsilon_htail = 0.0D;
        wf_b_epsilon_htail = 0.0D;
        wf_d_epsilon_htail = 0.0D;
        wt_theta1 = 0.104719755D;
        wt_theta2 = 0.20943951D;
        wt_dv = 0.9D;
        pr_k_diameter_min = 2.0D;
        pr_k_diameter_max = 3.0D;
        htail_dv = new Vector3D[2];
        htail_k_q = new double[2];
        vtail_dv = new Vector3D[2];
        vtail_k_q = new double[2];
        vtail_k_S = new double[2];
    }

    public double wing_cl;
    public double wing_ar;
    public Function2D k_SVtail_Wing_stall;
    public Function2D k_SVtail_Htail_stall;
    public double lt;
    internal Angle angle_t;
    public double wf_beta1;
    public double wf_beta2;
    public double wf_theta1;
    public double wf_theta2;
    public double wf_epsilon;
    public double wf_epsilon_htail;
    private double wf_b_epsilon_htail;
    private double wf_d_epsilon_htail;

    public double wt_theta1;
    public double wt_theta2;
    public double wt_dv;
    public double wt_htail_k;
    public double pr_dv;
    public double pr_k_diameter_min;
    public double pr_k_diameter_max;
    public double pr_k;
    public Vector3D[] htail_dv;
    public double[] htail_k_q;
    public Vector3D[] vtail_dv;
    public double[] vtail_k_q;
    public double[] vtail_k_S;
    public Vector3D[,] fuselage_dv;

    public void Init(AirPlane ap)
    {
        int i;
        for (i = 0; i <= 1; i++)
        {
            htail_dv[i] = new Vector3D();
            vtail_dv[i] = new Vector3D();
        }
        fuselage_dv = new Vector3D[ap.n_fuselage, 2];
        for (i = 0; i < ap.n_fuselage; i++)
        {
            for (int lr = 0; lr <= 1; lr++)
            {
                fuselage_dv[i, lr] = new Vector3D();
            }

        }

        if ((ap.n_wing > 0) && (ap.n_htail > 0) && (ap.wing[0].n_wing_block > 0) && (ap.htail[0].n_wing_block > 0))
        {
            Vector3D v = new Vector3D(ap.htail[0].wp[0, 0].ac_base.Sub(ap.wing[0].wp[0, 0].ac_base));
            v.y = 0.0D;
            lt = v.Length();
            angle_t.SetValue(System.Math.Atan2(-v.z, -v.x));
            wf_beta1 = System.Math.Atan((ap.wing[0].Span(0.0D) - ap.htail[0].Span(0.0D)) / 2.0D / lt);
            wf_beta2 = System.Math.Atan((ap.wing[0].Span(0.0D) + ap.htail[0].Span(0.0D)) / 2.0D / lt);
        }

        pr_k = (2.0D / (pr_k_diameter_max + pr_k_diameter_min));
    }

    public void Print()
    {
        System.Console.Out.WriteLine("å—ƒ¸‘¬‚Ì‚’¼”ö—ƒ–ÊÏŒW”(ƒ¿‚ÌŠÖ”)");
        k_SVtail_Wing_stall.Print();
        System.Console.Out.WriteLine("…•½”ö—ƒ¸‘¬‚Ì‚’¼”ö—ƒ–ÊÏŒW”(ƒ¿‚ÌŠÖ”)");
        k_SVtail_Htail_stall.Print();

        System.Console.Out.WriteLine("å—ƒ‚©‚ç…•½”ö—ƒ‚Ü‚Å‚Ì‹——£ [m]:" + DispFormat.DoubleFormat(lt, 3));
        System.Console.Out.WriteLine("å—ƒ‚©‚ç…•½”ö—ƒ‚Ü‚Å‚ÌŠp“x [deg]:" + DispFormat.DoubleFormat(angle_t.GetValue() * 180.0D / Math.PI, 1));
        System.Console.Out.WriteLine("å—ƒ~Šp‚ÌŠ®‘S‰e‹¿Šp(‰¡) [deg]:" + DispFormat.DoubleFormat(wf_beta1 * 180.0D / Math.PI, 1));
        System.Console.Out.WriteLine("å—ƒ~Šp‚Ì‰e‹¿‚ª‚È‚­‚È‚éŠp“x(‰¡) [deg]:" + DispFormat.DoubleFormat(wf_beta2 * 180.0D / Math.PI, 1));
        System.Console.Out.WriteLine("å—ƒ~Šp‚ÌŠ®‘S‰e‹¿Šp(c) [deg]:" + DispFormat.DoubleFormat(wf_theta1 * 180.0D / Math.PI, 1));
        System.Console.Out.WriteLine("å—ƒ~Šp‚Ì‰e‹¿‚ª‚È‚­‚È‚éŠp“x(c) [deg]:" + DispFormat.DoubleFormat(wf_theta2 * 180.0D / Math.PI, 1));
    }

    public void Calc_interference(AirPlane ap, double dt)
    {
        int lr;
        for (lr = 0; lr <= 1; lr++)
        {
            htail_dv[lr].SetVec(0.0D, 0.0D, 0.0D);
            htail_k_q[lr] = 1.0D;
            vtail_dv[lr].SetVec(0.0D, 0.0D, 0.0D);
            vtail_k_q[lr] = 1.0D;
        }
        for (int i = 0; i < ap.n_fuselage; i++)
        {
            for (lr = 0; lr <= 1; lr++)
            {
                fuselage_dv[i, lr].SetVec(0.0D, 0.0D, 0.0D);
            }

        }

        Calc_pp(ap, dt);

        Calc_w(ap, dt);

        Calc_v(ap, dt);

        Calc_stall(ap);
    }

    public void Calc_w(AirPlane ap, double dt)
    {
        Vector3D v = new Vector3D();
        Matrix44 ryMat = new Matrix44();

        if ((ap.n_wing > 0) && (ap.n_htail > 0) && (ap.wing[0].n_wing_block > 0) && (ap.htail[0].n_wing_block > 0))
        {
            double s_sum;
            double cl_sum = s_sum = 0.0D;

            wf_b_epsilon_htail = wf_epsilon_htail;

            int i;
            for (i = 0; i < ap.n_wing; i++)
            {
                for (int lr = 0; lr < ap.wing[i].n_lr; lr++)
                {
                    for (int j = 0; j < ap.wing[i].n_wing_block; j++)
                    {
                        WingPlane wpi = ap.wing[i].wp[j, lr];
                        cl_sum += wpi.cl * wpi.s2;
                        s_sum += wpi.s2;
                    }
                }
            }

            for (i = 0; i < ap.n_aileron; i++)
            {
                for (int lr_0 = 0; lr_0 < 2; lr_0++)
                {
                    if (ap.aileron[i, lr_0].type != 0)
                    {
                        WingPlane wpi_1 = ap.aileron[i, lr_0].baseWingBlock;
                        cl_sum += ap.aileron[i, lr_0].d_cl * wpi_1.s2;
                        s_sum += wpi_1.s2;
                    }
                }
            }

            for (i = 0; i < ap.n_t_flap; i++)
            {
                for (int lr_2 = 0; lr_2 < 2; lr_2++)
                {
                    if (ap.t_flap[i, lr_2].type != 0)
                    {
                        WingPlane wpi_3 = ap.t_flap[i, lr_2].baseWingBlock;
                        cl_sum += ap.t_flap[i, lr_2].d_cl * wpi_3.s2;
                        s_sum += wpi_3.s2;
                    }
                }
            }

            for (i = 0; i < ap.n_l_flap; i++)
            {
                for (int lr_4 = 0; lr_4 < 2; lr_4++)
                {
                    if (ap.l_flap[i, lr_4].type != 0)
                    {
                        WingPlane wpi_5 = ap.l_flap[i, lr_4].baseWingBlock;
                        cl_sum += ap.l_flap[i, lr_4].d_cl * wpi_5.s2;
                        s_sum += wpi_5.s2;
                    }
                }
            }

            wing_cl = (cl_sum / s_sum);
            wing_ar = (ap.wing[0].AspectRatio(ap.pMotion.beta) * ap.wing[0].K_ground_effect(ap.pMotion.beta, ap.pMotion.wpos.y));
            wf_epsilon = (2.0D * wing_cl / Math.PI / wing_ar);

            double alpha = Math.Abs(ap.pMotion.alpha / 2.0D - angle_t.GetValue());
            double beta = Math.Abs(ap.pMotion.beta);
            wf_epsilon_htail = Jp.Maker1.Sim.Tools.Tool.Hokan2_2D(wf_theta1, wf_beta1, wf_epsilon, wf_theta2, wf_beta2, 0.0D, alpha, beta);
            wf_d_epsilon_htail = ((wf_epsilon_htail - wf_b_epsilon_htail) / dt);

            double v0 = ap.pMotion.vc.Length();
            if (v0 > 5.0D)
                v.SetVec(0.0D, 0.0D, -(v0 * wf_epsilon_htail - wf_d_epsilon_htail * lt / v0));
            else
            {
                v.SetVec(0.0D, 0.0D, -(v0 * wf_epsilon_htail));
            }
            ryMat.SetRyMat(-(wf_epsilon_htail + ap.pMotion.alpha));
            v = v.MultMat(ryMat);
            htail_dv[1] = htail_dv[1].Add(v);
            htail_dv[0] = htail_dv[0].Add(v);
        }
    }

    public void Calc_v(AirPlane ap, double dt)
    {
        if ((ap.n_wing > 0) && (ap.n_htail > 0)
                && (ap.wing[0].n_wing_block > 0)
                && (ap.htail[0].n_wing_block > 0))
        {
            double alpha = Math.Abs(ap.pMotion.alpha - angle_t.GetValue());
            double beta = Math.Abs(ap.pMotion.beta);
            double tmp108_105 = Jp.Maker1.Sim.Tools.Tool.Hokan2_2D(wt_theta1, wf_beta1,
                    wt_dv, wt_theta2, wf_beta2, 1.0D, alpha,
                    beta);

            htail_k_q[1] = tmp108_105;
            htail_k_q[0] = tmp108_105;
        }
    }

    public void Calc_pp(AirPlane ap, double dt)
    {
        Vector3D delta_v = new Vector3D();

        double v0 = ap.pMotion.vc.Length();

        int lr;
        for (lr = 0; lr < ap.htail[0].n_lr; lr++)
        {
            for (int k = 0; k < ap.n_powerPlant; k++)
            {
                PowerPlant pp = ap.powerPlant[k];

                if (pp.engine_type == 1)
                {
                    ReciprocatingEngine re = (ReciprocatingEngine)pp.engine;
                    double pr_l = re.wash_line.LengthPerpendicular(ap.htail[0].wp[0, lr].ac);
                    double v_dash2 = pr_k * Jp.Maker1.Sim.Tools.Tool.Hokan(re.diameter * pr_k_diameter_min / 2.0D, re.wash_v_dash, re.diameter * pr_k_diameter_max / 2.0D, 0.0D, pr_l);
                    delta_v = re.wash_direction.SclProd(v_dash2);
                    htail_dv[lr] = htail_dv[lr].Add(delta_v);
                }
            }
        }

        for (lr = 0; lr < ap.vtail[0].n_lr; lr++)
        {
            for (int k_0 = 0; k_0 < ap.n_powerPlant; k_0++)
            {
                PowerPlant pp_1 = ap.powerPlant[k_0];

                if (pp_1.engine_type == 1)
                {
                    ReciprocatingEngine re_2 = (ReciprocatingEngine)pp_1.engine;
                    double pr_l_3 = re_2.wash_line.LengthPerpendicular(ap.vtail[0].wp[0, lr].ac);
                    double v_dash2_4 = pr_k * Jp.Maker1.Sim.Tools.Tool.Hokan(re_2.diameter * pr_k_diameter_min / 2.0D, re_2.wash_v_dash, re_2.diameter * pr_k_diameter_max / 2.0D, 0.0D, pr_l_3);
                    delta_v = re_2.wash_direction.SclProd(v_dash2_4);
                    vtail_dv[lr] = vtail_dv[lr].Add(delta_v);
                }
            }
        }

        for (int i = 0; i < ap.n_fuselage; i++)
            for (lr = 0; lr <= 1; lr++)
                if (ap.fuslage[i, lr].flag != 0)
                    for (int k_5 = 0; k_5 < ap.n_powerPlant; k_5++)
                    {
                        PowerPlant pp_6 = ap.powerPlant[k_5];

                        if (pp_6.engine_type == 1)
                        {
                            ReciprocatingEngine re_7 = (ReciprocatingEngine)pp_6.engine;
                            double pr_l_8 = re_7.wash_line.LengthPerpendicular(ap.fuslage[i, lr].ac);
                            double v_dash2_9 = pr_k * Jp.Maker1.Sim.Tools.Tool.Hokan(re_7.diameter * pr_k_diameter_min / 2.0D, re_7.wash_v_dash, re_7.diameter * pr_k_diameter_max / 2.0D, 0.0D, pr_l_8);
                            vtail_dv[lr] = vtail_dv[lr].Add(delta_v);
                            fuselage_dv[i, lr] = fuselage_dv[i, lr].Add(delta_v);
                        }
                    }
    }

    public void Calc_stall(AirPlane ap)
    {
        Angle alpha = new Angle(0);

        alpha.SetValue(System.Math.Atan2(ap.pMotion.vc.z, ap.pMotion.vc.x));

        double k_S_wg = 1.0D;
        if (ap.max_k_stall_wing >= 1.0D)
        {
            k_S_wg = k_SVtail_Wing_stall.F(alpha.GetValue());
        }

        double k_S_ht = 1.0D;
        if (ap.max_k_stall_htail >= 1.0D)
        {
            k_S_ht = k_SVtail_Htail_stall.F(alpha.GetValue());
        }
        double ret = k_S_wg;
        if (k_S_wg > k_S_ht)
            ret = k_S_ht;
        double tmp111_109 = ret;
        vtail_k_S[1] = tmp111_109;
        vtail_k_S[0] = tmp111_109;
    }
}
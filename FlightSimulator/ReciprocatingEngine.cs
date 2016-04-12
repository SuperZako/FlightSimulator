
    using Jp.Maker1.Sim.Tools;
    using Jp.Maker1.Vsys3.Tools;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.CompilerServices;

public class ReciprocatingEngine : Engine
{
    public ReciprocatingEngine()
    {
        ac = new Vector3D();
        vd = new Vector3D();
        h = 0.0D;
        v0 = 0.0D;
        enginePower0 = 0.0D;
        thrust = 0.0D;
        fv = new Vector3D();
        tv = new Vector3D();
        wash_direction = new Vector3D(1.0D, 0.0D, 0.0D);
        wash_line = new Segment3D();
    }

    public PowerPlant pp;
    public String engineName;
    public double p_ck;
    public double p_r;
    public double p_k;
    public double h_k;
    public double p_k2;
    public double h_k2;
    public double h_k2_shift;
    public String propellerName;
    public double diameter;
    public double fm;
    public double epsilon;
    public double area_prop;
    public Vector3D ac;
    public Vector3D vd;
    public double h;
    public double v0;
    public double enginePower0;
    public LowPassFilter1 enginePower;
    public double thrust;
    public Vector3D fv;
    public Vector3D tv;

    public Vector3D wash_direction;
    public Segment3D wash_line;
    public double wash_v_dash;

    public virtual void Init()
    {
        area_prop = (diameter * Math.PI);
        enginePower = new LowPassFilter1(Jp.Maker1.Sim.Tools.LowPassFilter1.TimeConstantFrom95pTime(1.0D), 0.0D);
    }

    public virtual void Print()
    {
        System.Console.Out.WriteLine("エンジン名称: " + engineName);
        System.Console.Out.WriteLine("離昇出力 [W]: " + p_r);
        System.Console.Out.WriteLine("地上公称出力[W]: " + p_ck);
        System.Console.Out.WriteLine("公称出力(1速) [W]: " + h_k);
        System.Console.Out.WriteLine("公称高度(1速) [m]: " + p_k);
        System.Console.Out.WriteLine("2速切替高度 [m]: " + h_k2_shift);
        System.Console.Out.WriteLine("公称出力(2速) [W]: " + h_k2);
        System.Console.Out.WriteLine("公称高度(2速) [m]: " + p_k2);

        System.Console.Out.WriteLine("プロペラ名称: " + propellerName);
        System.Console.Out.WriteLine("直径 [m]: " + diameter);
        System.Console.Out.WriteLine("Figure of merit FM : " + fm);
        System.Console.Out.WriteLine("推進効率/理想推進効率: " + epsilon);

        System.Console.Out.WriteLine("プロペラ面積: " + area_prop);
        System.Console.Out.WriteLine("---------------------------------------------");
    }

    public virtual void Calc_dynamics(AirPlane ap, double dt)
    {
        ac = pp.p.Sub(ap.inp.cg);
        vd = Dynamics.VWithRot(ap.pMotion.vc, ap.pMotion.omega, ac);
        v0 = pp.v.NmlVec().DotProd(vd);
        h = ap.pMotion.wpos.y;

        enginePower0 = Calc_engine_power(pp.throttle, h, ap);
        enginePower.Update(enginePower0, dt);

        if (v0 < 0.5D)
            thrust = Calc_T0(enginePower.Value(), ap.atmos.rho);
        else if (v0 < 2.6D)
            thrust = Jp.Maker1.Sim.Tools.Tool.Hokan(0.5D, Calc_T0(enginePower.Value(), ap.atmos.rho), 2.6D, Calc_T(enginePower.Value(), ap.atmos.rho, 2.6D), v0);
        else
        {
            thrust = Calc_T(enginePower.Value(), ap.atmos.rho, v0);
        }

        fv = pp.v.NmlVec().SclProd(thrust);
        tv.SetVec(0.0D, 0.0D, 0.0D);

        Calc_wash(ap);
    }

    public virtual Vector3D GetForce()
    {
        return fv;
    }

    public virtual Vector3D GetTorque()
    {
        return tv;
    }

    public virtual Vector3D GetPos()
    {
        return ac;
    }

    public double Calc_engine_power(double th, double h_0, AirPlane ap)
    {
        double h_k_dash = Isa.Giopotential_altitude(h_k);
        double h_k2_dash = Isa.Giopotential_altitude(h_k2);
        double h_k2_shift_dash = Isa.Giopotential_altitude(h_k2_shift);
        double p;

        if (h_0 <= h_k)
        {
            p = (p_k - p_ck) * h_0 / h_k + p_ck;
        }
        else
        {

            if (h_0 <= h_k2_shift)
            {
                p = p_k * ap.atmos.p / Isa.Pressure(h_k_dash) * Math.Sqrt(Isa.Temperature(h_k_dash) / ap.atmos.t);
            }
            else
            {

                if (h_0 <= h_k2)
                {
                    double p_k2_shift = p_k * Isa.Pressure(h_k2_shift_dash) / Isa.Pressure(h_k_dash) * Math.Sqrt(Isa.Temperature(h_k_dash) / Isa.Temperature(h_k2_shift_dash));
                    p = (p_k2 - p_k2_shift) * (h_0 - h_k2_shift) / (h_k2 - h_k2_shift) + p_k2_shift;
                }
                else
                {
                    p = p_k2 * ap.atmos.p / Isa.Pressure(h_k2_dash) * Math.Sqrt(Isa.Temperature(h_k2_dash) / ap.atmos.t);
                }
            }
        }
        p *= th;
        if (th == 1.0D)
        {
            if (h_0 < 15.0D)
                p = p_r;
            else if (h_0 < 30.0D)
            {
                p = Jp.Maker1.Sim.Tools.Tool.Hokan(15.0D, p_r, 30.0D, p, h_0);
            }
        }
        return p;
    }

    public double Calc_T0(double p, double rho)
    {
        double t0 = Math.Pow(2.0D * rho * area_prop, 0.3333333333333333D) * Math.Pow(fm * p, 0.6666666666666666D);

        return t0;
    }

    public double Calc_T(double p, double rho, double v)
    {
        if (p == 0.0D)
            return 0.0D;

        double a = rho * v0 * v0 * v0 / 4.0D / epsilon * area_prop / p;
        double x = MathTool.Log10(a);
        double y = -1.091449796E-005D * x * x * x * x * x * x - 5.230094596E-005D * x * x * x * x * x + 0.0009021150719700001D * x * x * x * x + 0.00305356792141D * x * x * x - 0.04105046479035D * x * x - 0.90453857341224D * x - 0.35712855489111D;
        double tc = Math.Pow(10.0D, y);
        return 0.5D * rho * v * v * area_prop * tc;
    }

    public void Calc_wash(AirPlane ap)
    {
        double v0_0 = ap.pMotion.vc.Length();
        Matrix44 mtemp = new Matrix44();

        wash_v_dash = (-v0_0 + Math.Sqrt(v0_0 * v0_0 + 2.0D * thrust / ap.atmos.rho / area_prop));

        wash_direction = ap.pMotion.vc.Add(pp.v.NmlVec().SclProd(wash_v_dash)).NmlVec();
        if (wash_direction.Length() == 0.0D)
        {
            wash_direction.SetVec(pp.v.NmlVec());
        }

        wash_line.SetP0(ac);
        wash_line.SetP1(ac.Add(wash_direction));
    }
}
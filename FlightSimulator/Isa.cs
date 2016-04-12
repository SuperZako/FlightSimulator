

    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.CompilerServices;


public class Isa
{
    public double h;
    public double tc;
    public double t;
    public double p;
    public double rho;
    public double a;
    public double mu;
    public double nu;

    public Isa()
    {
        Init(0.0D);
    }

    public Isa(double h_0)
    {
        Init(h_0);
    }

    public void Init(double hIn)
    {
        h = hIn;
        t = Temperature(h);
        tc = Jp.Maker1.Sim.Tools.UnitConvert.Ktot(t);
        p = Pressure(h);
        rho = Density(h);
        a = Sound_velocity(h);
        mu = Viscosity(h);
        nu = Kinematic_viscosity(h);
    }

    public override String ToString()
    {
        String ret = "H:" + DispFormat.DoubleFormat(h, 5, 0) + "[m'] "
                + "t:" + DispFormat.DoubleFormat(tc, 4, 2) + "[Åé] " + "T:"
                + DispFormat.DoubleFormat(t, 4, 2) + "[K] " + "p:"
                + DispFormat.DoubleFormat(p, 6, 0) + "[Pa] " + "Éœ:"
                + DispFormat.DoubleFormat(rho, 1, 4) + "[kg/m3] " + "a:"
                + DispFormat.DoubleFormat(a, 1, 4) + "[m/s] " + "É :"
                + DispFormat.DoubleFormat(mu * 100000.0D, 1, 4)
                + "E5[PaÅEs] " + "ÉÀ:"
                + DispFormat.DoubleFormat(nu * 100000.0D, 1, 4)
                + "E5[m2/s] ";

        return ret;
    }

    public static double Giopotential_altitude(double z)
    {
        double a_0 = 1.0D + z / 6370000.0D;
        double h_1;

        if (a_0 != 0.0D)
            h_1 = z / a_0;
        else
        {
            h_1 = -1.0D;
        }
        return h_1;
    }

    public static double Temperature(double h_0)
    {
        double t_1;

        if (h_0 < 11000.0D)
            t_1 = 288.14999999999998D - 0.0065D * h_0;
        else
        {
            t_1 = 216.65000000000001D;
        }
        return t_1;
    }

    public static double Pressure(double h_0)
    {
        double n = 5.255823837409817D;
        double p_1;

        if (h_0 < 11000.0D)
        {
            p_1 = 101325.0D * Math.Pow(1.0D - 0.0065D * h_0 / 288.14999999999998D, n);
        }
        else
        {
            double past = 101325.0D * Math.Pow(0.7518653479090751D, n);
            p_1 = past * Math.Exp(0.0001576868448795929D * (11000.0D - h_0));
        }
        return p_1;
    }

    public static double Density(double h_0)
    {
        double n = 5.255823837409817D;
        double rho_1;

        if (h_0 < 11000.0D)
        {
            rho_1 = 1.225D * Math.Pow(1.0D - 0.0065D * h_0 / 288.14999999999998D, n - 1.0D);
        }
        else
        {
            double rhoast = 1.225D * Math.Pow(0.7518653479090751D, n - 1.0D);
            rho_1 = rhoast * Math.Exp(0.0001576868448795929D * (11000.0D - h_0));
        }
        return rho_1;
    }

    public static double Sound_velocity(double h_0)
    {
        double t_1 = Temperature(h_0);
        double a_2 = Math.Sqrt(401.87419999999997D * t_1);
        return a_2;
    }

    public static double Viscosity(double h_0)
    {
        double t_1 = Temperature(h_0);
        double mu_2 = 1.7932E-005D * Math.Pow(t_1 / 288.14999999999998D, 1.5D)
                * 398.54999999999995D / (t_1 + 110.40000000000001D);
        return mu_2;
    }

    public static double Kinematic_viscosity(double h_0)
    {
        double mu_1 = Viscosity(h_0);
        double rho_2 = Density(h_0);
        double nu_3 = mu_1 / rho_2;
        return nu_3;
    }

    public static double Mach_number(double h_0, double v)
    {
        return v / Sound_velocity(h_0);
    }

    public static double Reynolds_number(double h_0, double v, double l)
    {
        return v * l / Kinematic_viscosity(h_0);
    }

    public static double Dynamic_pressure(double h_0, double v)
    {
        return 0.5D * Density(h_0) * v * v;
    }

    public static double Stagnetion_temp(double h_0, double v)
    {
        return Temperature(h_0) * (1.0D + 0.2D * Mach_number(h_0, v));
    }

    public static double Friction_coefficient(double re)
    {
        double cf;

        if (re <= 530000.0D)
            cf = 1.328D / Math.Sqrt(re);
        else
        {
            cf = 0.455D / Math.Pow(MathTool.Log10(re), 2.58D) - 1708.5D / re;
        }
        return cf;
    }
}
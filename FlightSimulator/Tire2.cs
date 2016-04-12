
	
	using Jp.Maker1.Sim.Tools;
	using Jp.Maker1.Vsys3.Tools;
	using System;
	using System.Collections;
	using System.ComponentModel;
	using System.IO;
	using System.Runtime.CompilerServices;

public class Tire2
{
    internal double r;
    internal double t_b_max;
    internal double mu_load0;

    internal Vector3D vd;
    internal double fz;
    internal double muMax;
    internal double muX;
    internal double muY;
    internal double s;
    internal double beta;
    internal double fx;
    internal double fy;

    public Tire2(double rIn, double t_b_maxIn)
    {
        mu_load0 = 0.01D;
        vd = new Vector3D(0.0D, 0.0D, 0.0D);
        fz = 0.0D;
        muMax = 0.0D;
        muX = 0.0D;
        muY = 0.0D;
        s = 0.0D;
        beta = 0.0D;
        fx = 0.0D;
        fy = 0.0D;
        r = rIn;
        t_b_max = t_b_maxIn;
    }

    public void Print()
    {
        System.Console.Out.WriteLine("車輪半径 [m]: " + r);
        System.Console.Out.WriteLine("最大ブレーキトルク [N・m]: " + t_b_max);
        System.Console.Out.WriteLine("---------------------------------------------");
    }

    public void Update(Vector3D vIn, double fzIn, double muMaxIn, double b)
    {
        vd.SetVec(vIn);
        vd.z = 0.0D;

        fz = fzIn;

        Bearing br = new Bearing(vd.R2l());
        beta = br.yaw.GetValue();

        if (fz > 0.0D)
        {
            s = (t_b_max / r * 0.1D / fz * b);
            if (s > 1.0D)
                s = 1.0D;
        }
        else
        {
            s = 0.0D;
        }

        muMax = muMaxIn;
        if (beta < 1.570796326794897D)
        {
            muX = (-(muMax * (Jp.Maker1.Fsim.TireMu.X(s, beta) + mu_load0)));
            muY = (-muMax * Jp.Maker1.Fsim.TireMu.Y(s, beta));
        }
        else if (beta < Math.PI)
        {
            muX = (muMax * (Jp.Maker1.Fsim.TireMu.X(s,
                    Math.PI - beta) + mu_load0));
            muY = (-muMax * Jp.Maker1.Fsim.TireMu.Y(s,
                    Math.PI - beta));
        }
        else if (beta < 4.71238898038469D)
        {
            muX = (muMax * (Jp.Maker1.Fsim.TireMu.X(s, -Math.PI
                    + beta) + mu_load0));
            muY = (muMax * Jp.Maker1.Fsim.TireMu.Y(s, -Math.PI
                    + beta));
        }
        else
        {
            muX = (-(muMax * (Jp.Maker1.Fsim.TireMu.X(s,
                    6.283185307179586D - beta) + mu_load0)));
            muY = (muMax * Jp.Maker1.Fsim.TireMu.Y(s,
                    6.283185307179586D - beta));
        }

        fx = (muX * fz);
        fy = (muY * fz);
    }
}
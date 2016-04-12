
    using Jp.Maker1.Sim.Tools;
    using Jp.Maker1.Vsys3.Tools;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.CompilerServices;

public class Wing
{
    public Wing()
    {
        flag = 0;
        n_lr = 0;
        n_wing_block = 0;
        fv = new Vector3D();
        tv = new Vector3D();
    }

    public int flag;
    public String name;
    public int hv_arrangement;
    public double s2;
    public Function2D b2_func;
    public double k_ar;
    public int n_lr;
    public int n_wing_block;
    public WingPlane[,] wp;
    public Vector3D fv;
    public Vector3D tv;

    public void Init()
    {
    }

    public void Print()
    {
        if (flag == 1)
        {
            System.Console.Out.WriteLine("翼名称: " + name);
            System.Console.Out.WriteLine("水平/垂直配置: " + hv_arrangement);
            System.Console.Out.WriteLine("1枚分の3面図投影翼面積 [m2]: " + s2);
            System.Console.Out.WriteLine("1枚分の翼幅 [m]: ");
            b2_func.Print();
            System.Console.Out.WriteLine("有効アスペクト比の計算係数: " + k_ar);
            System.Console.Out.WriteLine("---------------------------------------------");
            for (int lr = 0; lr < n_lr; lr++)
            {
                System.Console.Out.WriteLine(AirPlane.lrName[lr]);
                System.Console.Out.WriteLine("---------------------------------------------");
                for (int i = 0; i < n_wing_block; i++)
                {
                    wp[i, lr].Print();
                    System.Console.Out.WriteLine("---------------------------------------------");
                }
            }
        }
    }

    internal void Calc_dynamics(AirPlane ap, Vector3D[] dv, double[] k_q, double[] k_S)
    {
        fv.SetVec(0.0D, 0.0D, 0.0D);
        tv.SetVec(0.0D, 0.0D, 0.0D);
        for (int lr = 0; lr < n_lr; lr++)
            for (int i = 0; i < n_wing_block; i++)
            {
                WingPlane wpi = wp[i, lr];
                if (wpi.flag != 0)
                {
                    wpi.Calc_dynamics(lr, null, ap, dv[lr], k_q[lr], k_S[lr]);
                    fv = fv.Add(wpi.fv);
                    tv = tv.Add(wpi.tv);
                }
            }
    }

    public double Area()
    {
        return s2 * n_lr;
    }

    public double Span(double beta)
    {
        if (beta < -1.570796326794897D)
            beta += Math.PI;
        if (beta > 1.570796326794897D)
            beta -= Math.PI;
        double b;

        if (hv_arrangement == 1)
            b = b2_func.F(beta);
        else
        {
            b = b2_func.F(beta) * n_lr;
        }
        return b;
    }

    public double AspectRatio(double beta)
    {
        double s = Area();
        double b = Span(beta);
        return k_ar * b * b / s;
    }

    public double K_ground_effect(double beta, double h)
    {
        if (hv_arrangement == 1)
            return 1.0D;
        double b = Span(beta);
        double a = 33.0D * Math.Pow(h / b, 1.5D);
        return (1.0D + a) / a;
    }
}
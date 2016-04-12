namespace Jp.Maker1.Sim.Tools
{

    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class Function3D
    {
        public Function2D[] fy;
        public double[] y;

        public Function3D(double[] yIn, Function2D[] fyIn)
        {
            int n = yIn.Length;
            if (n > fyIn.Length)
                n = fyIn.Length;

            y = new double[n];
            fy = new Function2D[n];
            for (int i = 0; i < n; i++)
            {
                y[i] = yIn[i];
                fy[i] = new Function2D(fyIn[i].x, fyIn[i].y);
            }
        }

        public Function3D(double[] xIn, double[] yIn, double[][] z)
        {
            y = new double[yIn.Length];
            fy = new Function2D[yIn.Length];
            double[] fy1 = new double[xIn.Length];

            for (int i = 0; i < yIn.Length; i++)
            {
                y[i] = yIn[i];
                for (int j = 0; j < xIn.Length; j++)
                {
                    fy1[j] = z[i][j];
                }
                fy[i] = new Function2D(xIn, fy1);
            }
        }

        public double F(double xIn, double yIn)
        {
            int n = y.Length;

            if (n <= 0)
                return 0.0D;

            if (yIn < y[0])
                return fy[0].F(xIn);
            if (yIn >= y[(n - 1)])
                return fy[(n - 1)].F(xIn);

            for (int i = 0; i < n - 1; i++)
            {
                if ((yIn >= y[i]) && (yIn < y[(i + 1)]))
                {
                    double zi = fy[i].F(xIn);
                    double zi1 = fy[(i + 1)].F(xIn);

                    return zi + (zi1 - zi) * (yIn - y[i]) / (y[(i + 1)] - y[i]);
                }
            }
            return 0.0D;
        }

        public void Print()
        {
            for (int i = 0; i < y.Length; i++)
            {
                for (int j = 0; j < fy[i].GetN(); j++)
                {
                    System.Console.Out.Write("f(" + fy[i].x[j] + "," + y[i] + ")=" + fy[i].y[j]);
                    if (i < fy[i].GetN() - 1)
                    {
                        System.Console.Out.Write(", ");
                    }
                }
                System.Console.Out.WriteLine("");
            }
        }

        public double Fo(double xIn, double yIn)
        {
            int n = y.Length;

            if (n <= 0)
                return 0.0D;

            if (yIn < y[0])
            {
                if (n == 1)
                    return fy[0].Fo(xIn);
                if (y[0] == y[1])
                {
                    return fy[0].Fo(xIn);
                }
                double zi = fy[0].Fo(xIn);
                double zi1 = fy[1].Fo(xIn);
                return zi + (zi1 - zi) * (yIn - y[0]) / (y[1] - y[0]);
            }

            if (yIn >= y[(n - 1)])
            {
                if (n == 1)
                    return fy[(n - 1)].Fo(xIn);
                if (y[(n - 1)] == y[(n - 2)])
                {
                    return fy[(n - 1)].Fo(xIn);
                }
                double zi_0 = fy[(n - 2)].Fo(xIn);
                double zi1_1 = fy[(n - 1)].Fo(xIn);
                return zi_0 + (zi1_1 - zi_0) * (yIn - y[(n - 2)]) / (y[(n - 1)] - y[(n - 2)]);
            }

            for (int i = 0; i < n - 1; i++)
            {
                if ((yIn >= y[i]) && (yIn < y[(i + 1)]))
                {
                    double zi_2 = fy[i].Fo(xIn);
                    double zi1_3 = fy[(i + 1)].Fo(xIn);

                    return zi_2 + (zi1_3 - zi_2) * (yIn - y[i]) / (y[(i + 1)] - y[i]);
                }
            }
            return 0.0D;
        }
    }
}
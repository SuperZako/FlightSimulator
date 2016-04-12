    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.CompilerServices;


public class Vector3D
{
    public double x;
    public double y;
    public double z;

    public Vector3D()
    {
        x = (y = z = 0.0D);
    }

    public Vector3D(double x_0, double y_1, double z_2)
    {
        SetVec(x_0, y_1, z_2);
    }

    public Vector3D(Vector3D v)
    {
        SetVec(v);
    }

    public Vector3D Copy()
    {
        Vector3D ret = new Vector3D();
        ret.x = x;
        ret.y = y;
        ret.z = z;
        return ret;
    }

    public String ToString(String pref, String mid, String suf)
    {
        return pref + DispFormat.DoubleFormat(x, 3) + mid + DispFormat.DoubleFormat(y, 3) + mid + DispFormat.DoubleFormat(z, 3) + suf;
    }

    public override String ToString()
    {
        return ToString("[", " ", "]");
    }

    public String ToStringPos()
    {
        return ToString("(", ",", ")");
    }

    public void PrintPos()
    {
        DispVec("( ", ", ", " )");
    }

    public void PrintVec()
    {
        DispVec("[ ", " ", " ]");
    }

    public void DispVec(String pref, String mid, String suf)
    {
        System.Console.Out.Write(pref + DispFormat.DoubleFormat(x, 3) + mid + DispFormat.DoubleFormat(y, 3) + mid + DispFormat.DoubleFormat(z, 3) + suf);
    }

    public void PrintVecln()
    {
        PrintVec();
        System.Console.Out.WriteLine("");
    }

    public void SetVec(double x_0, double y_1, double z_2)
    {
        x = x_0;
        y = y_1;
        z = z_2;
    }

    public void SetVec(Vector3D v)
    {
        x = v.x;
        y = v.y;
        z = v.z;
    }

    public bool Equals(Vector3D v)
    {
        return (x == v.x) && (y == v.y) && (z == v.z);
    }

    public Vector3D MultMat(Matrix44 m)
    {
        Vector3D ret = new Vector3D();

        ret.x = (x * m.element[0, 0] + y * m.element[1, 0] + z * m.element[2, 0] + m.element[3, 0]);
        ret.y = (x * m.element[0, 1] + y * m.element[1, 1] + z * m.element[2, 1] + m.element[3, 1]);
        ret.z = (x * m.element[0, 2] + y * m.element[1, 2] + z * m.element[2, 2] + m.element[3, 2]);

        return ret;
    }

    public Vector3D Add(Vector3D v)
    {
        Vector3D ret = new Vector3D();

        ret.SetVec(x + v.x, y + v.y, z + v.z);

        return ret;
    }

    public Vector3D Sub(Vector3D v)
    {
        Vector3D ret = new Vector3D();

        ret.SetVec(x - v.x, y - v.y, z - v.z);

        return ret;
    }

    public Vector3D SclProd(double k)
    {
        Vector3D ret = new Vector3D(x * k, y * k, z * k);

        return ret;
    }

    public Vector3D CrsProd(Vector3D v)
    {
        Vector3D ret = new Vector3D();

        double xr = y * v.z - z * v.y;
        double yr = z * v.x - x * v.z;
        double zr = x * v.y - y * v.x;
        ret.SetVec(xr, yr, zr);

        return ret;
    }

    public double DotProd(Vector3D v)
    {
        return x * v.x + y * v.y + z * v.z;
    }

    public Vector3D NmlVec()
    {
        Vector3D ret = new Vector3D();

        double length = Length();
        if (length == 0.0D)
            ret.SetVec(0.0D, 0.0D, 0.0D);
        else
        {
            ret = SclProd(1.0D / length);
        }

        return ret;
    }

    public double VecAngle(Vector3D v)
    {
        double denominator = Length() * v.Length();
        double angle;

        if (denominator > 0.0D)
        {
            double p = DotProd(v) / denominator;
            if (p > 1.0D)
                p = 1.0D;
            if (p < -1.0D)
                p = -1.0D;
            angle = System.Math.Acos(p);
        }
        else
        {
            angle = 0.0D;
        }
        return angle;
    }

    public double Length()
    {
        return Math.Sqrt(x * x + y * y + z * z);
    }

    public Vector3D L2r()
    {
        return new Vector3D(z, x, -y);
    }

    public Vector3D R2l()
    {
        return new Vector3D(y, -z, x);
    }

    public Vector3D L2rAnglarVelocity()
    {
        return new Vector3D(-z, -x, y);
    }

    public Vector3D R2lAnglarVelocity()
    {
        return new Vector3D(-y, z, -x);
    }
}
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.CompilerServices;


public class Matrix44
{
    public double[,] element;

    public Matrix44()
    {
        element = new double[4, 4];
        //element[0] = new double[4];
        //element[1] = new double[4];
        //element[2] = new double[4];
        //element[3] = new double[4];

        SetUMat();
    }

    private double Det2(double t11, double t12, double t21, double t22)
    {
        return t11 * t22 - t12 * t21;
    }

    private double Det3(double t11, double t12, double t13, double t21, double t22, double t23, double t31, double t32, double t33)
    {
        return t11 * t22 * t33 + t12 * t23 * t31 + t13 * t21 * t32 - t13 * t22 * t31 - t12 * t21 * t33 - t11 * t23 * t32;
    }

    public void Print()
    {
        PrintRow(0, "„¡", "„¢");
        PrintRow(1, "„ ", "„ ");
        PrintRow(2, "„ ", "„ ");
        PrintRow(3, "„¤", "„£");
    }

    public void PrintRow(int row, String pref, String suf)
    {
        if ((row < 0) || (row > 3))
        {
            System.Console.Out.WriteLine("dispRow() out of bounds (row=" + row + ")");
        }
        else
        {
            System.Console.Out.Write(pref);
            for (int i = 0; i < 4; i++)
            {
                String s = DispFormat.DoubleFormat(element[row, i], 6, 3);
                System.Console.Out.Write(s + " ");
            }
            System.Console.Out.WriteLine(suf);
        }
    }

    public Matrix44 DtMat()
    {
        Matrix44 m = new Matrix44();

        m.SetMat(this);
        double tmp37_36 = (m.element[3, 2] = 0.0D);
        m.element[3, 1] = tmp37_36;
        m.element[3, 0] = tmp37_36;
        return m;
    }

    public Matrix44 InvMat()
    {
        Matrix44 inv = new Matrix44();

        double t11 = element[0, 0];
        double t12 = element[0, 1];
        double t13 = element[0, 2];
        double t14 = element[0, 3];
        double t21 = element[1, 0];
        double t22 = element[1, 1];
        double t23 = element[1, 2];
        double t24 = element[1, 3];
        double t31 = element[2, 0];
        double t32 = element[2, 1];
        double t33 = element[2, 2];
        double t34 = element[2, 3];
        double t41 = element[3, 0];
        double t42 = element[3, 1];
        double t43 = element[3, 2];
        double t44 = element[3, 3];

        double det = Det3(t11, t12, t13, t21, t22, t23, t31, t32, t33);
        if (det == 0.0D)
        {
            inv.SetZMat();
            return inv;
        }

        inv.element[0, 0] = (Det2(t22, t23, t32, t33) / det);
        inv.element[0, 1] = (-Det2(t12, t13, t32, t33) / det);
        inv.element[0, 2] = (Det2(t12, t13, t22, t23) / det);
        inv.element[0, 3] = 0.0D;

        inv.element[1, 0] = (-Det2(t21, t23, t31, t33) / det);
        inv.element[1, 1] = (Det2(t11, t13, t31, t33) / det);
        inv.element[1, 2] = (-Det2(t11, t13, t21, t23) / det);
        inv.element[1, 3] = 0.0D;

        inv.element[2, 0] = (Det2(t21, t22, t31, t32) / det);
        inv.element[2, 1] = (-Det2(t11, t12, t31, t32) / det);
        inv.element[2, 2] = (Det2(t11, t12, t21, t22) / det);
        inv.element[2, 3] = 0.0D;

        inv.element[3, 0] = (-Det3(t21, t22, t23, t31, t32, t33, t41, t42, t43) / det);
        inv.element[3, 1] = (Det3(t11, t12, t13, t31, t32, t33, t41, t42, t43) / det);
        inv.element[3, 2] = (-Det3(t11, t12, t13, t21, t22, t23, t41, t42, t43) / det);
        inv.element[3, 3] = 1.0D;

        return inv;
    }

    public Matrix44 MultMat(Matrix44 mat)
    {
        Matrix44 ret = new Matrix44();

        for (int i = 0; i <= 3; i++)
        {
            for (int j = 0; j <= 3; j++)
            {
                double temp = 0.0D;
                for (int k = 0; k <= 3; k++)
                {
                    temp += element[i, k] * mat.element[k, j];
                }
                ret.element[i, j] = temp;
            }
        }

        return ret;
    }

    public Matrix44 NtMat()
    {
        Matrix44 m = new Matrix44();
        Matrix44 ret = new Matrix44();

        m = TranspMat();
        double tmp45_44 = (m.element[2, 3] = 0.0D);
        m.element[1, 3] = tmp45_44;
        m.element[0, 3] = tmp45_44;

        ret = m.InvMat();

        return ret;
    }

    public void SetMat(Matrix44 mat)
    {
        for (int i = 0; i < 4; i++)
            for (int j = 0; j < 4; j++)
                element[i, j] = mat.element[i, j];
    }

    public void SetRefxyMat()
    {
        SetSMat(1.0D, 1.0D, -1.0D);
    }

    public void SetRefxzMat()
    {
        SetSMat(1.0D, -1.0D, 1.0D);
    }

    public void SetRefyzMat()
    {
        SetSMat(-1.0D, 1.0D, 1.0D);
    }

    public void SetRMat(double rx, double ry, double rz)
    {
        Matrix44 rxm = new Matrix44();
        Matrix44 rym = new Matrix44();
        Matrix44 rzm = new Matrix44();

        rxm.SetRxMat(rx);
        rym.SetRyMat(ry);
        rzm.SetRzMat(rz);

        SetMat(rxm.MultMat(rym).MultMat(rzm));
    }

    public void SetRMatL_RPY(double roll, double pitch, double yaw)
    {
        Matrix44 rxm = new Matrix44();
        Matrix44 rym = new Matrix44();
        Matrix44 rzm = new Matrix44();

        rxm.SetRxMat(pitch);
        rym.SetRyMat(yaw);
        rzm.SetRzMat(roll);

        SetMat(rzm.MultMat(rxm).MultMat(rym));
    }

    public void SetRxMat(double rx)
    {
        SetUMat();
        element[1, 1] = System.Math.Cos(rx);
        element[1, 2] = System.Math.Sin(rx);
        element[2, 1] = (-System.Math.Sin(rx));
        element[2, 2] = System.Math.Cos(rx);
    }

    public void SetRyMat(double ry)
    {
        SetUMat();
        element[0, 0] = System.Math.Cos(ry);
        element[0, 2] = (-System.Math.Sin(ry));
        element[2, 0] = System.Math.Sin(ry);
        element[2, 2] = System.Math.Cos(ry);
    }

    public void SetRzMat(double rz)
    {
        SetUMat();
        element[0, 0] = System.Math.Cos(rz);
        element[0, 1] = System.Math.Sin(rz);
        element[1, 0] = (-System.Math.Sin(rz));
        element[1, 1] = System.Math.Cos(rz);
    }

    public void SetSMat(double sx, double sy, double sz)
    {
        SetUMat();
        element[0, 0] = sx;
        element[1, 1] = sy;
        element[2, 2] = sz;
    }

    public void SetTMat(double tx, double ty, double tz)
    {
        SetUMat();
        element[3, 0] = tx;
        element[3, 1] = ty;
        element[3, 2] = tz;
    }

    public void SetUMat()
    {
        for (int i = 0; i <= 3; i++)
            for (int j = 0; j <= 3; j++)
                element[i, j] = 0.0D;
        double tmp68_67 = (element[2, 2] = element[3, 3] = 1.0D);
        element[1, 1] = tmp68_67;
        element[0, 0] = tmp68_67;
    }

    public void SetZMat()
    {
        for (int i = 0; i <= 3; i++)
            for (int j = 0; j <= 3; j++)
                element[i, j] = 0.0D;
    }

    public Matrix44 TranspMat()
    {
        Matrix44 m = new Matrix44();

        for (int i = 0; i <= 3; i++)
        {
            for (int j = 0; j <= 3; j++)
            {
                m.element[i, j] = element[j, i];
            }
        }
        return m;
    }

    public void Set3ColVec(Vector3D v1, Vector3D v2, Vector3D v3)
    {
        SetUMat();
        element[0, 0] = v1.x;
        element[0, 1] = v2.x;
        element[0, 2] = v3.x;
        element[1, 0] = v1.y;
        element[1, 1] = v2.y;
        element[1, 2] = v3.y;
        element[2, 0] = v1.z;
        element[2, 1] = v2.z;
        element[2, 2] = v3.z;
    }

    public void Set3RowVec(Vector3D v1, Vector3D v2, Vector3D v3)
    {
        SetUMat();
        element[0, 0] = v1.x;
        element[0, 1] = v1.y;
        element[0, 2] = v1.z;
        element[1, 0] = v2.x;
        element[1, 1] = v2.y;
        element[1, 2] = v2.z;
        element[2, 0] = v3.x;
        element[2, 1] = v3.y;
        element[2, 2] = v3.z;
    }
}
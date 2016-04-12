
    using Jp.Maker1.Vsys3.Tools;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.CompilerServices;

public class Inertia
{
    public static int MAX_BLOCK = 30;

    public String name;

    public InertiaBlock[] block;

    public Vector3D cg;

    public double m;

    public double ixx;

    public double iyy;

    public double izz;

    public double ixy;

    public double iyz;

    public double izx;

    internal Matrix44 InertiaMat;

    internal Matrix44 InertiaInvMat;

    public Inertia(String nameIn)
    {
        name = "";
        block = new InertiaBlock[MAX_BLOCK];
        cg = new Vector3D(0.0D, 0.0D, 0.0D);
        m = 0.0D;
        ixx = 0.0D;
        iyy = 0.0D;
        izz = 0.0D;
        ixy = 0.0D;
        iyz = 0.0D;
        izx = 0.0D;
        InertiaMat = new Matrix44();
        InertiaInvMat = new Matrix44();
        name = nameIn;
        for (int i = 0; i < MAX_BLOCK; i++)
        {
            block[i] = null;
        }
        InertiaMat.SetZMat();
        InertiaInvMat.SetZMat();
    }

    public void Update()
    {
        cg.SetVec(0.0D, 0.0D, 0.0D);
        m = 0.0D;
        int i;
        for (i = 0; i < MAX_BLOCK; i++)
        {
            InertiaBlock ib;
            if ((ib = block[i]) != null)
            {
                m += ib.m;
                cg = cg.Add(ib.M_cg());
            }

        }

        if (m > 0.0D)
        {
            cg = cg.SclProd(1.0D / m);
        }

        ixx = (iyy = izz = ixy = iyz = izx = 0.0D);
        for (i = 0; i < MAX_BLOCK; i++)
        {
            InertiaBlock ib_0;
            if ((ib_0 = block[i]) != null)
            {
                ixx += ib_0.Ixx(cg);
                iyy += ib_0.Iyy(cg);
                izz += ib_0.Izz(cg);
                ixy += ib_0.Ixy(cg);
                iyz += ib_0.Iyz(cg);
                izx += ib_0.Izx(cg);
            }

        }

        InertiaMat.SetUMat();
        InertiaMat.element[0, 0] = ixx;
        InertiaMat.element[1, 1] = iyy;
        InertiaMat.element[2, 2] = izz;
        double tmp330_329 = (-ixy);
        InertiaMat.element[1, 0] = tmp330_329;
        InertiaMat.element[0, 1] = tmp330_329;
        double tmp358_357 = (-izx);
        InertiaMat.element[2, 0] = tmp358_357;
        InertiaMat.element[0, 2] = tmp358_357;
        double tmp386_385 = (-iyz);
        InertiaMat.element[2, 1] = tmp386_385;
        InertiaMat.element[1, 2] = tmp386_385;

        InertiaInvMat = InertiaMat.InvMat();
    }

    public void SetBlock(int i, InertiaBlock ib)
    {
        block[i] = ib;
        Update();
    }

    public void DeleteBlock(int i)
    {
        block[i] = null;
        Update();
    }

    public void SetMass(int i, double m_0)
    {
        block[i].m = m_0;
        Update();
    }

    public void SetCG(int i, Vector3D cg_0)
    {
        block[i].cg = cg_0;
        Update();
    }

    public void Print()
    {
        int n = 0;

        System.Console.Out.WriteLine("慣性パラメータ名:" + name);
        System.Console.Out.WriteLine("慣性パラメータブロック:");
        for (int i = 0; i < MAX_BLOCK; i++)
        {
            InertiaBlock ib;
            if ((ib = block[i]) != null)
            {
                System.Console.Out.WriteLine("   No." + i + " " + ib.ToString());
                n++;
            }
        }
        System.Console.Out.WriteLine("慣性パラメータブロック数:" + n);
        System.Console.Out.WriteLine("m=" + m + "[kg] ");
        System.Console.Out.WriteLine("C.G.=" + cg.ToString() + "[m] ");
        System.Console.Out.WriteLine("Ixx=" + ixx + "[kg・m2] ");
        System.Console.Out.WriteLine("Iyy=" + iyy + "[kg・m2] ");
        System.Console.Out.WriteLine("Izz=" + izz + "[kg・m2] ");
        System.Console.Out.WriteLine("Ixy=" + ixy + "[kg・m2] ");
        System.Console.Out.WriteLine("Iyz=" + iyz + "[kg・m2] ");
        System.Console.Out.WriteLine("Izx=" + izx + "[kg・m2] ");
        System.Console.Out.WriteLine("角運動量の係数行列");
        InertiaMat.Print();
        System.Console.Out.WriteLine("角運動量の係数行列の逆行列");
        InertiaInvMat.Print();
    }
}
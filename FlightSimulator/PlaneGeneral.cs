

    using Jp.Maker1.Vsys3.Tools;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.CompilerServices;

public class PlaneGeneral
{
    public String name;
    public String maker;
    public double length;
    public double width;
    public double height;
    public double x_offset;
    public double m;
    public double m0;
    public Vector3D cg;
    public double ixx_m0;
    public double iyy_m0;
    public double izz_m0;
    public double ixy_m0;
    public double iyz_m0;
    public double izx_m0;
    public Vector3D eyePoint;
    public Vector3D[] reference_point;
    public Matrix44 opm;
    public Matrix44 pom;

    public void Init()
    {
        Vector3D pe = new Vector3D(eyePoint).R2l();

        opm = new Matrix44();
        opm.SetTMat(-pe.x, -pe.y, -pe.z);

        pom = new Matrix44();
        pom.SetTMat(pe.x, pe.y, pe.z);
    }

    public void Print()
    {
        Console.Out.WriteLine("飛行機名: " + name);
        Console.Out.WriteLine("メーカー: " + maker);
        Console.Out.WriteLine("全長 [m]: " + length);
        Console.Out.WriteLine("全幅 [m]: " + width);
        Console.Out.WriteLine("全高 [m]: " + height);
        Console.Out.WriteLine("先端から基準点までの長さ [m]: " + x_offset);
        Console.Out.WriteLine("正規全備重量 [kg]: " + m);
        Console.Out.WriteLine("自重 [kg]: " + m0);
        Console.Out.WriteLine("重心 [m]: " + cg.ToStringPos());
        Console.Out.WriteLine("Ixx/m0 [m2]: " + ixx_m0);
        Console.Out.WriteLine("Iyy/m0 [m2]: " + iyy_m0);
        Console.Out.WriteLine("Izz/m0 [m2]: " + izz_m0);
        Console.Out.WriteLine("Ixy/m0 [m2]: " + ixy_m0);
        Console.Out.WriteLine("Iyz/m0 [m2]: " + iyz_m0);
        Console.Out.WriteLine("Izx/m0 [m2]: " + izx_m0);
        Console.Out.WriteLine("視点位置 [m]: " + eyePoint.ToStringPos());
        Console.Out.WriteLine("接触判定点:");
        for (int i = 0; i < reference_point.Length; i++)
        {
            reference_point[i].PrintPos();
            Console.Out.WriteLine("");
        }
        Console.Out.WriteLine("機体系→パイロット視点系変換行列");
        opm.Print();
        Console.Out.WriteLine("パイロット視点系→機体系変換行列");
        pom.Print();
    }
}
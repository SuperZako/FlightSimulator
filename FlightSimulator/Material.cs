

    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.CompilerServices;


public class Material
{
    public LightColor diffuse;
    public LightColor specular;
    public double specularSharpness;
    public LightColor radiation;

    public Material()
    {
        specular = new LightColor(0.0D, 0.0D, 0.0D);
        specularSharpness = 1.0D;
        diffuse = new LightColor(0.0D, 0.0D, 0.0D);
        radiation = new LightColor(0.0D, 0.0D, 0.0D);
    }

    public Material(Material m)
    {
        specular = new LightColor(m.specular);
        specularSharpness = m.specularSharpness;
        diffuse = new LightColor(m.diffuse);
        radiation = new LightColor(m.radiation);
    }

    public Material(LightColor diff, LightColor spe, double speParam,
            LightColor rad)
    {
        specular = spe;
        specularSharpness = speParam;
        diffuse = diff;
        radiation = rad;
    }

    public void Print()
    {
        System.Console.Out.Write("[Diff:");
        diffuse.Print();
        System.Console.Out.Write("/Spec:");
        specular.Print();
        System.Console.Out.Write(":"
                + DispFormat.DoubleFormat(specularSharpness, 1));
        System.Console.Out.Write("/Radi:");
        radiation.Print();
        System.Console.Out.Write("]");
    }
}
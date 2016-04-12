

    using System;
    using System.Drawing;
    using System.Collections;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.CompilerServices;

public class LightColor
{

    public const int RED = 0;
    public const int GREEN = 1;
    public const int BLUE = 2;
    public double red;
    public double green;
    public double blue;

    public LightColor(double r, double g, double b)
    {
        Set(r, g, b);
    }

    public LightColor(LightColor lc)
    {
        Set(lc.red, lc.green, lc.blue);
    }

    public void SetRed(double r)
    {
        red = r;
    }

    public void SetGreen(double g)
    {
        green = g;
    }

    public void SetBlue(double b)
    {
        blue = b;
    }

    public double GetElement(int i)
    {
        double ret = 0.0D;
        if (i == 0)
            ret = red;
        if (i == 1)
            ret = green;
        if (i == 2)
            ret = blue;
        return ret;
    }

    public void SetElement(int i, double val)
    {
        if (i == 0)
            red = val;
        if (i == 1)
            green = val;
        if (i == 2)
            blue = val;
    }

    public void Set(double r, double g, double b)
    {
        red = r;
        green = g;
        blue = b;
    }

    public LightColor Add(LightColor lc)
    {
        return new LightColor(red + lc.red, green + lc.green, blue + lc.blue);
    }

    public LightColor Mult(double k)
    {
        return new LightColor(red * k, green * k, blue * k);
    }

    public LightColor MultEach(double kr, double kg, double kb)
    {
        return new LightColor(red * kr, green * kg, blue * kb);
    }

    public Color GetColor()
    {
        int r;
        if (red > 255D)
            r = 255;
        else
            r = (int)red;
        int g;
        if (green > 255D)
            g = 255;
        else
            g = (int)green;
        int b;
        if (blue > 255D)
            b = 255;
        else
            b = (int)blue;
        return Color.FromArgb(r, g, b);
    }

    public void Print()
    {
        System.Console.Out.Write("[R:" + DispFormat.DoubleFormat(red, 1));
        System.Console.Out.Write("/G:" + DispFormat.DoubleFormat(green, 1));
        System.Console.Out.Write("/B:" + DispFormat.DoubleFormat(blue, 1) + "]");
    }

    public void Println()
    {
        Print();
        System.Console.Out.WriteLine("");
    }
}

    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.CompilerServices;

public class MathTool
{
    public static double DegToRad(double deg)
    {
        return deg / 180.0D * Math.PI;
    }

    public static double RadToDeg(double rad)
    {
        return rad / Math.PI * 180.0D;
    }

    public static double Log10(double x)
    {
        return Math.Log(x) / Math.Log(10.0D);
    }
}

    using Jp.Maker1.Util;
    using Jp.Maker1.Vsys3.Tools;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.CompilerServices;

public class Bearing3
{
    public Angle roll;

    public Angle pitch;

    public Bearing3(Vector3D target)
    {
        roll = new Angle(0.0D, 1);
        pitch = new Angle(0.0D, 0);
        double x = target.x;
        double y = target.y;
        double z = target.z;
        if ((x == 0.0D) && (y == 0.0D))
            roll.SetValue(0.0D);
        else
        {
            roll.SetValue(System.Math.Atan2(-x, y));
        }
        pitch.SetValue(target.VecAngle(new Vector3D(0.0D, 0.0D, 1.0D)));
    }
}
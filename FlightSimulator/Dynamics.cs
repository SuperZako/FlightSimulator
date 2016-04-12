

    using Jp.Maker1.Vsys3.Tools;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.CompilerServices;

public class Dynamics
{
    public static Vector3D Force(double f, Vector3D d)
    {
        return d.SclProd(f);
    }

    public static Vector3D Torque(Vector3D p, Vector3D f)
    {
        return p.CrsProd(f);
    }

    public static Vector3D VOfRot(Vector3D w, Vector3D p)
    {
        return w.CrsProd(p);
    }

    public static Vector3D VWithRot(Vector3D vc, Vector3D w, Vector3D p)
    {
        return vc.Add(VOfRot(w, p));
    }
}
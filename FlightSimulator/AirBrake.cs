

    using Jp.Maker1.Vsys3.Tools;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.CompilerServices;

public class AirBrake
{
    internal int flag;
    internal double delta_max;
    internal double s;
    internal Vector3D gc_base;
    internal Vector3D delta_gc;
    internal double delta;
    internal Vector3D gc;
    internal Vector3D vd;
    internal double d;
    internal Vector3D df;
    internal Vector3D dt;
}
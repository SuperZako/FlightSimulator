
    using Jp.Maker1.Vsys3.Tools;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.CompilerServices;

public class PowerPlant
{
    public PowerPlant()
    {
        fv = new Vector3D();
        tv = new Vector3D();
    }

    public Vector3D p;
    public Vector3D v;
    public int engine_type;
    public Engine engine;
    public double throttle;
    public Vector3D fv;
    public Vector3D tv;

    public void Init()
    {
        throttle = 0.0D;
        engine.Init();
    }

    public void Print()
    {
        System.Console.Out.WriteLine("推力着力点 [m]: " + p.ToStringPos());
        System.Console.Out.WriteLine("推力方向ベクトル [m]: " + v.ToStringPos());
        switch (engine_type)
        {
            case 1:
                engine.Print();
                break;
            case 2:
                break;
            case 4:
                break;
            case 3:
                break;
        }
    }

    public void Calc_dynamics(AirPlane ap, double dt)
    {
        engine.Calc_dynamics(ap, dt);
        fv = engine.GetForce();
        tv = engine.GetTorque();
    }

}
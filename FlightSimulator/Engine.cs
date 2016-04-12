
    using Jp.Maker1.Vsys3.Tools;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.CompilerServices;

public interface Engine
{
    void Init();

    void Print();

    void Calc_dynamics(AirPlane paramAirPlane, double paramDouble);

    Vector3D GetForce();

    Vector3D GetTorque();

    Vector3D GetPos();
}
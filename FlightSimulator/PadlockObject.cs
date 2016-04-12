   using Jp.Maker1.Sim.Tools;
    using Jp.Maker1.Vsys3.Tools;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.CompilerServices;

public class PadlockObject
{
    public String name;
    public Vector3D wpos;

    public PadlockObject(String nameIn, Vector3D wposIn)
    {
        name = "";
        wpos = new Vector3D();
        name = nameIn;
        SetWPos(wposIn);
    }

    public void SetWPos(Vector3D wposIn)
    {
        wpos.SetVec(wposIn);
    }

    public double Dist(AirPlane ap)
    {
        return ap.pMotion.wpos.Sub(wpos).Length();
    }

    public Vector3D RPosPilot(AirPlane ap)
    {
        Matrix44 mat = ap.pMotion.wom.MultMat(ap.plane.opm);
        return wpos.MultMat(mat);
    }

    public Vector3D RPosPilotView(AirPlane ap)
    {
        Matrix44 mat = ap.pMotion.wom.MultMat(ap.plane.opm).MultMat(
                ap.pilot.ViewMatrix());
        return wpos.MultMat(mat);
    }

    public Bearing3 Bearing2PilotView(AirPlane ap)
    {
        return new Bearing3(RPosPilotView(ap));
    }
}
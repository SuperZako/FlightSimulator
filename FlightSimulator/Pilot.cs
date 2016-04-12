
    using Jp.Maker1.Sim.Tools;
    using Jp.Maker1.Vsys3.Tools;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.CompilerServices;

public class Pilot
{
    public Bearing viewDirection;
    internal PadlockObjectList pObjList;

    public Pilot()
    {
        pObjList = new PadlockObjectList();
        viewDirection = new Bearing(0.0D, 0.0D);
    }

    public void SetViewDirection(Vector3D target)
    {
        viewDirection.Set(target);
    }

    public void SetViewDirection(double yaw, double pitch)
    {
        viewDirection.Set(yaw, pitch);
    }

    public Matrix44 ViewMatrix()
    {
        Matrix44 rymat = new Matrix44();
        Matrix44 rxmat = new Matrix44();
        rymat.SetRyMat(-viewDirection.yaw.GetValue());
        rxmat.SetRxMat(-viewDirection.pitch.GetValue());
        return rymat.MultMat(rxmat);
    }

    public void Update(AirPlane ap, CockpitInterface cif, double dt)
    {
        PadlockObject pobj = pObjList.PadlockObj(ap);
        if ((cif.padLock_sw == 0) || (pobj == null))
        {
            double pitch;
            double yaw = pitch = 0.0D;
            if (cif.view_direction == 0)
            {
                if (cif.view_upper == 1)
                    pitch = -1.570796326794897D;
            }
            else
            {
                yaw = -(cif.view_direction - 1) / 4.0D * Math.PI;
                if (cif.view_upper == 1)
                {
                    pitch = -0.7853981633974483D;
                }
            }
            SetViewDirection(yaw, pitch);
        }
        else
        {
            SetViewDirection(pobj.RPosPilot(ap));
        }
    }
}
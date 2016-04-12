    using Jp.Maker1.Vsys3.Tools;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.CompilerServices;

public class PadlockObjectList
{
    public PadlockObjectList()
    {
        obj = new ArrayList();
        padLock = -1;
    }

    private ArrayList obj;
    private int padLock;

    private int GetNObject()
    {
        return obj.Count;
    }

    private int GetIndex(String name)
    {
        int n = GetNObject();
        int ret = -1;
        for (int i = 0; i < n; i++)
        {
            PadlockObject pobj = (PadlockObject)obj[i];
            if (pobj.name.Equals(name))
            {
                ret = i;
            }
        }
        return ret;
    }

    public void AddObject(String name, Vector3D wpos)
    {
        int id = GetIndex(name);
        if (id < 0)
            obj.Add(new PadlockObject(name, wpos));
        else
            obj[id] = new PadlockObject(name, wpos);
    }

    public void DeleteObject(String name)
    {
        int id = GetIndex(name);
        if (id >= 0)
        {
            //ILOG.J2CsMapping.Collections.Collections.RemoveAt(obj,id);
        }
        if (id == padLock)
            padLock = -1;
    }

    public PadlockObject GetObj(String name)
    {
        int id = GetIndex(name);
        PadlockObject pobj = null;
        if (id >= 0)
        {
            pobj = (PadlockObject)obj[id];
        }
        return pobj;
    }

    public PadlockObject PadlockObj(AirPlane ap)
    {
        if (padLock < 0)
        {
            if (GetNObject() > 0)
                PadLockNext(ap);
            else
            {
                return null;
            }
        }
        return (PadlockObject)obj[padLock];
    }

    private int[] IdList(AirPlane ap)
    {
        int n = GetNObject();

        if (n == 0)
            return null;
        int[] ret = new int[n];
        double[] dist = new double[n];

        int i;
        for (i = 0; i < n; i++)
        {
            ret[i] = i;
            dist[i] = ((PadlockObject)obj[i]).Dist(ap);
        }

        for (i = 0; i < n - 1; i++)
        {
            for (int j = i + 1; j < n; j++)
            {
                if (dist[i] > dist[j])
                {
                    int itemp = ret[i];
                    ret[i] = ret[j];
                    ret[j] = itemp;
                    double dtemp = dist[i];
                    dist[i] = dist[j];
                    dist[j] = dtemp;
                }
            }
        }
        return ret;
    }

    private int GetPadIndexInIDList(int[] idList)
    {
        if (idList == null)
            return -1;
        int n = idList.Length;
        int ret = -1;
        for (int i = 0; i < n; i++)
        {
            if (idList[i] == padLock)
            {
                ret = i;
            }
        }
        return ret;
    }

    public void PadLockNext(AirPlane ap)
    {
        int[] idList = IdList(ap);

        int padIdx = GetPadIndexInIDList(idList);

        if (idList == null)
        {
            padLock = -1;
            return;
        }
        int n = idList.Length;
        padIdx++;
        if (padIdx >= n)
            padIdx = 0;
        padLock = idList[padIdx];
    }

    public void PadLockPrev(AirPlane ap)
    {
        int[] idList = IdList(ap);
        int padIdx = GetPadIndexInIDList(idList);
        if (idList == null)
        {
            padLock = -1;
            return;
        }
        int n = idList.Length;
        padIdx--;
        if (padIdx < 0)
            padIdx = n - 1;
        padLock = idList[padIdx];
    }
}
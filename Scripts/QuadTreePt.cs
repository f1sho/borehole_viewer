using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;
using System.Runtime.Serialization;

public class QuadTreePt //: MonoBehaviour
{
    public double x;
    public double y;
    public double z;

    public QuadTreePt(double _x, double _z)
    {
        x = _x;
        z = _z;
    }

    public QuadTreePt(double _y)
    {
        y = _y;
    }

    public QuadTreePt()
    {
        x = 0;
        z = 0;
    }
}

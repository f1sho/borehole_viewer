using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;
using System.Runtime.Serialization;

public class QuadTreeNode //: MonoBehaviour
{
    //public QuadTreePt point2d;
    public QuadTreePt y;
    public double[] data;
    public double dataDepth;

    public QuadTreeNode(QuadTreePt _y, double _dataDepth, double[] _data)
    {
        y = _y;
        dataDepth = _dataDepth;
        data = _data;
    }

    public QuadTreeNode()
    {
        data = null;
    }
}

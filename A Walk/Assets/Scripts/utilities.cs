using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Utilities
{
    public static Vector3 VectorAbs(Vector3 v)
    {
        return new Vector3(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z));
    }
}
//public class Utilities
//{

//}

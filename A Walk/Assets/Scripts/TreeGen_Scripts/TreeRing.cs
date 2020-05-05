using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct TreeRing //info for one "ring" of a Segment
{
    public TreeRing(int subdivY, Vector3 curvePnt, Vector3 curvePerpendicularTangent)
    { 
        float Yincrement = 360 / subdivY; //rotation increments around point in curve
        Vector3[] ringVerts = new Vector3[subdivY];
        for (int j = 0; j <= subdivY - 1; j++)
        {
            Vector3 ringVert = curvePnt + (Quaternion.Euler(0, Yincrement * j, 0) * curvePerpendicularTangent); //point along curve + perpendicular normal vector * rotation multiple 
            Debug.Log(ringVert);
            ringVerts[j] = ringVert;
        }
        curvePoint = curvePnt;
        verts = ringVerts;
    }

    public Vector3 curvePoint { get; } //keep as parent point for ring -> used for moving ring as whole with movement of curve
    public Vector3[] verts {get;}//verts in the ring
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public struct Ring //info for one "ring" of a Segment
{
    internal Ring(Vector3 curvePnt, Vector3[] vertices)
    {
        curvePoint = curvePnt;
        verts = vertices;
    }

    public Vector3 curvePoint { get; } //keep as parent point for ring -> used for moving ring as whole with movement of curve
    public Vector3[] verts {get;}//verts in the ring
}
*/

public class TreeRing
{
    public int subdivY; //a value of TreeRing because this remains the same for duration of segment
  
    public TreeRing(int divY)
    {
        subdivY = divY;
    }
    public Ring NewRing(Vector3 curvePoint, Vector3 curvePerpendicularTangent) //these properties are not a value of TreeRing because they change for each ring
    {
        float Yincrement = 360 / subdivY; //rotation increments around point in curve
        Vector3[] ringVerts = new Vector3[subdivY];
        for (int j = 0; j <= subdivY - 1; j++)
        {
            Vector3 ringVert = curvePoint + (Quaternion.Euler(0, Yincrement * j, 0) * curvePerpendicularTangent); //point along curve + perpendicular normal vector * rotation multiple 
            Debug.Log(ringVert);
            ringVerts[j] = ringVert;
        }
        return new Ring(curvePoint, ringVerts);
    }
    
}

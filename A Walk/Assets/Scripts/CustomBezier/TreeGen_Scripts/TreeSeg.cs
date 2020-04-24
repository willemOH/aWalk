using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Segment
{
    internal Segment(Bezier path, Ring[] vertices)
    {
        P1 = path.P1;
        P2 = path.P2;
        P3 = path.P3;
        P4 = path.P4;


        ringVerts = vertices;

        ringsNum = ringVerts.Length;
        vertsNum = ringVerts[0].verts.Length;
        vertsInSeg = ringsNum * vertsNum;

    }

    public Vector3 P1 { get; }  //these are kept for later possible modification
    public Vector3 P2 { get; }
    public Vector3 P3 { get; }
    public Vector3 P4 { get; }

    public Ring[] ringVerts { get; } //subdimension contains a set of verts forming a ring
                                     //around point on Segment curve 
                                     //float[] tDistributions; //t_values where the rings will be put along the curve(t)

    public int ringsNum { get; }
    public int vertsNum { get; }
    public int vertsInSeg { get; }
}
public class TreeSeg

{ 
    public int subDivY;
    public int subDivX;
    Bezier bez;

    public TreeSeg(int subDivisionsX, int subDivisionsY, Bezier bezierPath)
    {
        subDivY = subDivisionsY;
        subDivX = subDivisionsX;
        bez = bezierPath;
    }
   
    public Segment NewSegment() //generates array of Ring structs containing vertex placement for each ring and curve data for one Segment
    {
        Ring[] segments = new Ring[subDivX];
        float Xincrement = 1 / subDivX; // increments along Segment curve
        TreeRing ring = new TreeRing(subDivY);
        for (int i = 0; i <= subDivX - 1; i++)
        {
            Vector3 curvePoint = bez.Curve(i * Xincrement);
            Vector3 curvePerpendicularTangent = Quaternion.Euler(0, 0, 90) * bez.CurveTanNVec(); //find out what value this give me
            segments[i] = ring.NewRing(curvePoint, curvePerpendicularTangent);
        }
        return new Segment(bez, segments);
    }

    

}

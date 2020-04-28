using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct TreeSeg //segment of tree made from one bezier curve
{
    public TreeSeg(int subDivX, int subDivY, Bezier bezPath)
    {

        TreeRing[] segment = new TreeRing[subDivX];
        float Xincrement = 1f / (float)subDivX; // increments along Segment curve
        for (int i = 0; i <= subDivX - 1; i++)
        {
            Vector3 curvePoint = bezPath.Curve(i * Xincrement);
            Vector3 curvePerpendicularTangent = Quaternion.Euler(90, 0, 0) * bezPath.CurveTanNVec();
            Debug.Log(curvePoint);//find out what value this give me
            segment[i] = new TreeRing(subDivY, curvePoint, curvePerpendicularTangent);
        }

        P1 = bezPath.P1;
        P2 = bezPath.P2;
        P3 = bezPath.P3;
        P4 = bezPath.P4;


        ringVerts = segment;

        ringsNum = ringVerts.Length;
        vertsNum = ringVerts[0].verts.Length;
        vertsInSeg = ringsNum * vertsNum;

    }

    public Vector3 P1 { get; }  //these are kept for later possible modification
    public Vector3 P2 { get; } //replace with Bezier class
    public Vector3 P3 { get; }
    public Vector3 P4 { get; }

    public TreeRing[] ringVerts { get; } //subdimension contains a set of verts forming a ring
                                     //around point on Segment curve 
                                     //float[] tDistributions; //t_values where the rings will be put along the curve(t)

    public int ringsNum { get; }
    public int vertsNum { get; }
    public int vertsInSeg { get; }
}



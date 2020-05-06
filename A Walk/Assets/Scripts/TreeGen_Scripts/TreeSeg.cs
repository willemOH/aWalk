using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct TreeSeg //segment of tree made from one bezier curve
{
    /// <summary>
    /// Treeseg params
    /// </summary>
    /// <param name="subDivX"> divisions of curve</param>
    /// <param name="subDivY"> number of points on rings</param>
    /// <param name="bezPath"></param>
    public TreeSeg(int subDivX, int subDivY, Bezier bezPath)
    {

        TreeRing[] segment = new TreeRing[subDivX];
        float Xincrement = 1f / (float)subDivX; // increments along Segment curve
        for (int i = 0; i < subDivX - 1; i++)
        {
            Vector3 curvePoint = bezPath.Curve(i * Xincrement);
            Vector3 curvePerpendicularTangent = Vector3.Cross(bezPath.CurveTangentVectorN(i * Xincrement), new Vector3(1, 0, 0));
            //Vector3 curvePerpendicularTangent = Quaternion.Euler(90, 0, 0) * bezPath.CurveTanNVec();
            Debug.Log(curvePoint);//find out what value this give me
            segment[i] = new TreeRing(subDivY, curvePoint, curvePerpendicularTangent);
        }

        bezierPath = bezPath;

        ringVerts = segment;

        ringsNum = ringVerts.Length;
        vertsNum = ringVerts[0].verts.Length;
        vertsInSeg = ringsNum * vertsNum;

    }
    public Bezier bezierPath { get; }

    public TreeRing[] ringVerts { get; } //subdimension contains a set of verts forming a ring
                                     //around point on Segment curve 
                                     //float[] tDistributions; //t_values where the rings will be put along the curve(t)

    public int ringsNum { get; }
    public int vertsNum { get; }
    public int vertsInSeg { get; }
}



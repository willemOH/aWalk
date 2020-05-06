using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//given the four points, can perform a variety of curve function calculations
public class Bezier
{
    public Vector3 P1 = new Vector3(0,0,0); //startPoint
    public Vector3 P2 = new Vector3(.25f, .25f, 0);//startPoint Handle
    public Vector3 P3 = new Vector3(-.25f, .75f, 0);//endPoint Handle
    public Vector3 P4 = new Vector3(0,1,0); //endPoint
 
    public Vector3 Curve(float t)
    {
        Vector3 position3D = (Mathf.Pow((1 - t), 3)) * P1 + 3 * 
                             (Mathf.Pow((1 - t), 2)) * t * P2 + 3 * (1 - t) * 
                             (Mathf.Pow(t, 2)) * P3 + (Mathf.Pow(t, 3)) * P4; //cubic bezier curve function
        return position3D;
    }

    public Vector3 Handle1(float t) //point along handle line given (t)
    {
        Vector3 position3D = -(P2 - P1) * t + P2; //directionVec * t * point1
        return position3D;
    }

    public Vector3 Handle2(float t) //point along handle line given (t)
    {
        Vector3 position3D = -(P3 - P4) * t + P3; //directionVec * t * point1
        return position3D;
    }
    
    public Vector3 CurveTangentLine(float t) //derivative of cubic bezier curve function
    {
        Vector3 position3D = (Mathf.Pow(3*(1 - t), 2)) * P1 + 3 * 2*(1 - t) * t * P2 + 3 * (1 - t) * (2*t) * P3 + (Mathf.Pow(3*t, 2)) * P4;
        return position3D;
    }

    public Vector3 CurveTangentVectorN(float t)
    {
        Vector3 firstPoint = this.Curve(t);
        Vector3 secondPoint = this.Curve(t + .01f);
        Vector3 tangentVector = secondPoint - firstPoint;
        return tangentVector.normalized;
    }
    /*
    public Vector3 CurveTanNVec() //normal vector of tangent line
    {
        Vector3 point1 = CurveTangentLine(0);
        Vector3 point2 = CurveTangentLine(1);
        return point1 - point2; //add this vector to bez point and that should do it
    }
    */

}

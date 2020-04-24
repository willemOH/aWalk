using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BezContCurve : MonoBehaviour
{

    //public Transform P0;
    //public Transform P1;
    //public Transform P2;
    //public Transform P3;

    public pointPathGen pointGen;
   // public List<Vector3>  points = pointPathGen.points;
   

    [Range(0f, 1f)]
    public float tObject;

    private float timer;
    private float tTex;

    public GameObject followPath;
    public GameObject curveLine;

    public int lineDensity = 20;
    private int internal_lineDensity = 20;

    private float[] testIncrements;
    private float[] lineDrawIncrements;

    private bool everyOtherUpdate = true;

    [Range(0f, 1f)]
    public float lineSize = .5f;

    

    private void OnEnable()
    {
        pointGen = this.GetComponent<pointPathGen>();
    }

    void Start()
    {
        //P0 = this.transform.Find("P0");
        //P1 = this.transform.Find("P1");
        //P2 = this.transform.Find("P2");
        //P3 = this.transform.Find("P3");
    }

   


    void Update()
    {
        //foreach curve segment, replace with curve segment of same values except for new t

        //for(int i = 0; i<curves.Count; i++) //curve update to change t value
        //{
        //    Vector3 curveUpdate = Curve(curves[i][0], curves[i][1], curves[i][2], t);
        //    curves[i] = curveUpdate;
        //}

        //if (followPath != null)
        //{
        //    followPath.transform.position = Curve(tObject);
        //}

        
        //Debug.Log(pointGen.curves[0][0]);
        lineDrawIncrements = new float[lineDensity];

        for (int i = 1; i < lineDensity; i++)
        {
            
            lineDrawIncrements[i] = (1 / (float)lineDensity) * (float)i;
        }

        //foreach (List<Vector3> curve in pointGen.curves)
       
            foreach (float increment in lineDrawIncrements)
            {
                var linePoint = Instantiate(curveLine, Curve(pointGen.curves[0][0], pointGen.curves[0][1], pointGen.curves[0][2], increment), Quaternion.identity);
            
                //var linePoint = Instantiate(curveLine, new Vector3(0,0,0), Quaternion.identity);
                linePoint.transform.parent = this.transform.Find("LineSegments2");
            }
        

        foreach (GameObject tag in GameObject.FindGameObjectsWithTag("BezierLine"))
        {
            tag.transform.localScale = new Vector3(lineSize, lineSize, lineSize);
        }

        //if (everyOtherUpdate == true)
        //{
        //    foreach (GameObject tag in GameObject.FindGameObjectsWithTag("BezierLine"))
        //    {
        //        DestroyImmediate(tag);
        //    }
        //    everyOtherUpdate = false;
        //}
        //else
        //    everyOtherUpdate = true;


    }

    //Vector3 Curve(float t)
    //{
    //    //Vector3 position2D = (1 - t) * ((1 - t) * P0.position + t * P1.position) + t * ((1 - t) * P1.position + t * P2.position);
    //    Vector3 position3D = (Mathf.Pow((1 - t), 3)) * P0.position + 3 * (Mathf.Pow((1 - t), 2)) * t * P1.position + 3 * (1 - t) * (Mathf.Pow(t, 2)) * P2.position + (Mathf.Pow(t, 3)) * P3.position;
    //    return position3D;
    //}

    Vector3 Curve(Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        //Vector3 position2D = (1 - t) * ((1 - t) * P0.position + t * P1.position) + t * ((1 - t) * P1.position + t * P2.position);
        Vector3 position3D = p2 + (Mathf.Pow((1 - t), 2)) * (p1 - p2) + (Mathf.Pow((t), 2)) * (p3 - p2); //quadratic bezier curve formula as a function of (t) where t is the current location along the curve from 0 to 1
        return position3D;
    }


}

   


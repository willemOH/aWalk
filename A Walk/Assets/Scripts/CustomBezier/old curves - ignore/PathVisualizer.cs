using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PathVisualizer : MonoBehaviour
{

    public Vector3 P1; //startPoint
    public Vector3 P2; //startPoint Handle
    public Vector3 P3; //endPoint Handle
    public Vector3 P4; //endPoint

 

    public GameObject p1;
    public GameObject p2;
    public GameObject p3;
    public GameObject p4;

   

    [Range(0f, 1f)]
    public float handleDistanceRatio = .5f;

    public float maxDistance_x;
    //public float maxDistance_y;
    public float totalDistance = 5;
    public float distanceBias = 1;
    public float maxDistance_z;

    [Range(0f, 360f)]
    public float handleRotMax = 45;

   
    public float speed = 1;

    public Vector3 PlayerHandPos = new Vector3(0, 0, 0);

    public float travelRange = 4f; //is used for curve endpoint calculation




    public bool previous = false;

    [Range(0f, 1f)]
    public float tObject;

    private float tTex;

    
    public GameObject curveLine;
    public GameObject handleLine;

    public int lineDensity = 20;
    private int internal_lineDensity = 20;

    private float[] testIncrements;
    private float[] lineDrawIncrements;

    private bool everyOtherUpdate = true;

    [Range(0f, 1f)]
    public float lineSize = .5f;

    void Start()
    {
       
    }

    

   

    private void updateTestObjects()
    {
        p1.transform.position = P1;
        p2.transform.position = P2;
        p3.transform.position = P3;
        p4.transform.position = P4;
    }
    void Update()
    {

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

        tObject += Time.deltaTime / speed;
        
       
        if(Mathf.Round(tObject*100)/100 >= 1) //rounding t value to 2 decimal places for comparison
        {
           
            updateTestObjects();
            tObject -= tObject; //possible buggy scenario should set to 0?
        }

        

        lineDrawIncrements = new float[lineDensity];

        for (int i = 1; i < lineDensity; i++)
        {
            
            lineDrawIncrements[i] = (1 / (float)lineDensity) * (float)i;
        }

        foreach (float increment in lineDrawIncrements)
        {
            var curveLinePoint = Instantiate(curveLine, Curve(increment), Quaternion.identity);
            var handle1LinePoint = Instantiate(handleLine, Handle(P1, P2, increment), Quaternion.identity);
            var handle2LinePoint = Instantiate(handleLine, Handle(P4, P3, increment), Quaternion.identity);
            curveLinePoint.transform.parent = this.transform.Find("LineSegments2"); //assigning parent directory for instantiated line sprite
            handle1LinePoint.transform.parent = this.transform.Find("LineSegments2");
            handle2LinePoint.transform.parent = this.transform.Find("LineSegments2");

        }

        foreach (GameObject tag in GameObject.FindGameObjectsWithTag("BezierLine"))
        {
            tag.transform.localScale = new Vector3(lineSize, lineSize, lineSize);
        }

        


    }

    Vector3 Curve(float t)
    {
        Vector3 position3D = (Mathf.Pow((1 - t), 3)) * P1 + 3 * (Mathf.Pow((1 - t), 2)) * t * P2 + 3 * (1 - t) * (Mathf.Pow(t, 2)) * P3 + (Mathf.Pow(t, 3)) * P4; //cubic bezier curve
        return position3D;
    }

    Vector3 Handle(Vector3 handle1, Vector3 handle2, float t) //point along handle line given (t)
    {
        Vector3 position3D = -(handle1 - handle2) * t + handle1; //directionVec * t * point1
        return position3D;
    }

}

   


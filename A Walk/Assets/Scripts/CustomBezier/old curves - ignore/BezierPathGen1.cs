using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(LineRenderer))]
public class BezierPathGen1 : MonoBehaviour
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

    public GameObject followPath;
    public GameObject curveLine;
    public GameObject handleLine;
    
 
    public int lineDensity = 20;
    private int internal_lineDensity = 20;

    private float[] testIncrements;
    private float[] lineDrawIncrements;

    private bool everyOtherUpdate = true;

    [Range(0f, 1f)]
    public float lineSize = .5f;

    public int layerOrder = 0;

    public LineRenderer lineRenderer;
    void Start()
    {
        lineRenderer.sortingLayerID = layerOrder;
       
        P1 = PlayerHandPos; //initial Startpoint is 
        P4 = P4Gen(P1, maxDistance_z, maxDistance_x);
        P2 = P1 + new Vector3(0, 0, 1); //away from player
        P3 = P4 + (Quaternion.Euler(1, 1, Random.Range(0f, handleRotMax)) * (P4 - P1 * handleDistanceRatio));
    }

    private Vector3 P4Gen(Vector3 P1, float maxDistance_Z, float maxDistance_X)
    {
        float zCoord = Random.Range(0f, maxDistance_Z);
        float xCoord = Random.Range(-(totalDistance - zCoord), (totalDistance - zCoord)) - distanceBias; 
        float yCoord = totalDistance - zCoord - xCoord; 
        //some kind of adjustment to make with absolute values total distances are not the same
        return new Vector3(xCoord, yCoord, zCoord);
    }

    private void newCurvePoints()
    {
        //memory intensive method -> BUT ITS NOT VERY MEMORY INTENSIVE DO THIS FOR REITERATION
        //Vector3 newP1 = P4;
        //Vector3 newP2 = P4 + (P3 - P4);
        //Vector3 newP4 = P4Gen(newP1, maxDistance_z, maxDistance_x);
        //Vector3 curveDirection = P4 - P1;
        //Vector3 newP3 = P4 + (Quaternion.Euler(1, 1, handleRot) * (curveDirection * handleDistanceRatio)); //to get handle - endpoint plus rotation * direction

        //to optimize memory by replacing values directly, order of assignment determines whether new P(X) or old P(X) is declared

        //P2 = P4 - (P3 - P4); //p1 handle = p1 position - p4 handle vector -> get handle 180 degrees opposite to p4 handle for smooth continuous bezier curve addition
        //P3 = P4 + (Quaternion.Euler(1, 1, Random.Range(0f, handleRotMax)) * (P4 - P1 * handleDistanceRatio)); //p4 handle = p1 + previous curve direction vector * handleDistanceRation * random rotation of vector capped by handleRotMax
        //P1 = P4; //make p1 from old p4
        //P4 = P4Gen(P1, maxDistance_z, maxDistance_x); // new p4 from gen

        Vector3 newP1 = P4;
        Vector3 newP2 = newP1 - (P3 - newP1);
        //Vector3 newP4 = P1 + (Quaternion.Euler(1, 1, Random.Range(0f, handleRotMax)) * (P1 - P4)); //p4 handle = p1 + previous curve direction vector * handleDistanceRation * random rotation of vector capped by handleRotMax
        Vector3 p4vec = newP1 - P1;
        Vector3 p4rot = Quaternion.Euler(0, 0, 2) * p4vec;
        Vector3 newP4 = newP1 + p4rot;
        Vector3 p3handle = (Quaternion.Euler(1, Random.Range(0f, handleRotMax),1 ) * new Vector3(0, 0, 1));
        Vector3 newP3 = newP4 + p3handle ; // probably p3 calc problem remember to subtract objective point from pivot point before multiplying by euler

        P1 = newP1;
        P2 = newP2;
        P3 = newP3;//* handleDistanceRatio
        P4 = newP4;//* handleDistanceRatio

        //note Unity requires angle * vector instead of vector * angle
        //(Quaternion.LookRotation(curveDirection * handleDistanceRatio) * Quaternion.Euler(1,1,handleRot));
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
        if (everyOtherUpdate == true)
        {
            foreach (GameObject tag in GameObject.FindGameObjectsWithTag("BezierLine"))
            {
                DestroyImmediate(tag);
            }
            everyOtherUpdate = false;
        }
        else
            everyOtherUpdate = true;

        tObject += Time.deltaTime / speed;
        
        if(Mathf.Round(tObject * 100) / 100 == .9)
        {
            previous = true;
            Debug.Log("true");
        }
        if(Mathf.Round(tObject*100)/100 >= 1) //rounding t value to 2 decimal places for comparison
        {
            newCurvePoints();
            updateTestObjects();
            tObject -= tObject; //possible buggy scenario should set to 0?
        }

        if (followPath != null)
        {
            followPath.transform.position = Curve(tObject);
        }

        lineDrawIncrements = new float[lineDensity];

        for (int i = 1; i < lineDensity; i++)
        {
            
            lineDrawIncrements[i] = (1 / (float)lineDensity) * (float)i;
        }

        foreach (float increment in lineDrawIncrements)
        {
            //lineRenderer.positionCount = lineDensity;
            //lineRenderer.SetPosition((int)increment, Curve(increment));
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

   


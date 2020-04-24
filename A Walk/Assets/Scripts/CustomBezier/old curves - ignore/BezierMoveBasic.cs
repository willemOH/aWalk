using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BezierMoveBasic : MonoBehaviour
{

    public Transform P0;
    public Transform P1;
    public Transform P2;
    public Transform P3;

    [Range(0f, 1f)]
    public float tObject;

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

    void Start()
    {
        P0 = this.transform.Find("P0");
        P1 = this.transform.Find("P1");
        P2 = this.transform.Find("P2");
        P3 = this.transform.Find("P3");
    }

    void Update()
    {

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
            var linePoint = Instantiate(curveLine, Curve(increment), Quaternion.identity);
            linePoint.transform.parent = this.transform.Find("LineSegments");

        }

        foreach (GameObject tag in GameObject.FindGameObjectsWithTag("BezierLine"))
        {
            tag.transform.localScale = new Vector3(lineSize, lineSize, lineSize);
        }

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


    }

    Vector3 Curve(float t)
    {
        //Vector3 position2D = (1 - t) * ((1 - t) * P0.position + t * P1.position) + t * ((1 - t) * P1.position + t * P2.position);
        Vector3 position3D = (Mathf.Pow((1 - t), 3)) * P0.position + 3 * (Mathf.Pow((1 - t), 2)) * t * P1.position + 3 * (1 - t) * (Mathf.Pow(t, 2)) * P2.position + (Mathf.Pow(t, 3)) * P3.position; //cubic bezier curve
        return position3D;
    }

}

   


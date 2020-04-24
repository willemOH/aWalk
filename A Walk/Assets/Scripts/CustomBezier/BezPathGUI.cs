using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//creates visual representation of curve and curvepoints
public class BezPathGUI : MonoBehaviour 
{
    public tController tCtrl;
    public GameObject curveLine;
    public GameObject handleLine;

    public int lineDensity = 20;
    private int internal_lineDensity = 20;
    private float[] testIncrements;
    private float[] lineDrawIncrements;

    [Range(0f, 1f)]
    public float lineSize = .5f;

    public bool previous = false;

    public GameObject p1;
    public GameObject p2;
    public GameObject p3;
    public GameObject p4;

    private bool everyOtherUpdate = true; //part of updating every other frame for lighter load and less flashing

    void Start()
    {
        tCtrl = this.GetComponent<tController>();
        p1 = GameObject.Find("p1");
        p2 = GameObject.Find("p2");
        p3 = GameObject.Find("p3");
        p4 = GameObject.Find("p4");
    }

    public void updateTestObjects()
    {
        p1.transform.position = tCtrl.pathGen.pth.P1;
        p2.transform.position = tCtrl.pathGen.pth.P2;
        p3.transform.position = tCtrl.pathGen.pth.P3;
        p4.transform.position = tCtrl.pathGen.pth.P4;
    }
    void Update()
    {
        updateTestObjects();
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

        lineDrawIncrements = new float[lineDensity];

        if (Mathf.Round(tCtrl.tObject * 100) / 100 == .9)
        {
            previous = true; //tell GUI to move represented points to state "previous"

        }

        for (int i = 1; i < lineDensity; i++)
        {

            lineDrawIncrements[i] = (1 / (float)lineDensity) * (float)i;
        }

        foreach (float increment in lineDrawIncrements) //drawing sprites along curves and points
        {
            var curveLinePoint = Instantiate(curveLine, tCtrl.bez.Curve(increment), Quaternion.identity);
            var handle1LinePoint = Instantiate(handleLine, tCtrl.bez.Handle1(increment), Quaternion.identity);
            var handle2LinePoint = Instantiate(handleLine, tCtrl.bez.Handle2(increment), Quaternion.identity);
            curveLinePoint.transform.parent = this.transform.Find("LineSegments2"); //assigning parent directory for instantiated line sprite
            handle1LinePoint.transform.parent = this.transform.Find("LineSegments2");
            handle2LinePoint.transform.parent = this.transform.Find("LineSegments2");

        }

        foreach (GameObject tag in GameObject.FindGameObjectsWithTag("BezierLine"))
        {
            tag.transform.localScale = new Vector3(lineSize, lineSize, lineSize); 
        }
    }
}

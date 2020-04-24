using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//testing tool
//visuallizes previous curve to compare the two
public class testController : MonoBehaviour
{
    public BezPathGUI original;

    public  PathVisualizer previous;
    public GameObject test;
    public GameObject oldLine;

    void Start()
    {
        original = this.GetComponent<BezPathGUI>();

        previous = test.AddComponent<PathVisualizer>();

        previous.p1 = GameObject.Find("p1old");
        previous.p2 = GameObject.Find("p2old");
        previous.p3 = GameObject.Find("p3old");
        previous.p4 = GameObject.Find("p4old");
        previous.curveLine = oldLine;
        previous.handleLine = original.handleLine;
        previous.lineDensity = original.lineDensity;
        


    }

    void Update()
    {
        if (original.previous == true)
        {
            previous.P1 = original.tCtrl.pathGen.pth.P1;
            previous.P2 = original.tCtrl.pathGen.pth.P2;
            previous.P3 = original.tCtrl.pathGen.pth.P3;
            previous.P4 = original.tCtrl.pathGen.pth.P4;
            original.previous = false;
        }
    }
}





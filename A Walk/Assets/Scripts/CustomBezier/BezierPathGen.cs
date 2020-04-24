using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BezierPathGen : MonoBehaviour //contains generator parameters and method for utilizing them for randomized curve points
{
    public Bezier pth;
    public Vector3 PlayerHandPos = new Vector3(0, 0, 0);
    public Vector3 treeLoc = new Vector3(0, 0, 0);

    [Header("DISTANCE VEC VALUES")]
    public float travelRange = 1f; 
    public float travelRangeMin = 1f;  //is used for curve endpoint calculation
    public float travelRangeMax = 1.5f;
    private float originalTravelRange; 
    public float z_rotMax;
    public float x_rotMax;
    public float y_rotMax;

    [Header("HANDLE VEC VALUES")]
    [Range(0f, 10f)]
    public float handleStrength = 1f;
    public float x_handleRotMax;
    public float y_handleRotMax;
    public float z_handleRotMax;

    [Header("Start Location Options (enable only one)")]
    public bool tree = false;
    public bool butterfly = false;
    public bool test = false;

    void Start()
    {
     originalTravelRange = travelRange; //copy of travel Range
     pth = new Bezier();
        if (tree)
        {
            pth.P1 = treeLoc;
            pth.P4 = pth.P1 + new Vector3(travelRange, 0, 0);
            pth.P2 = pth.P1 + new Vector3(0, 0, 1); //away from player
            pth.P3 = pth.P4 + Quaternion.Euler(Random.Range(-x_handleRotMax, x_handleRotMax), //generate amount to rotate p4 distance vector for direction
                                             Random.Range(-y_handleRotMax, y_handleRotMax),
                                             Random.Range(-z_handleRotMax, z_handleRotMax)) * (pth.P4 - pth.P1 * handleStrength);
        } //Start Location options selected from within editor
        if (butterfly)
        {


            pth.P1 = PlayerHandPos; //initial Startpoint is 
            pth.P4 = pth.P1 + new Vector3(travelRange, 0, 0);
            pth.P2 = pth.P1 + new Vector3(0, 0, 1); //away from player
            pth.P3 = pth.P4 + Quaternion.Euler(Random.Range(-x_handleRotMax, x_handleRotMax), //generate amount to rotate p4 distance vector for direction
                                             Random.Range(-y_handleRotMax, y_handleRotMax),
                                             Random.Range(-z_handleRotMax, z_handleRotMax)) * (pth.P4 - pth.P1 * handleStrength);
        }
        if (test)
        {
            pth.P1 = new Vector3(1, 2, 3);
            pth.P4 = new Vector3(2, 3, 4);
            pth.P2 = new Vector3(1, 3, 3);
            pth.P3 = new Vector3(2, 4, 4);
        }
    }

    public void newCurvePoints() //generates new collection of points for a curve to be generated from -> P1, P2, P3, P4
    {
        travelRange = originalTravelRange * Random.Range(travelRangeMin, travelRangeMax);
        Vector3 newP1 = pth.P4;
        Vector3 newP2 = newP1 - (pth.P3 - newP1);
        //**begin p4 calculations**
        Vector3 p4vec = Vector3.ClampMagnitude((newP1 - pth.P1)*100, travelRange); //direction of vector with magnitude clamped to max travel rangevalue 
                                                                                   //-> so change does not accumulate over time as with alternative of multiplying by travelRange
        //Debug.Log(p4vec.magnitude); //this test shows rounding error but not a problem
        Vector3 p4rot = Quaternion.Euler(Random.Range(-x_rotMax, x_rotMax), //generate amount to rotate p4 distance vector for direction
                                         Random.Range(-y_rotMax, y_rotMax), 
                                         Random.Range(-z_rotMax, z_rotMax)) 
                                         * p4vec;
        Vector3 newP4 = newP1 + p4rot; //adding rotated distance vector for final p4 value
        //**end p4 calculations**
        Vector3 p3handle = Quaternion.Euler(Random.Range(-x_handleRotMax, x_handleRotMax), //generate amount to rotate p4 distance vector for direction
                                         Random.Range(-y_handleRotMax, y_handleRotMax),
                                         Random.Range(-z_handleRotMax, z_handleRotMax))
                                         * p4vec.normalized*handleStrength;
        Vector3 newP3 = newP4 + p3handle ;

        pth.P1 = newP1;
        pth.P2 = newP2;
        pth.P3 = newP3;
        pth.P4 = newP4;
    }
}

   


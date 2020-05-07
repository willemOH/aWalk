using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//moves objects along curve and resets when reaches end

public class tController : MonoBehaviour
{
    public BezierPathGen pathGen;
    public Bezier bez;

    public float speed = 1;
    public float speedToRangeRatio = 1.2f; //how fast long ranges are vs how slow short ranges are
    private float originalSpeed;
    private float previousSpeed; //speed from last point of last curve serves as base to smooth lerping to next curve speed
    public float lerpTune = .5f;
    public float globalSpeed = 1;
    float t;

    public float current_speed; //live-fed speed
    private Vector3 lastLoc; //for magnitude calculations
    [Range(0f, 1f)]
    public float tObject = 0;
    Vector3 tVecPrevious;
    
    public GameObject followPath;

    private float tTex;
    bool once = false;

   
    public enum state
    {
        idle,
        flying,
        landing
    };
    

    public state butterState;

    [Header("path subtypes")]
    public bool curveTangent;
    public bool curveTangentPerpendicular;
  
    void Start()
    {
        pathGen = this.GetComponent<BezierPathGen>();
        bez = new Bezier();
        originalSpeed = speed;
        lastLoc = followPath.transform.position;
    }

    private void bezUpdate()
    {
        bez.P1 = pathGen.pth.P1;
        bez.P2 = pathGen.pth.P2;
        bez.P3 = pathGen.pth.P3;
        bez.P4 = pathGen.pth.P4;
    }
        
    void Update()
    {
        if(once != true)
        {
            bezUpdate();
            once = true;
        }
       
        if (butterState == state.flying)
        {
            // speed smoothing operations -- might look into using smooth.Dampen
            t += Time.deltaTime * lerpTune;
            speed = Mathf.Lerp(previousSpeed, originalSpeed / pathGen.travelRange * speedToRangeRatio, Mathf.Clamp(t,0,1)); //copy of speed so calculations don't build on up eachother
            current_speed = Vector3.Magnitude(followPath.transform.position - lastLoc) * 100; //distance between position at last frame and position at this frame * 100 for 1 decimal range 
            lastLoc = followPath.transform.position;                                    //setting previous position for next frame as current position
            tObject += Time.deltaTime * speed / 10 * globalSpeed; // division for precision
            if (Mathf.Round(tObject * 100) / 100 >= 1) //rounding t value to 2 decimal places 
            {
                pathGen.newCurvePoints();
                bezUpdate();
                tObject -= tObject;
                t = 0.0f; 
                previousSpeed = speed;
            }

            if (followPath != null)
            {
                if (curveTangent == true)
                {
                    followPath.transform.position = bez.CurveTangentLine(tObject);
                }
                else if (curveTangentPerpendicular == true)
                {
                    followPath.transform.position = Quaternion.Euler(0, 0, 90) * bez.CurveTangentLine(tObject);
                }
                else
                {
                    tVecPrevious = followPath.transform.position;//storing previous position vector
                    followPath.transform.position = bez.Curve(tObject);//assigning new position vector based on curve function
                    followPath.transform.LookAt(followPath.transform.position + (followPath.transform.position - tVecPrevious));//subtracting one from the other and adding to current to find facing vector and assigning lookAt vector
                }
            }
        }

        
    }
}

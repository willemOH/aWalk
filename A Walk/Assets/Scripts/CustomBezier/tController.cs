using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//moves objects along curve and resets when reaches end

public class tController : MonoBehaviour
{
    public BezierPathGen pathGen;
    public Bezier bez;
    public Collider boundsBox;

    public float speed = 1;
    public float speedToRangeRatio = 1.2f; //how fast long ranges are vs how slow short ranges are
    private float originalSpeed;
    private float previousSpeed; //speed from last point of last curve serves as base to smooth lerping to next curve speed
    public float lerpTune = .5f;
    public float globalSpeed = 1;
    float t;

    public bool initialize = false; 
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
        if (initialize == true) //messy solution to only start tController when initialize is set to true
        {    //by external script. Better if it could be more self-sufficient
            if (once != true)
            {
                bezUpdate();
                once = true;
            }

            if (butterState == state.flying)
            {

                // speed smoothing operations -- might look into using smooth.Dampen
                t += Time.deltaTime * lerpTune;
                speed = Mathf.Lerp(previousSpeed, originalSpeed / pathGen.travelRange * speedToRangeRatio, Mathf.Clamp(t, 0, 1)); //copy of speed so calculations don't build on up eachother
                current_speed = Vector3.Magnitude(followPath.transform.position - lastLoc) * 100; //distance between position at last frame and position at this frame * 100 for 1 decimal range 
                lastLoc = followPath.transform.position;                                    //setting previous position for next frame as current position
                tObject += Time.deltaTime * speed / 10 * globalSpeed; // division for precision
                if (Mathf.Round(tObject * 100) / 100 >= 1) //rounding t value to 2 decimal places 
                {
                    pathGen.newCurvePoints();
                    int i = 0;
                    while (boundsBox.bounds.Contains(pathGen.pth.P4) == false) //this could use some behavior that diverts butter path in opposite direction than just tried
                    {
                        i++;
                        pathGen.newCurvePoints();
                        if(i>10)
                        {
                            Debug.LogError("tried too many times to path butter");
                            break;
                        }
                    }
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
                        followPath.transform.position = Quaternion.Euler(0, 0, 90) * bez.CurveTangentLine(tObject); //follow line perpendicular to curve tangent line
                    }
                    else
                    {
                        tVecPrevious = followPath.transform.position;//storing previous position vector
                        followPath.transform.position = bez.Curve(tObject);//assigning new position vector based on curve function
                        followPath.transform.LookAt(followPath.transform.position + (followPath.transform.position - tVecPrevious));//subtracting one from the other and adding to current to find facing vector/direction and assigning lookAt vector
                    }
                }
            }
        }
        
    }

  
}

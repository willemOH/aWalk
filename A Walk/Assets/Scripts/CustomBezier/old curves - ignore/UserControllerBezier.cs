
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[ExecuteInEditMode]
public class UserControllerBezier : MonoBehaviour //POSSIBLY CREATE CONTROLLER SCRIPT FOR ALL BEZIER SCRIPTS
{
    public Camera cam;
    public Transform endPoint;
    public Transform startPoint;
    public Transform movingObject;
    //public MoveAlongBezier moveAlongScrip;
    public BezierMoveBasic bezScrip;


    public bool objectMove = false;
    public float t = 0;
    [Range(1f, 50f)]
    public float speed = 1;


    private void Start()
    {
        //moveAlongScrip = this.GetComponent<MoveAlongBezier>();
        
        //next step is to find the object and set p0 to be the object position on click
    }

    private void OnEnable()
    {
        bezScrip = this.GetComponent<BezierMoveBasic>();
        endPoint = this.transform.Find("P3");
        startPoint = this.transform.Find("P0");
    }


    void Update()
    {
        var fingerCount = 0;
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
            {
                fingerCount++;
            }
        }
        if (fingerCount > 0)
        {
            routeDirection();
        }


        if (bezScrip.tObject < 1f && objectMove == true)
        {
            t += Time.deltaTime / speed;
            bezScrip.tObject = Mathf.Lerp(0f, 1f, t);
        }

        if (bezScrip.tObject >= 1f)
        {
            bezScrip.P0.position = bezScrip.P3.position;
            bezScrip.tObject = 0f;
            //t = 0;
            objectMove = false;
           
        }

    }

    void routeDirection()
    {
        t = 0;
        Ray ray = cam.ScreenPointToRay(Input.touches[0].position);
        RaycastHit hit;
        //Debug.Log(Input.touches[0].position);

        if (Physics.Raycast(ray, out hit))
        {
            endPoint.position = hit.point;
           
        }

        startPoint.position = bezScrip.followPath.transform.position;
        objectMove = true;
    }
}

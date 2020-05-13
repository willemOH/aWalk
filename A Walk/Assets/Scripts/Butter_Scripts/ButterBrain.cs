using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterBrain : MonoBehaviour
{
    public bool fly = false;
    public bool land = false;
    public bool idle = false;
    public bool bezGUI = false;

    public tController tcon;

    public float flyspeed = 1;
    private float current_wingspeed = 1;
    public float wingSpeed;
    
    public float wingSpeedToDistanceMultiplier = 1.2f;
    public float currentSetSpeed;
    public float currentSpeed;
    public float wingSpeedLive;
    public float lerpTune = .5f;
    [Range(.1f, 1f)]
    public float sloMo = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fly == true)
        {
            idle = false;
            current_wingspeed = wingSpeed;
            tcon.butterState = tController.state.flying;

            GetComponentInChildren<tController>().globalSpeed = flyspeed * (1 - sloMo);
            GetComponentInChildren<tController>().lerpTune = lerpTune;
            //butterList[0].GetComponentInChildren<Animator>().speed = wingspeed / butterList[0].GetComponent<tController>().current_speed * wingSpeedToDistanceMultiplier; //changing animator speed every frame is causing wing freeze - update every other frame etc?
            GetComponentInChildren<Animator>().speed = current_wingspeed * (1 - sloMo); //what previous line was when it worked
            currentSetSpeed = GetComponentInChildren<tController>().speed;
            currentSpeed = GetComponentInChildren<tController>().current_speed;
            wingSpeedLive = GetComponentInChildren<Animator>().speed;
        }
        if (land == true)
        {
            tcon.butterState = tController.state.landing;
            fly = false;
            if (idle == true)
            {
                current_wingspeed = 1;
                tcon.butterState = tController.state.idle;
                land = false;
            }
        }
    }
}

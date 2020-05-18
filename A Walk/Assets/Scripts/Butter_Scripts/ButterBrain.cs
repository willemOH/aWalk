using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class ButterBrain : MonoBehaviour
{
    
    //add flapcycle weights depending on velocity
    public bool fly = false;
    public bool land = false;
    public bool idle = false;
    public bool bezGUI = false;

    public Material eyesOpaque; //have to change to this non-transparent material after becoming untransparent or eyes are seen through body
    
    public Material butterWingR;
    public Material butterWingL;
    public Material butterBodMat;
    public Renderer eyeMesh;
    public tController tcon;

    public Animator anim;

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

    public float transitionTime = 0;
    public float timeToFade = 2f;
    public float test_t = 0;
    public float colorValRand;
    // Start is called before the first frame update
    void Start()
    {
       
        colorValRand = Random.Range(0f, 400f);
        butterWingR.SetFloat("Butter_Col", colorValRand);
        butterWingL.SetFloat("Butter_Col", colorValRand);
        butterBodMat.SetFloat("Body_Col", colorValRand);
        eyesOpaque.SetFloat("Body_Col", colorValRand);
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        /* 
        test_t += Time.deltaTime;
        if(test_t > .5f)
        {
            fly = true;
        }
        */
        wingSpeedLive = anim.speed;
       
        if (fly == true)
        {
            idle = false;
            current_wingspeed = wingSpeed;
            tcon.butterState = tController.state.flying;

            GetComponentInChildren<tController>().globalSpeed = flyspeed * (1 - sloMo);
            GetComponentInChildren<tController>().lerpTune = lerpTune;
            //butterList[0].GetComponentInChildren<Animator>().speed = wingspeed / butterList[0].GetComponent<tController>().current_speed * wingSpeedToDistanceMultiplier; //changing animator speed every frame is causing wing freeze - update every other frame etc?
            anim.speed = current_wingspeed * (1 - sloMo); //what previous line was when it worked
            currentSetSpeed = GetComponentInChildren<tController>().speed;
            currentSpeed = GetComponentInChildren<tController>().current_speed;
            
        }
        if (idle == true)
        {
           
            anim.speed = 1;
            current_wingspeed = 1;
            tcon.butterState = tController.state.idle;
            fly = false;
        }
        /*
        if (land == true)
        {
            tcon.butterState = tController.state.landing;
            
            fly = false;
            if (idle == true)
            {
                anim.speed = 1;
                current_wingspeed = 1;
                tcon.butterState = tController.state.idle;
                land = false;
            }
        }
        */
    }
}

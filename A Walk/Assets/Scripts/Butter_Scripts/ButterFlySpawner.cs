using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterFlySpawner : MonoBehaviour
{
    public GameObject ButterModel;
    public GameObject BezierGenPrefab;
    public RuntimeAnimatorController animator;
    public bool spawnButter = false;
    public bool fly = false;
    public bool land = false;
    public bool idle = false;
    public bool bezGUI = false;
    List<GameObject> butterList = new List<GameObject>();

    private tController tcon;
    public float flyspeed = 1;
    public float wingspeed = 1;
    private float selectedWingSpeed = 5;
    public float wingSpeedToDistanceMultiplier = 1.2f;
    public float currentSetSpeed;
    public float currentSpeed;
    public float wingSpeedLive;
    public float lerpTune =.5f;
    [Range (.1f,1f)]
    public float sloMo = 0;

    void Update()
    {
        
        if(spawnButter == true)
        {
            StartCoroutine("NewButter");
            spawnButter = false;
        }
        if (butterList.Count >0)
        {
            butterList[0].GetComponentInChildren<tController>().globalSpeed = flyspeed * (1 - sloMo);
            butterList[0].GetComponentInChildren<tController>().lerpTune = lerpTune;
            //butterList[0].GetComponentInChildren<Animator>().speed = wingspeed / butterList[0].GetComponent<tController>().current_speed * wingSpeedToDistanceMultiplier; //changing animator speed every frame is causing wing freeze - update every other frame etc?
            butterList[0].GetComponentInChildren<Animator>().speed = wingspeed *(1 - sloMo); //what previous line was when it worked
            currentSetSpeed = butterList[0].GetComponentInChildren<tController>().speed;
            currentSpeed = butterList[0].GetComponentInChildren<tController>().current_speed;
            wingSpeedLive = butterList[0].GetComponentInChildren<Animator>().speed;


        }
        if (fly == true)
        {
            idle = false;
            wingspeed = selectedWingSpeed;
            tcon.butterState = tController.state.flying;
        }
        if (land == true)
        {
            tcon.butterState = tController.state.landing;
            fly = false;
            if(idle == true)
            {
                wingspeed = 1;
                tcon.butterState = tController.state.idle;
                land = false;
            }
        }
    }

    IEnumerator NewButter()
    {
        GameObject butterInst = Instantiate(ButterModel);
        GameObject butterMesh = butterInst.transform.GetChild(0).gameObject; //getting child mesh and adding animator
        Animator butterAnim = butterMesh.AddComponent<Animator>();           //
        butterAnim.runtimeAnimatorController = animator;                     //
        GameObject bezierFabInst = Instantiate(BezierGenPrefab);
        tcon = bezierFabInst.GetComponent<tController>();
        tcon.followPath = butterInst;
        bezierFabInst.transform.parent = butterInst.transform;
        butterInst.transform.parent = this.transform;
        //butterInst.transform.parent = bezierFabInst.transform;
        //bezierFabInst.transform.parent = this.transform;
        butterList.Add(butterInst); //question for justin - better to have butterfly in bezierfab or bezierfab in butterfly for adding to list?
        selectedWingSpeed = wingspeed;
        wingspeed = 1;
        tcon.butterState = tController.state.idle;

        yield return null;

        //more programatic approach
        //GameObject butterInst = new GameObject();
        //butterInst.AddComponent<tController>();
        //butterInst.AddComponent<BezierPathGen>();
        //BezierPathGen pathgen = butterInst.GetComponent<BezierPathGen>();
        //pathgen.butterfly = true;
    }

    

}

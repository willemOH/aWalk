using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterFlySpawner : MonoBehaviour
{
    public GameObject ButterModel;
    public GameObject BezierGenPrefab;
    public RuntimeAnimatorController animator;
    public bool spawnButter = false;
    List<GameObject> butterList = new List<GameObject>();

    

    private float selectedWingSpeed = 5;
    public float wingspeed = 1;
    public float flyspeed = 1;

    void Update()
    {
        
        if(spawnButter == true)
        {
            StartCoroutine("NewButter");
            spawnButter = false;
        }
        if (butterList.Count >0)
        {
           

        }
       
    }

    IEnumerator NewButter()
    {
        GameObject butterInst = Instantiate(ButterModel);
        ButterBrain brain = butterInst.AddComponent<ButterBrain>();
        GameObject butterMesh = butterInst.transform.GetChild(0).gameObject; //getting child mesh and adding animator
        Animator butterAnim = butterMesh.AddComponent<Animator>();           //
        butterAnim.runtimeAnimatorController = animator;                     //
        GameObject bezierFabInst = Instantiate(BezierGenPrefab);
        brain.tcon = bezierFabInst.GetComponent<tController>();
        brain.tcon.followPath = butterInst;
        bezierFabInst.transform.parent = butterInst.transform;
        butterInst.transform.parent = this.transform;
        butterList.Add(butterInst);
        brain.wingSpeed = wingspeed;
        brain.flyspeed = flyspeed;
        brain.tcon.butterState = tController.state.idle;

        yield return null;

        //more programatic approach
        //GameObject butterInst = new GameObject();
        //butterInst.AddComponent<tController>();
        //butterInst.AddComponent<BezierPathGen>();
        //BezierPathGen pathgen = butterInst.GetComponent<BezierPathGen>();
        //pathgen.butterfly = true;
    }

    

}

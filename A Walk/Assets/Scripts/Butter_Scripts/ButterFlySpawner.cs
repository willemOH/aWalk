using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class ButterFlySpawner : MonoBehaviour
{
    public GameObject ButterModel;
    public GameObject BezierGenPrefab;
    public GameObject Hand;

    private GameObject butterInst;
    private GameObject bezierFabInst; //add control to flying to this script // think about what controls when the butterfly
    //gets close to hand and slows down
    //public RuntimeAnimatorController animator;
    public bool spawnButter = false;
    public bool release = false;
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
        if (release != true)
        {
            butterInst.transform.position = Hand.transform.position;
        }
        bezierFabInst.GetComponent<BezierPathGen>().startLoc = Hand.transform.position;
    }

    IEnumerator NewButter()
    {
        butterInst = Instantiate(ButterModel,Hand.transform); //Model Instantiation
        butterInst.transform.parent = this.transform;

        bezierFabInst = Instantiate(BezierGenPrefab); //Bezier controller instantiation
        bezierFabInst.transform.parent = butterInst.transform;

        ButterBrain brain = butterInst.GetComponent<ButterBrain>(); //get behavior controller
        brain.tcon = bezierFabInst.GetComponent<tController>(); //getting butterfly behavior controller and assigning parameters
        brain.tcon.followPath = butterInst;
        brain.wingSpeed = wingspeed;
        brain.flyspeed = flyspeed;
        brain.tcon.butterState = tController.state.idle;
        brain.butterBodMat = butterInst.transform.Find("butterV8/body").gameObject.GetComponent<SkinnedMeshRenderer>().material;
        brain.eyeMesh = butterInst.transform.Find("butterV8/body/eyes").gameObject.GetComponent<SkinnedMeshRenderer>(); //target new material from eyereplace script 
        brain.eyesOpaque = butterInst.transform.Find("butterV8/body/eyes").gameObject.GetComponent<SkinnedMeshRenderer>().material;
        brain.butterWingL = butterInst.transform.Find("butterV8/wings/Bone.L/wingL").gameObject.GetComponent<SkinnedMeshRenderer>().material;
        brain.butterWingR = butterInst.transform.Find("butterV8/wings/Bone.R/wingR").gameObject.GetComponent<SkinnedMeshRenderer>().material;
        //could maybe move some of these things to start() of butterbrain
        butterList.Add(butterInst);
       
        yield return null;

        //more programatic approach
        //GameObject butterInst = new GameObject();
        //butterInst.AddComponent<tController>();
        //butterInst.AddComponent<BezierPathGen>();
        //BezierPathGen pathgen = butterInst.GetComponent<BezierPathGen>();
        //pathgen.butterfly = true;
    }

    

}

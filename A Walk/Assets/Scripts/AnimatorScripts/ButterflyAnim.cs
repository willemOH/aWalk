using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyAnim : MonoBehaviour
{
    tController tCon;
    public Animator anim;
    
    void Start()
    {
        anim = this.transform.parent.GetComponentInChildren<Animator>();
        tCon = this.transform.parent.GetComponentInChildren<tController>();
    }
    void Update()
    {
        anim.SetInteger("State", (int)tCon.butterState); //sets "state" animator parameter with int of butterState enum
    }

    
  
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyAnim : MonoBehaviour
{
    tController tCon;
    public Animator anim;
    
    void Start()
    {
        anim = this.GetComponentInChildren<Animator>();
        tCon = this.GetComponent<tController>();
    }
    void Update()
    {
        anim.SetInteger("State", (int)tCon.butterState); //sets "state" animator parameter with int of butterState enum
        
    }

    
  
}

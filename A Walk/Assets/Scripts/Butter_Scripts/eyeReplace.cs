using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eyeReplace : MonoBehaviour
{
    Animator anim;
    Renderer eyeMaterial;
    public Material eyeOpaque;
    void Start()
    {
        anim = GetComponent<Animator>();
        eyeMaterial = this.transform.Find("body/eyes").gameObject.GetComponent<SkinnedMeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(anim.GetBool("ButterAppeared") == true)
        {
            eyeMaterial.material = eyeOpaque;
            this.enabled = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeMeshVisualizer : MonoBehaviour
{
    public TreeRandom treeMesh;
    public GameObject vertMarker;
    public float markerSize = 10f;
    Vector3 scale;
    
    void Awake()
    {
        treeMesh = this.GetComponentInParent<TreeRandom>();
        //vertMarker = this.GameObject.Find("vertMarker(Clone)");
        scale = new Vector3(1, 1, 1) * markerSize;
    }
    
    void Start()
    {
        vertMarker = this.transform.Find("vertMarker(Clone)").gameObject;
        Invoke("LateStart",1f);
       
    }

    void LateStart()
    {
        //int i = 1;
        for(int i=0; i <= treeMesh.Verts.Length - 1; i++ )
        {
            GameObject marker = Instantiate(vertMarker, treeMesh.Verts[i], Quaternion.identity);
            marker.name = $"VM {i}";
            marker.transform.localScale = scale;
      
        }
    }


  
}

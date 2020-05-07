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
        Invoke("LateStart",2f);
       
    }

    void LateStart()
    {
        int i = 1;
        foreach (Vector3 vert in treeMesh.Verts)
        {
            GameObject marker = Instantiate(vertMarker, vert, Quaternion.identity);
            marker.name = $"VM {i}";
            marker.transform.localScale = scale;
            i++;
        }
    }


  
}

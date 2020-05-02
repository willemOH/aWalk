using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeRingTester : MonoBehaviour
{
    public GameObject testobj;
    TreeRing ring;
    public int subdivY;
    public Vector3 point;
    private void Awake()
    {
        ring = new TreeRing(subdivY, point, Vector3.Cross(point + new Vector3(0, 1, 0), new Vector3(1, 0, 0)));
    }
    void Start()
    {
        foreach(Vector3 point in ring.verts)
        {
            Instantiate(testobj, point, Quaternion.identity);
        }
    }

  
}

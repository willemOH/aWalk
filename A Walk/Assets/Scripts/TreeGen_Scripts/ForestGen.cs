using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestGen : MonoBehaviour
{
    //public Object testObj;
    //GameObject tree;
    public int Number_of_Segments = 3;
    public int Rings_X = 3; //# of Segment rings
    public int Rings_Y = 6; //# of vertices on Segment rings
    public int branchDensity = 1;
    public float branchVariance = 0;
    
    public bool generate = false;
    public bool testGenerate = false;

    private void Start()
    {
        
    }
    void Update()
    {
        if (generate)
        {
            GameObject newTree = new GameObject("TreeInst");
            newTree.transform.parent = this.transform;
            TreeRandom treeScript = newTree.AddComponent<TreeRandom>();
            treeScript.segNum = Number_of_Segments;
            treeScript.subdivX = Rings_X;
            treeScript.subdivY = Rings_Y;
            generate = false;
        }
        if (testGenerate)
        {
            StartCoroutine("testGen");
            testGenerate = false;
        }
    }
    
   /* 
    IEnumerator testGen()
    {
        Mesh test = new Mesh();

        Vector3[] Verts = new Vector3[] {new Vector3(0, 1, 0),
                                        new Vector3(1, 1, 0),
                                        new Vector3(0, 0, 0),
                                        new Vector3(1, 0, 0)};

        Vector2[] uvs = new Vector2[] {new Vector2(0, 1),
                                        new Vector2(1, 1),
                                        new Vector2(0, 0),
                                        new Vector2(1, 0)};

        int[] Tris = new int[] { 0, 1, 2, 2, 1, 3 };


        Tree = new Mesh();

        Tree.vertices = Verts;
        Tree.triangles = Tris;
        Tree.uv = uvs;
        treeObject = new GameObject("Tree", typeof(MeshFilter), typeof(MeshRenderer));
        treeObject.transform.localScale = new Vector3(1, 1, 1);
        MeshFilter treeMeshFilt = treeObject.GetComponent<MeshFilter>();
        treeMeshFilt.mesh = Tree;

        yield return null;
    }
    */

    float[] branchDistributions(int SegmentCount) //revisit array of Segments to determine order to see how many branches it gets
    {
        float increment = 1 / Rings_X; 
        float[] tDistribs = new float[branchDensity * SegmentCount];
        for(int i=0; i<Rings_X; i++)
        {
            tDistribs[i] = increment * i + Random.Range(-branchVariance, branchVariance);
        }

        return tDistribs;
    }

    
}

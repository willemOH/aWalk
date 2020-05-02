using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public struct Tree
{
    internal Tree(List<TreeSeg> treeSeg)
    {
        segList = treeSeg;
    }
    List<TreeSeg> segList { get; }


}
*/

[RequireComponent(typeof(BezierPathGen))]

public class TreeRandom : MonoBehaviour
{
    List<TreeSeg> treeSegs = new List<TreeSeg>(); //tree TreeSegs
    public BezierPathGen pthGen;
    public Mesh Tree;
    GameObject treeObject;
    public Vector3[] Verts; //verts arranged for mesh formation

   // [HideInInspector] to uncomment when testng is over
    public int segNum=0;
    public int subdivX=0; //# of TreeSeg rings
    public int subdivY=0;


    /* can't do constructor with components because of monobehavior and adding values seperate from void Start
    public TreeRandom(int seg, int divY, int divX)
    {
        segNum = seg;
        subdivX = divX;
        subdivY = divY;
    }
    */
    void Awake()
    {
        pthGen = this.GetComponent<BezierPathGen>();
    }
    private void Start()
    {
        //need to ENABLE TREE IN BEZPATHGEN 
        StartCoroutine("generateTreeMesh");
        if (segNum == 0 || subdivX == 0 || subdivY == 0)
        {
            throw new ArgumentException("segNum subdivX or subdivY cannot be zero");
        }
    }


    IEnumerator generateTreeMesh()
    {
        //gen = new TreeGen(Number_of_TreeSegs, Rings_Y, Rings_X);
        for (int i = 0; i <= segNum - 1; i++)
        {
            addTreeSeg(pthGen.pth); //.pth is not being populated with p1 values my last guess was that BezierPathGen was not init Ps before this function but replaceing Start witht Awake() with  didn't help
            pthGen.newCurvePoints();
        }

        Tree = new Mesh();
        Verts = meshVerts();
        Tree.vertices = Verts;
        Tree.triangles = Triangles();
        Tree.uv = Uvs();

        treeObject = new GameObject("Tree", typeof(MeshFilter), typeof(MeshRenderer));
        treeObject.transform.localScale = new Vector3(1, 1, 1);
        treeObject.transform.parent = this.transform;
        MeshFilter treeMeshFilt = treeObject.GetComponent<MeshFilter>();
        treeMeshFilt.mesh = Tree;


        yield return null;
    }
    public int[] Triangles()
    {
        int ringVerts = subdivY;
        int[] tris = new int[Verts.Length * 6 - subdivY * 6];
        int j = 0;
        for (int i = 0; i <= Verts.Length - subdivY - 2; i++) //populate tri array with proper index location of verts for triangle from vert construction path
        {
            tris[j] = i;
            tris[j + 1] = ringVerts + i;
            tris[j + 2] = ringVerts + 1 + i;
            tris[j + 3] = ringVerts + 1 + i; //same on purpose
            tris[j + 4] = i;
            tris[j + 5] = i + 1;

            j += subdivY;
        }
        return tris;
    }

    public Vector2[] Uvs()
    {
        Vector2[] uvs = new Vector2[Verts.Length];
        int r = 0; //verts in row - corresponds to % U coordinate
        int s = 0; //# of segs - corresponds to % of V coordinate
        for (int i = 0; i < Verts.Length - 1; i++)
        {
            if (r >= subdivY)
            {
                r = 0;
                s++;
            }
            uvs[i] = new Vector2(r / 6, s / subdivX * segNum);
            r++;
        }
        return uvs;
    }
    public Vector3[] meshVerts() //retrieve list of all verts in every TreeSeg for entire tree mesh creation
    {
        Vector3[] verts = new Vector3[treeSegs.Count * treeSegs[0].vertsInSeg]; //TreeSegs * verts in TreeSegs = total # verts in TreeSeg
        TreeSeg sGeneric = treeSegs[0]; //all TreeSegs should have same vertex count data - using count of data from first seg for all segs
        for (int i = 0; i <= treeSegs.Count - 1; i++)
        {
            for (int j = 0; j <= sGeneric.ringsNum - 1; j++)
            {
                for (int l = 0; l <= sGeneric.vertsNum - 1; l++)
                {
                    verts[i * sGeneric.vertsInSeg + j * sGeneric.ringsNum + l] = treeSegs[i].ringVerts[j].verts[l];
                }
            }
        }

        return verts;
    }

    public void addTreeSeg(Bezier path)
    {
        TreeSeg seg = new TreeSeg(subdivX, subdivX, path);
        treeSegs.Add(seg);
    }

    

}

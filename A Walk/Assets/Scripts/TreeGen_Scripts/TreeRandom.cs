﻿using System;
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

    //public bool meshGenerated = false;


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
        int[] tris = new int[Verts.Length * 6 - subdivY * 6]; //each vert * # of verts to make a plane - verts in last ring (to close off the top of the tree)
        int j = 0;
        for (int i = 0; i <= Verts.Length - subdivY - 1; i++) //populate tri array with proper index location of verts for triangle from vert construction path
        {
            if ((j + 6) % (6*(subdivY)) == 0 && j != 0) //if reached last plane to close ring, different triangle assignment to hook first verts in ring to last
            {
                tris[j] = i; //is 5
                tris[j + 1] = (ringVerts + 1 + i) - subdivY; //needs = 6 //is currently 12 //subtract subdivY
                tris[j + 2] = ringVerts + i; 
                tris[j + 3] = (ringVerts + 1 + i) - subdivY; 
                tris[j + 4] = i;
                tris[j + 5] = (i + 1)-subdivY; //needs to be 0 //is curretly 6 //subtract subdivY
                //j -= 6; //taking away a plane (6 verts) increment every time a ring is linked back to its first plane
                //i -= subdivY; //so that tri index assignment starts where this assignment ended
            } 
            else
            {
                tris[j] = i;
                tris[j + 1] = ringVerts + 1 + i; //should be 12 is 13
                tris[j + 2] = ringVerts + i;
                tris[j + 3] = ringVerts + 1 + i; //same on purpose
                tris[j + 4] = i;
                tris[j + 5] = i + 1;
            }
            j += 6;
        }
        return tris;
    }

    public Vector2[] Uvs()
    {
        /*Vector2[] uvs = new Vector2[Verts.Length];
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
        */
        Vector2[] uvs = new Vector2[Verts.Length];
        int i = 0;
        while (i < uvs.Length)
        {
            //float width = 
            uvs[i] = new Vector2(Verts[i].y, Verts[i].x); //divide these x and y values by width and height of the plane
            i++;
        }
        return uvs;

    }
    /*
    public Vector3[] meshVerts() //retrieve list of all verts in every TreeSeg for entire tree mesh creation
    { //new approach -- establish ground ring then loop through by seg num for subsequent
        Vector3[] verts = new Vector3[(treeSegs.Count + 1) * subdivY]; //TreeSegs * verts in TreeSegs = total # verts in TreeSeg
        TreeSeg sGeneric = treeSegs[0]; //all TreeSegs should have same vertex count data - using count of data from first seg for all segs
        for (int l = 0; l <= sGeneric.vertsInRing - 1; l++) //populate from initial ground ring
        {
            verts[l] = treeSegs[0].ringVerts[0].verts[l]; //apply messy fix here
        }
        for (int i = 1; i <= treeSegs.Count - 1; i++) //populate from subsequent rings
        {
            for (int j = 0; j <= sGeneric.ringsNum - 1; j++)
            {
                for (int l = 0; l <= sGeneric.vertsInRing - 1; l++)
                {
                    verts[i * (sGeneric.vertsInSeg - sGeneric.vertsInRing) + j * sGeneric.vertsInRing + l] = treeSegs[i].ringVerts[j].verts[l];
                }
            }
        }

        return verts;
    }
    */// key here was knowing an list can be pushed to an array and then can use simplifying list functions

    public Vector3[] meshVerts()
    {
        List<Vector3> allVerts = new List<Vector3>();
        foreach(TreeSeg segment in treeSegs)
        {
            foreach(TreeRing ring in segment.ringVerts)
            {
                foreach (Vector3 vert in ring.verts)
                {
                    allVerts.Add(vert);
                }
            }
        }
        return allVerts.ToArray();
    }

        


    public void addTreeSeg(Bezier path)
    {
        TreeSeg seg = new TreeSeg(subdivX, subdivY, path);
        treeSegs.Add(seg);
    }

    

}

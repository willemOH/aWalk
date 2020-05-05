using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

//beginning of unit tests
namespace Tests
{
    public class TreeGen_Tests : MonoBehaviour
    {
        GameObject treeTestObject = new GameObject();
        TreeRandom tree;
        TreeSeg seg;
        TreeRing ring;                             

        [SetUp]
        public void Setup()
        {

            tree = treeTestObject.AddComponent<TreeRandom>();
            tree.segNum = 3;
            tree.subdivX = 4;
            tree.subdivY = 3;
            seg = treeTestObject.GetComponent<TreeSeg>();
            ring = treeTestObject.GetComponent<TreeRing>();
        }

        // A Test behaves as an ordinary method
        [Test]
        public void RingTest()
        {
        int subdivY = 6;
        Vector3 point = new Vector3(0,0,0);
        TreeRing ring = new TreeRing(subdivY, point, Vector3.Cross(point + new Vector3(0, 1, 0), new Vector3(1, 0, 0))); ;
        Assert.AreEqual(ring.verts[0], new Vector3(0f,0f,-1.0f));
        Assert.AreEqual(ring.verts[1], new Vector3(-.9f, 0f, -.5f));
        Assert.AreEqual(ring.verts[2], new Vector3(-.9f, 0f, .5f));
        Assert.AreEqual(ring.verts[0], new Vector3(0f, 0f, 1f));
        Assert.AreEqual(ring.verts[2], new Vector3(.9f, 0f, .5f));
        Assert.AreEqual(ring.verts[1], new Vector3(.9f, 0f, -.5f));
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        //[UnityTest]
        //public IEnumerator subDivTest()
        //{
        //    //gen.subDivVertGen()
        //    // Use the Assert class to test conditions.
        //    // Use yield to skip a frame.
        //    //yield return null;
        //}
    }
}

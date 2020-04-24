using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

//beginning of unit tests
namespace Tests
{
    public class TreeGen_Tests
    {
        GameObject treeTestObject = new GameObject();
        TreeRandom tree;
        TreeSeg seg;
        TreeRing ring;

        [SetUp]
        public void Setup()
        {
            tree = treeTestObject.AddComponent<TreeRandom>();
            tree.segNum = 4;
            tree.subdivX = 4;
            tree.subdivY = 4;
            seg = treeTestObject.GetComponent<TreeSeg>();
            ring = treeTestObject.GetComponent<TreeRing>();
        }
        
        // A Test behaves as an ordinary method
        [Test]
        public void NewRing()
        {
            /*
            Bezier testBez = new Bezier();
            testBez.P1 = new Vector3(1, 0, 0);
            testBez.P2 = new Vector3(1, .2f, 0);
            testBez.P3 = new Vector3(2, .2f, 0);
            testBez.P4 = new Vector3(2, 0, 0);
            */
            Vector3 perpendicularVec = new Vector3(1, 0, 0);
            ring.subdivY = tree.subdivY;
            Ring testRing = ring.NewRing(new Vector3(0,1,0), perpendicularVec); //simulated point along curve
                                                                               //point and point on 90 degrees axis to other point
            float Yincrement = 360 / tree.subdivY;
            Assert.AreEqual(testRing.verts[0], new Vector3(1, 1, 0));
            Assert.AreEqual(testRing.verts[1], (Quaternion.Euler(0, Yincrement * 1, 0) * perpendicularVec));
            Assert.AreEqual(testRing.verts[2], (Quaternion.Euler(0, Yincrement * 2, 0) * perpendicularVec));
            Assert.AreEqual(testRing.verts[3], (Quaternion.Euler(0, Yincrement * 3, 0) * perpendicularVec));

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

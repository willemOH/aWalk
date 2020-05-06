using System;
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
        float epsilon;

        [SetUp]
        public void Setup()
        {

            tree = treeTestObject.AddComponent<TreeRandom>();
            tree.segNum = 3;
            tree.subdivX = 4;
            tree.subdivY = 3;
            epsilon = .1f;
        
        }

        // A Test behaves as an ordinary method
        [Test]
        public void RingTest()
        {
            int subdivY = 6;
            Vector3 point = new Vector3(0, 0, 0);
            ring = new TreeRing(subdivY, point, Vector3.Cross(point + new Vector3(0, 1, 0), new Vector3(1, 0, 0))); ;
            
            //these points were determined by instantiating cubes at these points (from this TreeRing calculation
            //cubes were seen in engine as forming a circle around the origin point
            Assert.AreEqual(ring.verts[0].x, 0f);
            Assert.AreEqual(ring.verts[0].y,0f);
            Assert.AreEqual(ring.verts[0].z, -1.0f);

            Assert.LessOrEqual(Mathf.Abs(ring.verts[1].x - (-.9f)), epsilon);
            Assert.AreEqual(ring.verts[1].y, 0f);
            Assert.AreEqual(ring.verts[1].z, -.5f);
            /*
            Assert.AreEqual(ring.verts[1], new Vector3(-.9f, 0f, -.5f));
            Assert.AreEqual(ring.verts[2], new Vector3(-.9f, 0f, .5f));
            Assert.AreEqual(ring.verts[0], new Vector3(0f, 0f, 1f));
            Assert.AreEqual(ring.verts[2], new Vector3(.9f, 0f, .5f));
            Assert.AreEqual(ring.verts[1], new Vector3(.9f, 0f, -.5f));
            */
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
        [Test]
        public void SegTest()
        {
            int subDivX = 3;
            float Xincrement = 1f / (float)subDivX;
            Bezier bez = new Bezier();
            seg = new TreeSeg(subDivX,6, bez);
            //Assert.AreEqual(seg.ringVerts[0].curvePoint.x, bez.Curve(Xincrement).x);
            //Assert.AreEqual(seg.ringVerts[0].curvePoint.y, bez.Curve(Xincrement).y);
            //Assert.AreEqual(seg.ringVerts[0].curvePoint.z, bez.Curve(Xincrement).z);

            Assert.AreEqual(seg.ringVerts[0].curvePoint.x, 0);
            Assert.AreEqual(seg.ringVerts[0].curvePoint.y, 0);
            Assert.AreEqual(seg.ringVerts[0].curvePoint.z, 0 );

            Assert.AreEqual(seg.ringVerts[1].curvePoint.x, bez.Curve(Xincrement).x);
            Assert.AreEqual(seg.ringVerts[1].curvePoint.y, bez.Curve(Xincrement).y);
            Assert.AreEqual(seg.ringVerts[1].curvePoint.z, bez.Curve(Xincrement).z);




        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointPathGen : MonoBehaviour
{
    public List<Vector3> points = new List<Vector3>();
    private float timer = 0;
    public float pointSpawnTime = 4; //probably will want to tie to point destroy function and object along curve movement speed

    

    public float randRangeX;
    public float randRangeY;
    public float randRangeZ;

    public List<List<Vector3>> curves = new List<List<Vector3>>();



    void Start()
    {
        
        for (int i =0; i< 8; i++) //test populate with 7 points (3 sets of points overlapping) 
        {
            Vector3 newPoint = new Vector3(i, i, i);
            points.Add (newPoint);
        }
        StartCoroutine("AddCurve");
        

    }
    IEnumerator AddCurve()
    {
        if (curves.Count > 1)
        {
            curves.RemoveAt(0);
        }
        for (int i = 0; i < 6; i += 2)
        {
            curves.Add(new List<Vector3> { points[i], points[i + 1], points[i + 2] }); 

            //curves.Add(Curve(p1, p2, p3, t));
            
        }
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {

    //timer += Time.deltaTime;
    //if (timer > 5)
    //{
    //    StartCoroutine("addCurve");
    //    timer -= timer;
    //}


    //timer += Time.deltaTime;

    //if (timer % Mathf.Round(pointSpawnTime) == 0)
    //{
    //    float randX = Random.Range(0.0f, randRangeX);
    //    float randY = Random.Range(0.0f, randRangeY);
    //    float randZ = Random.Range(0.0f, randRangeZ);
    //    points.Add(new Vector3(randX, randY, randZ));
    //}
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class constantSpawn : MonoBehaviour
{
    ButterFlySpawner spawner;
    public float interval = 1f;
    public float time = 0;
    void Start()
    {
        spawner = this.GetComponent<ButterFlySpawner>();
        Invoke("wait",30f);
    }

    void Update()
    {
        time += Time.deltaTime;
        if(time > interval)
        {
            spawner.spawnButter = true;
            time -= time;
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(30);
    }
}

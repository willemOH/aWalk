using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//a script to add scripts to children of current object - for adding test scripts to procedurally generated gameobject hierarchies

public class subScript : MonoBehaviour
{
    public GameObject scriptObject;
    public string scriptType;
    public string objectName;
    public bool findAndAssign = false;

    private void Update()
    {
        if (findAndAssign)
        {
            findAssign();
            findAndAssign = false;
        }
    }
    void findAssign()
    {
        GameObject obj = this.transform.Find(objectName).gameObject;
        obj.AddComponent(System.Type.GetType(scriptType));
        if (scriptObject)
            Instantiate(scriptObject, obj.transform);
    }
}

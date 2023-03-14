using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilitiesClass
{
    public static GameObject FindChildWithTag(GameObject parent, string tag)
    {
        GameObject child = null;

        foreach (Transform transform in parent.transform)
        {
            if (transform.CompareTag(tag))
            {
                child = transform.gameObject;
                break;
            }
        }

        return child;
    }  

    public static GameObject FindChildByName(GameObject parent, string name)
    {
        GameObject child = null;

        child = parent.transform.Find(name).gameObject;
        return child;
    }      

    public static List<GameObject> FindAllChildWithTag(GameObject parent, string tag)
    {
        List<GameObject> child = new List<GameObject>();
        foreach (Transform transform in parent.transform)
        {
            if (transform.CompareTag(tag))
            {
                child.Add(transform.gameObject);
            }
        }

        return child;
    }    

    public static List<GameObject> FindAllChildByLayer(GameObject parent, string layer)
    {
        List<GameObject> child = new List<GameObject>();
        foreach (Transform transform in parent.transform)
        {
            if (transform.gameObject.layer == LayerMask.NameToLayer(layer))
            {
                child.Add(transform.gameObject);
            }
        }
        return child;
    }              
}

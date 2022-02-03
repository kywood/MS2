using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    public Dictionary<int, GameObject> ResList = new Dictionary<int, GameObject>();

    void Awake()
    {
        ResList.Clear();
    }

    public bool MakePool(GameObject parent, string preFabsPath, int count)
    {
        if (parent == null)
        {
            return false;
        }

        for (int i = 0; i < count; i++)
        {
            //GameObject NewObj = Instantiate(Resources.Load(preFabsPath) as GameObject);
            GameObject NewObj = Util.AddChildWithOutScaleLayer(parent, Instantiate(Resources.Load(preFabsPath) as GameObject));

            NewObj.SetActive(false);

            ResList.Add(i, NewObj);
        }

        return true;
    }

    public GameObject GetAbleObject()
    {
        var Em = ResList.GetEnumerator();
        while (Em.MoveNext())
        {
            if (Em.Current.Value.activeSelf == false)
            {
                Em.Current.Value.SetActive(true);
                return Em.Current.Value;
            }
        }
        return null;
    }

}

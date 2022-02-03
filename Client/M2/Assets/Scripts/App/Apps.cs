using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apps : MonoBehaviour
{
    // Start is called before the first frame update
    static public Apps Instance = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public int GetA()
    {
        return 10;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DT<T> where T : DT<T>
{
    public static T Instance;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = (T)this;
    }
}


public class AP : DT<AP>
{
    public int GetA()
    {
        return 10;
    }

    public static void main()
    {
        Instance.GetA();
    }
}
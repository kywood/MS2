using UnityEngine;
using System.Collections;
using System.Text;
using System.Collections.Generic;

public class ListEx<T> : List<T>
{
    public void Rotate()
    {
        T last = this[ Count - 1];

        for (int i = Count - 1; i > 0; i--)
        {
            this[i] = this[i - 1];
        }
        this[0] = last;
    }

    

}
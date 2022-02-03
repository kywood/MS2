using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    private Text txTest;
    public GameObject cube;

    private int dir = 1;
    // Start is called before the first frame update
    void Start()
    {
        txTest = gameObject.GetComponent<Text>();
        //txTest = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        cube.GetComponent<Transform>().rotation = cube.transform.rotation * new Quaternion(0, 0.01f, 0, 1);

        txTest.fontSize+= dir;

        if(txTest.fontSize > 200 )
        {
            dir = -1;
        }
        else if (txTest.fontSize < 50 )
        {
            dir = 1;
        }
    }
}

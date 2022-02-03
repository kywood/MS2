using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        //Transform tr = GetComponent<Transform>();

        Transform tr = transform;
        // ¿Ãµø 

        float Acc = 0.1f;
        float dis = Time.deltaTime * Acc;

        tr.position = tr.position + new Vector3(dis, 0, 0);


        //tr.rotation = tr.rotation * new Quaternion(0, 0.01f, 0, 1);

    }
}

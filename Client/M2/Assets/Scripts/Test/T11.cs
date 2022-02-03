using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T11 : MonoBehaviour
{
    public GameObject Pick;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            //Vector3 mousePosition = ;

            //mousePosition.z = 0;

            //Pick.transform.position = new Vector3( Camera.main.ScreenToWorldPoint(Input.mousePosition));



            Vector3 scPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            scPos.z = 0;

            Pick.transform.position = scPos;

            Debug.Log(scPos);

            //Input.mousePosition;

        }
    }
}

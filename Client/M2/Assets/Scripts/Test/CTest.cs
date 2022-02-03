using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D r = GetComponent<Rigidbody2D>();

        float fforce = 180.0f;

        r.AddForce(new Vector2(fforce, fforce));
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = transform.position + new Vector3(0.0f, 0.1f, 0.0f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float fForce = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        //float fForce = 1.20f;

        GetComponent<Rigidbody2D>().AddForce(new Vector2(fForce, 0.0f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerEnter2D");

        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f, 100.0f));

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("OnCollisionEnter2D");

        gameObject.SetActive(false);


    }

}

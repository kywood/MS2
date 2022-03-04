using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRay : MonoBehaviour
{

    public GameObject Mark;
  
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

            transform.position = scPos;

            //Debug.Log(scPos);

            //Input.mousePosition;

        }

        //int layermask = (1<<11);
        //int layerMask = (-1) - ((1 << LayerMask.NameToLayer("GAME_RAY")));
        int layerMask = 1 << LayerMask.NameToLayer("GAME_RAY");


        //float radius = GameManager.Instance.BallController.GetMainBall().transform.GetComponent<SphereCollider>().radius;
        //float radius = GameManager.Instance.BallController.GetMainBall().transform.GetComponent<SphereCollider>().radius;
        float radius = 1.25f;

        RaycastHit hit;

        //Debug.Log("BB");

        RaycastHit2D hit2d = Physics2D.CircleCast(transform.position, radius, Vector2.up, Mathf.Infinity);



        if (hit2d)
        {

            //Vector3 vv = Util.CanvasChildToWorldPosition(Camera.main, vposShootBody);

            //HitMark.transform.position = vv + (Dir * hit2d.distance);

            //Circle.transform.position = vShootBodyPos + (Dir * hit2d.distance);

            Mark.transform.position = hit2d.point;

            //Debug.Log(hit2d.transform.position.ToString() );
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eGameState
{
    None = -1,

    ShootReady = 0,
    Shoot,
    Result,

    MAX
}


public class T15 : MonoBehaviour
{
    public GameObject Target;
    public GameObject ShootBody;
    public GameObject Bubble;


    public float ShootForce = 1.0f;
//    eGameState GameState = eGameState.ShootReady;

    Rigidbody2D RbBubble;

    bool bPress = false;

    // Start is called before the first frame update
    void Start()
    {
        bPress = false;
        //Bubble.SetActive(false);

        RbBubble = Bubble.GetComponent<Rigidbody2D>();
    }

    void Shoot()
    {

        Bubble.transform.position = ShootBody.transform.position;

       // Bubble.SetActive(true);

        Vector2 vStart = CMath.ConvertV3toV2(ShootBody.transform.position);
        Vector2 vEnd = CMath.ConvertV3toV2(Target.transform.position);




        float fAngle = CMath.GetAngle(vStart, vEnd);

        float RadianAngle = fAngle * Mathf.Deg2Rad;
        float fX = Mathf.Cos(RadianAngle);
        float fY = Mathf.Sin(RadianAngle);
        Vector3 vel = new Vector3(fX, fY, 0f);
        Vector2 vel2 = new Vector2(fX, fY);
        //RbBubble.velocity = transform.TransformDirection(vel) * ShootForce;

        //RbBubble.AddForce(transform.TransformDirection(vel) * ShootForce);
        RbBubble.velocity = vel * ShootForce;

        //RbBubble.AddForce( vel2 * ShootForce , ForceMode2D.Force);


        Debug.Log("Shoot Ang : " + fAngle + " vel " + vel);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            bPress = true;
            //Bubble.SetActive(false);

            Vector3 wPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            wPos.z = 0;
            Target.transform.position = wPos;

            float dis = Util.Distance(Target.transform.position, ShootBody.transform.position);
            float angle = CMath.GetAngle(Target.transform.position, ShootBody.transform.position);

            Debug.Log(" Dis : " + dis + " Angle : " + angle );

        }
        else
        {
            if (bPress)
            {
                bPress = false;
                Shoot();
            }
        }
    }
}

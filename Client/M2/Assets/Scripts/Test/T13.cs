using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class T13 : MonoBehaviour
{
    public GameObject mTarget;
    public GameObject mShootBody;

    eGameState mGameState = eGameState.ShootReady;

    bool mbPress = false;

    // Start is called before the first frame update
    void Start()
    {
        mbPress = false;
    }

    void Shoot()
    {

        Debug.Log("Shoot");

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            mbPress = true;

            Vector3 wPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            wPos.z = 0;
            mTarget.transform.position = wPos;

            float dis = Util.Distance(mTarget.transform.position, mShootBody.transform.position);
            float angle = CMath.GetAngle(mTarget.transform.position, mShootBody.transform.position);

            Debug.Log(" Dis : " + dis + " Angle : " + angle );

        }
        else
        {
            if (mbPress)
            {
                mbPress = false;
                Shoot();
            }
        }
    }
}

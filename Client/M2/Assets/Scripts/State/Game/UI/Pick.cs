using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pick : MonoBehaviour
{

    public GameObject Target;
    public GameObject ShootBody;
    public GameObject Line = null;

    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}

    void LateUpdate()
    {
        Line.transform.position = ShootBody.transform.position;




        Vector2 vStart = CMath.ConvertV3toV2(ShootBody.transform.position);
        Vector2 vEnd = CMath.ConvertV3toV2(Target.transform.position);

        float _fShootAngle = 0.0f;
        _fShootAngle = CMath.GetAngle(vStart, vEnd);

        Line.transform.rotation = Quaternion.Euler(new Vector3(0, 0, _fShootAngle));

        //Vector2 OriSize = Line.GetComponent<RectTransform>().sizeDelta;

        ////OriSize.x = Distance;
        //OriSize.x = 1000.0f;
        //Line.GetComponent<RectTransform>().sizeDelta = new Vector2(OriSize.x, OriSize.y);
        //Vector3 Dir = (Target.transform.position - transform.position).normalized;



        ////int layermask = (1<<11);
        //int layerMask = (-1) - ((1 << LayerMask.NameToLayer("Target")));



    }

}

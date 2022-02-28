using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pick : MonoBehaviour
{

    public GameObject Target;
    public GameObject ShootBody;
    public GameObject Line = null;
    public GameObject LineReverse = null;


    public GameObject ShootObject;

    //public GameObject HitMark ;
    //public GameObject Circle ;

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

        LineReverse.SetActive(false);

        Vector2 vStart = CMath.ConvertV3toV2(ShootBody.transform.position);
        Vector2 vEnd = CMath.ConvertV3toV2(Target.transform.position);

        float _fShootAngle = 0.0f;
        _fShootAngle = CMath.GetAngle(vStart, vEnd);

        Line.transform.rotation = Quaternion.Euler(new Vector3(0, 0, _fShootAngle));

        ShootObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, _fShootAngle - 90.0f));

        Vector2 OriSize = Line.GetComponent<RectTransform>().sizeDelta;

        Vector3 vShootBodyPos = new Vector3(ShootBody.transform.position.x, ShootBody.transform.position.y, 0.0f);
        Vector3 vTargetPos = new Vector3(Target.transform.position.x, Target.transform.position.y, 0.0f);

        OriSize.x = 1000.0f;
        Line.GetComponent<RectTransform>().sizeDelta = new Vector2(OriSize.x, OriSize.y);
        Vector3 Dir = (vTargetPos - vShootBodyPos).normalized;


        int layerMask = 1 << LayerMask.NameToLayer("GAME_RAY");
        float radius = 0.05f;

        RaycastHit2D hit2d = Physics2D.CircleCast(vShootBodyPos, radius, Dir , Mathf.Infinity , layerMask);

        if (hit2d)
        {
            LineReverse.SetActive(true);
            //HitMark.transform.position = hit2d.point;
            LineReverse.transform.position = hit2d.point;
            LineReverse.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180.0f - _fShootAngle));


            //Debug.Log(_fShootAngle);
        }

    }

}

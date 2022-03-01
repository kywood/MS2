using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskArea : MonoBehaviour
{
    // Start is called before the first frame update

    //  For case 1 I use Camera.main.ScreenToWorldPoint(Input.mousePosition) and it seems to work okay for my purposes.
    //  For case 2 I use RectTransformUtility.WorldToScreenPoint(Camera.main, obj.transform.position);
    //  For case 3 I use Camera.main.ScreenToWorldPoint(rectTransform.transform.position) and it seems to give good results.

    public GameObject canvas;

    public void AdJustMaskArea()
    {

        Walls walls = GameManager.Instance.Walls.GetComponent<Walls>();

        GameObject worldObject = walls.WT;

        Vector3 wposT = new Vector3(walls.WT.transform.position.x,
            walls.WT.transform.position.y - walls.WT.GetComponent<BoxCollider2D>().size.y / 2.0f, 0);
        Vector3 canvasPosT = Util.WorldToCanavsPosition(Camera.main, wposT, canvas);

        Vector3 wposB = new Vector3(walls.WB.transform.position.x,
            walls.WB.transform.position.y + walls.WB.GetComponent<BoxCollider2D>().size.y / 2.0f, 0);
        Vector3 canvasPosB = Util.WorldToCanavsPosition(Camera.main, wposB, canvas);

        Vector3 wposL = new Vector3(walls.WL.transform.position.x + walls.WL.GetComponent<BoxCollider2D>().size.x / 2.0f,
            walls.WL.transform.position.y, 0);
        Vector3 canvasPosL = Util.WorldToCanavsPosition(Camera.main, wposL, canvas);

        Vector3 wposR = new Vector3(walls.WR.transform.position.x - walls.WR.GetComponent<BoxCollider2D>().size.x / 2.0f,
            walls.WR.transform.position.y, 0);
        Vector3 canvasPosR = Util.WorldToCanavsPosition(Camera.main, wposR, canvas);


        Debug.Log($" canvasPosT : {canvasPosT.ToString()} , canvasPosB:{canvasPosB.ToString()}");
        Debug.Log($" canvasPosL : {canvasPosL.ToString()} , canvasPosR:{canvasPosR.ToString()}");

        RectTransform rt = GetComponent<RectTransform>();


        float width = Mathf.Abs(canvasPosR.x - canvasPosL.x);
        float height = Mathf.Abs(canvasPosT.y - canvasPosB.y);
        float posY = canvasPosT.y - (height / 2f);

        Debug.Log($" width : {width} , height:{height}");

        rt.position = new Vector3(0, posY, 0);
        rt.sizeDelta = new Vector2(width, height);

        transform.localPosition = rt.position;
    }


}

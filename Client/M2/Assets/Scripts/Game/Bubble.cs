using RotSlot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ConstData;
using static Defines;

public class Bubble : MonoBehaviour
{

    public void Start()
    {
        OnStart();
    }

    protected virtual void OnStart()
    {

        MyPlayer player = GameManager.Instance.MyPlayer.GetComponent<MyPlayer>();


        transform.localScale = new Vector3(player.Scale,
            player.Scale,
            1);
    }

    public void SetVisible(bool value)
    {
        gameObject.SetActive(value);
    }


}

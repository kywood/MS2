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
        transform.localScale = new Vector3(GameManager.Instance.GetBubbleManager().Scale,
            GameManager.Instance.GetBubbleManager().Scale,
            1);
    }

    public void SetVisible(bool value)
    {
        gameObject.SetActive(value);
    }


}

using RotSlot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ConstData;

public class BubbleManager : MonoBehaviour
{
    public Player Player;


    private void Awake()
    {
        OnAwake();
    }

    protected virtual void OnAwake(){ }


    private void Start()
    {
        OnStart();

        
    }

    protected virtual void OnStart() { 
    }

    

    public virtual void SetVisible(bool visible)
    {
        gameObject.SetActive(visible);
    }
    public virtual Sprite GetSprite(E_BUBBLE_TYPE bubble_type)
    {
        return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Next : MonoBehaviour
{
    public  GameObject[] nextBubble;

    private MyPlayer myPlayer;
    private BubbleManager bubbleManager;


    private void Start()
    {
        myPlayer = GameManager.Instance.MyPlayer.GetComponent<MyPlayer>();
        bubbleManager = myPlayer.BubbleManager.GetComponent<BubbleManager>();
    }

    //public void Update()
    //{
    //    UpdateNext();
    //}

    public void SetVisible(bool visible)
    {
        for (int i = 0; i < nextBubble.Length; i++)
        {
            nextBubble[i].SetActive(visible);
        }
    }

    public void UpdateNext()
    {

        for( int i = 0; i <  nextBubble.Length; i++ )
        {
            nextBubble[i].GetComponent<SpriteRenderer>().sprite =
            bubbleManager.GetSprite(bubbleManager.NextPeek(i));
        }

        //nextBubble[0].GetComponent<SpriteRenderer>().sprite =
        //    bubbleManager.GetSprite(bubbleManager.NextPeek());

        //nextBubble[0].GetComponent<SpriteRenderer>().sprite =
        //    bubbleManager.GetSprite(bubbleManager.NextPeek());
    }
   
}

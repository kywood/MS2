using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Next : MonoBehaviour
{
    public  GameObject[] nextBubble;

    public Player myPlayer;
    public BubbleManager bubbleManager;


    private void Start()
    {
       // myPlayer = PlayerManager.Instance.GetPlayer(PlayerManager.E_PLAYER_TYPE.MY_PLAYER);

        
       // bubbleManager = myPlayer.BubbleManager.GetComponent<BubbleManager>();
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
            bubbleManager.GetSprite(((ShootBubbleManager)bubbleManager).NextPeek(i));
        }

        //nextBubble[0].GetComponent<SpriteRenderer>().sprite =
        //    bubbleManager.GetSprite(bubbleManager.NextPeek());

        //nextBubble[0].GetComponent<SpriteRenderer>().sprite =
        //    bubbleManager.GetSprite(bubbleManager.NextPeek());
    }
   
}

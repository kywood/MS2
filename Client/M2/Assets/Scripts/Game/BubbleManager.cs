using RotSlot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ConstData;

public class BubbleManager : MonoBehaviour
{
    public GameObject shootBubble;


    Queue<E_BUBBLE_TYPE> _BubbleQueue = new Queue<E_BUBBLE_TYPE>();


    public Queue<E_BUBBLE_TYPE> BubbleQueue
    {
        get { return _BubbleQueue; }
    }

    Dictionary<E_BUBBLE_TYPE, Sprite> mBubbleSprite = new Dictionary<E_BUBBLE_TYPE, Sprite>();

    private void Awake()
    {
        

        foreach ( E_BUBBLE_TYPE bubble_type in ConstData.GetBubblePropertys().Keys)
        {
            cBubbleProperty bpro = ConstData.GetBubbleProperty(bubble_type);
            mBubbleSprite.Add(bubble_type, Resources.Load<Sprite>(bpro.mImgPath));
        }
        SetVisible(false);
    }

    private void Start()
    {
        //GameManager.Instance.GetBubbleManager

        for( int i = 0; i < 5; i++ )
        {
            _BubbleQueue.Enqueue(ConstData.GetNextBubbleType());
        }
    }

    public E_BUBBLE_TYPE NextPop()
    {
        _BubbleQueue.Enqueue(ConstData.GetNextBubbleType());
        return _BubbleQueue.Dequeue();
    }
    public E_BUBBLE_TYPE NextPeek()
    {
        return _BubbleQueue.Peek();
    }

    public E_BUBBLE_TYPE NextPeek(int index)
    {
        return _BubbleQueue.ToArray()[index];
    }

    public void SetShootBodyYPos(float y)
    {
        shootBubble.transform.position = new Vector3(
            shootBubble.transform.position.x,
            y,
            shootBubble.transform.position.z
            );
    }

    //public void Start()
    //{
    //    shootBubble.transform.position = new Vector3(
    //        shootBubble.transform.position.x,
    //        shootBubble.transform.position.y,
    //        shootBubble.transform.position.z
    //        );
    //}

    public Sprite GetSprite(E_BUBBLE_TYPE bubble_type )
    {
        return mBubbleSprite[bubble_type];
    }


    public void SetVisible( bool visible )
    {
        GetBubble().SetVisible(visible);
    }

    public ShootBubble GetBubble()
    {
        return shootBubble.GetComponent<ShootBubble>();
    }

}

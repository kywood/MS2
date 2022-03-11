using Google.Protobuf.Collections;
using RotSlot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ConstData;

public class ShootBubbleManager : BubbleManager
{
    public GameObject shootBubble;

    Queue<E_BUBBLE_TYPE> _BubbleQueue = new Queue<E_BUBBLE_TYPE>();

    public Queue<E_BUBBLE_TYPE> BubbleQueue
    {
        get { return _BubbleQueue; }
    }

    Dictionary<E_BUBBLE_TYPE, Sprite> mBubbleSprite = new Dictionary<E_BUBBLE_TYPE, Sprite>();

    protected override void OnAwake()
    {
        foreach (E_BUBBLE_TYPE bubble_type in ConstData.GetBubblePropertys().Keys)
        {
            cBubbleProperty bpro = ConstData.GetBubbleProperty(bubble_type);
            mBubbleSprite.Add(bubble_type, Resources.Load<Sprite>(bpro.mImgPath));
        }
        SetVisible(false);
    }

    public void EnqueueNextBubble(RepeatedField<int> bubbleTypes)
    {
        foreach( int bt in bubbleTypes)
        {
            _BubbleQueue.Enqueue((E_BUBBLE_TYPE)bt);
        }        
    }


    public void EnqueueNextBubble(E_BUBBLE_TYPE bubbleType)
    {
        _BubbleQueue.Enqueue(bubbleType);
    }

    public E_BUBBLE_TYPE NextPop()
    {
        //_BubbleQueue.Enqueue(ConstData.GetNextBubbleType());
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

    public override Sprite GetSprite(E_BUBBLE_TYPE bubble_type)
    {
        return mBubbleSprite[bubble_type];
    }

    public float GetRadius()
    {
        return shootBubble.GetComponent<CircleCollider2D>().radius * shootBubble.transform.localScale.x;
    }


    public override void SetVisible(bool visible)
    {
        GetBubble().SetVisible(visible);
    }

    public ShootBubble GetBubble()
    {
        return shootBubble.GetComponent<ShootBubble>();
    }
}

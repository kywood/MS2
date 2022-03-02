using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Pick;
    public GameObject BubbleManager;
    public GameObject Walls;
    public GameObject WallMaskArea;

    public GameObject RotSlot;
    public GameObject BG;


    public float BUBBLE_DIAMETER = 0.40f;
    //public const float G_BUBBLE_DIAMETER = 0.6f;
    public float SLOT_RADIUS_GAP = 0.03f;


    float _diameter;
    float _scale;
    public float Diameter { get { return _diameter; } }
    public float Scale { get { return _scale; } }


    private void Awake()
    {
        OnAwake();
    }

    private void Start()
    {
        SpriteRenderer sp = GameManager.Instance.BallSample.GetComponent<SpriteRenderer>();

        _scale = Defines.G_BUBBLE_DIAMETER / sp.bounds.size.x;
        _diameter = Defines.G_BUBBLE_DIAMETER - 0.015f;// * G_BUBBLE_SCALE;

        OnStart();
    }

    protected virtual void OnAwake()
    {

    }
    protected virtual void OnStart()
    {

    }
    public GameObject GetRotSlot()
    {
        return RotSlot;
    }

    public BubbleManager GetBubbleManager()
    {
        return BubbleManager.GetComponent<BubbleManager>();
    }

}

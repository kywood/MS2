using Google.Protobuf;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerManager;
using static PlayerStateManager;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update

   
    public GameObject Pick;
    public GameObject BubbleManager;
    public GameObject Walls;
    public GameObject WallMaskArea;
    public GameObject BubblePool;

    public GameObject RotSlot;
    public GameObject BG;

    public GameObject DeadLine;

    public float LocalScale = 1.0f;

    public float BUBBLE_DIAMETER = 0.4f;
    //public const float G_BUBBLE_DIAMETER = 0.6f;
    public float SLOT_RADIUS_GAP = 0.03f;

    

    


    float _diameter;
    float _scale;
    public float Diameter { get { return _diameter; } }
    public float Scale { get { return _scale; } }

    private E_PLAYER_TYPE _player_type;

    public E_PLAYER_TYPE PlayerType { get {return _player_type; }  set { _player_type = value; } }

    PlayerStateManager _stateManager;

    public PlayerStateManager StateManager { get { return _stateManager; } }

    private void Awake()
    {
        _stateManager = new PlayerStateManager( this );

        transform.localScale = new Vector3(LocalScale, LocalScale, 1f);
        OnAwake();
    }

    private void Start()
    {
        SpriteRenderer sp = GameManager.Instance.BallSample.GetComponent<SpriteRenderer>();

        _scale = BUBBLE_DIAMETER / sp.bounds.size.x;
        _diameter = BUBBLE_DIAMETER - 0.015f;// * G_BUBBLE_SCALE;

        OnStart();
    }

    private void Update()
    {
        OnUpdate();
    }

    protected virtual void OnAwake()
    {

    }
    protected virtual void OnStart()
    {

    }


    protected virtual void OnUpdate()
    {
        StateManager.OnUpdate();

        //while(_queue.Count != 0)
        //{
        //    _queue.Dequeue().Invoke();
        //}
    }


    public GameObject GetRotSlot()
    {
        return RotSlot;
    }

    public void SetPlayerState(E_PLAYER_STATE player_state , Action<State<StateManager>> act = null)
    {
        StateManager.SetState((int)player_state , act );
    }

    public BubbleManager GetBubbleManager()
    {
        return BubbleManager.GetComponent<BubbleManager>();
    }

}

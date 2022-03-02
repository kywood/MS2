using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    GameStateManager _StateManager = new GameStateManager();

    //public GameObject BubbleManager;


    public GameObject MyPlayer;
    public GameObject BallSample;

    //float ShootForce = 10.0f;
    //[HideInInspector]
    //public GameStateManager GameStateManaget = new GameStateManager();

    

    public MyPlayer GetMyPlayer()
    {
        return MyPlayer.GetComponent<MyPlayer>();
    }


    //public BubbleManager GetBubbleManager()
    //{
    //    return BubbleManager.GetComponent<BubbleManager>();
    //}

    public GameStateManager GetGameStateManager()
    {
        return _StateManager;
    }

    override protected void OnStart()
    {
        _StateManager.SetGameState(GameStateManager.E_GAME_STATE.READY);
    }

    override protected void OnUpdate()
    {
        base.OnUpdate();

        _StateManager.OnUpdate();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePopup : Windows
{
    public enum eWindows
    {
        None = -1,
        GameOver,
        GameResult,
        MAX
    }

    private static GamePopup _instance = null;

    public static GamePopup Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("InGameUI == null");
            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
    }
}

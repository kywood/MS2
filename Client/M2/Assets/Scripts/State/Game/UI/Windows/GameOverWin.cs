using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverWin : MonoBehaviour
{
    public void Onclick_RePlay()
    {
        Application.LoadLevel(Defines.GetScenesName(Defines.E_SCENES.MENU));

    }


}

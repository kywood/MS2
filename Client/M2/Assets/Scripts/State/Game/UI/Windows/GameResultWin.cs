using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class GameResultWin : MonoBehaviour
{
    public GameObject ResurtComments;

    private void Start()
    {
        
    }

    public void UpdateRankText()
    {
        Text txt = ResurtComments.GetComponent<Text>();

        StringBuilder sb = new StringBuilder();

        int loopCount = 0;
        PlayerManager.Instance.PlayerRank.ForEach((pr) => {

            if (loopCount++ != 0)
            {
                sb.Append(System.Environment.NewLine);
            }

            sb.Append($"{pr.Rank}. {pr.PlayerId}");
        });

        txt.text = sb.ToString();
    }


    //public void SetActive(bool value)
    //{
    //    gameObject.active = value;

    //    Debug.Log("AAAAAAAAAA");

    //}
    

    //public void 

    public void Onclick_Lobby()
    {
        Application.LoadLevel(Defines.GetScenesName(Defines.E_SCENES.LOBBY));

    }


}

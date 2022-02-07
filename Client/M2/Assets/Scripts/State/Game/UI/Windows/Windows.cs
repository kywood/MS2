using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Windows : MonoBehaviour
{
    public List<GameObject> WindowList = new List<GameObject>();


    public GameObject GetWindow(int windowType)
    {
        return WindowList[(int)windowType];
    }
    public void ActiveAll(bool bActive)
    {
        for (int i = 0; i < WindowList.Count; i++)
        {
            WindowList[i].SetActive(bActive);
        }
    }

    //public bool ActiveOK(bool bActive, string text)
    //{
    //    Active((int)CUIManager.eWindow.OK, bActive);
    //    GetWindow((int)CUIManager.eWindow.OK).GetComponent<OKWindow>().SetText(text);
    //    return true;
    //}

    //public bool ActiveReOK(bool bActive, string text)
    //{
    //    Active((int)CUIManager.eWindow.ReOK, bActive);
    //    GetWindow((int)CUIManager.eWindow.ReOK).GetComponent<OKWindow>().SetText(text);
    //    return true;
    //}

    public bool Active(int windowType, bool bActive)
    {
        if ((int)windowType < 0 || (int)windowType >= WindowList.Count)
        {
            return false;
        }

        //GameObject obj = WindowList[(int)windowType];
        //uTweener[] tween = obj.GetComponents<uTweener>();

        //if (tween.Length > 0)
        //{
        //    if (bActive == true)
        //    {
        //        for (int i = 0; i < tween.Length; i++)
        //        {
        //            tween[i].ResetToBeginning();
        //            tween[i].enabled = true;
        //        }
        //    }
        //}
        WindowList[(int)windowType].SetActive(bActive);
        return true;
    }

    public bool IsActive(int windowType)
    {
        if ((int)windowType < 0 || (int)windowType >= WindowList.Count)
        {
            return false;
        }

        return WindowList[(int)windowType].activeSelf;
    }


    public GameObject GetWindows(int windowType)
    {
        if ((int)windowType < 0 || (int)windowType >= WindowList.Count)
        {
            return null;
        }

        return WindowList[(int)windowType];
    }


}

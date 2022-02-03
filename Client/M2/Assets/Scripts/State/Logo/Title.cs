using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {
        StartCoroutine("RunFadeOut");
    }


    void Update()
    {
    }

    IEnumerator RunFadeOut()
    {
        yield return new WaitForSeconds(1.2f);

        Application.LoadLevel(Defines.GetScenesName(Defines.E_SCENES.MENU) );

        yield return null;
    }
}

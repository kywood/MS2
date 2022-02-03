using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    // Start is called before the first frame update

    RawImage texture;

    void Start()
    {
        texture = GetComponent<RawImage>();
        texture.texture.wrapMode = TextureWrapMode.Repeat;
    }

    // Update is called once per frame
    void Update()
    {
        Rect currentUV = texture.uvRect;
        currentUV.x -= Time.deltaTime * 0.2f;

        if (currentUV.x <= -1f || currentUV.x >= 1)
        {
            currentUV.x = 0f;
        }

        texture.uvRect = currentUV;
    }
}

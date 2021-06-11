using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{

    public float fadeTime;

    private Image blackScreen;

    // Start is called before the first frame update
    void Start()
    {
        blackScreen = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        //목표치, 목표에 도달할 때 까지의 시간, 시간을 무시할지
        blackScreen.CrossFadeAlpha(0f, fadeTime, false);

        if(blackScreen.color.a == 0) //알파가 0이라면
        {
            gameObject.SetActive(false);
        }
    }
}

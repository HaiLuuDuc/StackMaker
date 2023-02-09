using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadingControl : MonoBehaviour
{
    RectTransform rectTransform;
    float progressValue = 0;
    float targetWidth;
    void Start()
    {
        rectTransform = this.GetComponent<RectTransform>();
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0);
        targetWidth = 780;
    }

    void Update()
    {
        if (progressValue > targetWidth)
        {
            GoToIntroScene();
        }
        else
        {
            progressValue += 5f;
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, progressValue);
            rectTransform.anchoredPosition += new Vector2((float)2.5, 0);
        }
    }

    private void GoToIntroScene()
    {
        SceneManager.LoadScene("IntroScene");
    }
}

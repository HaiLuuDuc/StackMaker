using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeTrigger : MonoBehaviour
{

    [SerializeField] private Color colorAfter;
    [SerializeField] GameObject wallColor;
    [SerializeField] GameObject bridge;


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "container")
        {
            ChangeColor();
            other.gameObject.GetComponent<Container>().onBridge = true;
        }
    }

    private void ChangeColor()
    { 
        foreach (Transform child in bridge.transform)
        {
            if (child.name == "WallColor")
            { 
                child.GetComponent<ColorWall>().ChangeColor(colorAfter);
            }
        }
    }
}

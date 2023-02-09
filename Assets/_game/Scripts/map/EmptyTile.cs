using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EmptyTile : MonoBehaviour
{
    string color = "red";

    void ChangeColor()
    {
        this.gameObject.GetComponent<MeshRenderer>().material.color = Color.yellow;
        color = "yellow";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "container")
        {
       /*     other.gameObject.GetComponent<Container>().onBridge = true;*/
            if (color == "red")
            {
                ChangeColor();
                other.gameObject.GetComponent<Container>().RemoveStack();

            }

        }
    }
}

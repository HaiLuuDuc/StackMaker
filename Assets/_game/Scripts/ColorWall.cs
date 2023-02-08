using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorWall : MonoBehaviour
{
    [SerializeField] private  Color colorBefore;
    private Color color;
    private void Start()
    {
        color = colorBefore;
    }
    void Update()
    {
        GetComponent<MeshRenderer>().material.color = color;
    }
    public void ChangeColor(Color newColor)
    {
        color = newColor;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class IntroCameraControl : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] GameObject target;

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(speed, 0, 0);
        transform.LookAt(target.transform);
    }
}

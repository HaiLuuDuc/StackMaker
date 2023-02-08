using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject container;
    public Vector3 offset;
    private Vector3 targetSpot;
    private void Start()
    {
        
    }
    private void LateUpdate()
    {
        transform.position = container.transform.position + offset;

        if (container.gameObject.GetComponent<Container>().onBridge)
        {
            offset = Vector3.Lerp(offset, offset + new Vector3(1, 0, 0), Time.deltaTime * 0.8f);
            transform.LookAt(container.transform);
        }
    }
}

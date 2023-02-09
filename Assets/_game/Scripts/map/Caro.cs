using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caro : MonoBehaviour
{
    [SerializeField] GameObject caro_VFX;

    void DestroyVFX()
    {
        Destroy(caro_VFX.gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "container")
        {
            Instantiate(caro_VFX, transform.position, Quaternion.identity);
            Invoke(nameof(DestroyVFX), 2f);
        }
    }
}

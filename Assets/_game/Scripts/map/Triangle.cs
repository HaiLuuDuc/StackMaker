using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Triangle : MonoBehaviour
{
    [SerializeField] Animator anim;
    private string currentAnim = "idle";
    private float timer;
    private float bounceTime = 1f;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > bounceTime)
        {
            ChangeAnim("idle");
        }
    }
    void ChangeAnim(string newAnim)
    {
        if (currentAnim != newAnim)
        {
            anim.SetTrigger(newAnim);
            anim.ResetTrigger(currentAnim);
            currentAnim = newAnim;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "container")
        {
            ChangeAnim("bouncy");
            timer = 0;
        }
    }
}

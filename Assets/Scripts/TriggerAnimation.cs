using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private bool once = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && once == false)
        {
            animator.SetTrigger("trigger");
            once = true;
        }
    }
}

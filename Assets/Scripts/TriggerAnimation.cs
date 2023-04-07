using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private bool shouldDie = true;

    [Header("If shouldDie is true")]
    [SerializeField] private float lifetime;

    [Header("If shouldDie is false")]
    [SerializeField] private float triggerCD;
    private bool once = false;
    private float cdAllow;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && once == false)
        {
            if (shouldDie)
            {
                animator.SetTrigger("trigger");
                once = true;
                Destroy(gameObject, lifetime);

            }
            else if (Time.time > cdAllow)
            {
                animator.SetTrigger("trigger");
                cdAllow = Time.time + triggerCD;
            }
        }
    }
}

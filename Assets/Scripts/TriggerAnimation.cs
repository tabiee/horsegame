using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private bool shouldDie = true;

    [Header("If shouldDie is true")]
    [SerializeField] private float lifetime;

    [Header("If shouldDie is false", order = 1)]
    [Space(-10, order = 2)]
    [Header("Chance at 0 is 1 in 10 | 10%", order = 3)]
    [SerializeField] private float triggerCD;
    [SerializeField] private int chance = 10;

    private bool once = false;
    private float cdAllow;

    private int random;
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
                random = Random.Range(chance, 11);

                if (random == 10)
                {
                    animator.SetTrigger("trigger");
                    cdAllow = Time.time + triggerCD;
                }
            }
        }
    }
}

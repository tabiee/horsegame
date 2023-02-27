using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectLifetime : MonoBehaviour
{
    [SerializeField] private int lifetime = 1;
    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}

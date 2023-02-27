using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] private int health = 8;
    [SerializeField] private MovementAlt moveAlt;
    public float speedReduction = 0.25f;
    void Update()
    {
        if (health <= 0)
        {
            //play some effect and do death
            Debug.Log("You have died!");
            SceneManager.LoadScene("GameOver");
        }
    }
    public void KelpieAttack()
    {
        health = health - 4;
        Debug.Log("Kelpie nibble!");
    }
    public void LimsectAttack()
    {
        health = health - 2;
        moveAlt.speedModifier = moveAlt.speedModifier - speedReduction;
    }
}

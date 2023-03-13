using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    public int health = 8;
    public float speedReduction = 0.25f;
    public int kelpieDamage = 4;
    public int limsectDamage = 2;
    [SerializeField] private MovementAlt moveAlt;
    void Update()
    {
        if (health <= 0)
        {
            //play some effect and do death
            //Debug.Log("You have died!");
            //SceneManager.LoadScene("GameOver");
        }
    }
    public void KelpieAttack()
    {
        health = health - kelpieDamage;
        Debug.Log("Kelpie nibble!");
    }
    public void LimsectAttack()
    {
        Debug.Log("Limsect snatch!");
        health = health - limsectDamage;
        moveAlt.speedModifier = moveAlt.speedModifier - speedReduction;
    }
}

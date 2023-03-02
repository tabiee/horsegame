using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyInLight : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private float kelpieSpeed = 0.45f;
    [SerializeField] private float kelpieSideSpeed = 0.65f;
    [SerializeField] private float limsectSpeed = 0.9f;
    [SerializeField] private float limsectSideSpeed = 1.1f;
    [SerializeField] private GameObject playerObject;
    [SerializeField] private LightCheck lightCheck;
    [SerializeField] private PlayerLife playerLife;
    public bool inLight = false;
    public bool running = false;

    [Header("Raycast")]
    public RaycastHit2D hitData;
    public RaycastHit2D hitData2;
    public LayerMask playerLayer;
    public LayerMask enemyLayer;
    public float distance = 25f;

    [Header("Kelpie")]
    [SerializeField] private float lightCD = 0.2f;
    private float lightAllow = 0f;
    [SerializeField] private float avoidCD = 0.2f;
    private float avoidAllow = 0f;
    [SerializeField] private float attackCD = 3f;
    private float attackAllow = 0f;
    private int random;
    [SerializeField] private GameObject splashParticles;

    [Header("Limsect")]
    [SerializeField] private int enemyHealth = 300;
    [SerializeField] private float attachDistance = 0.15f;
    [SerializeField] private GameObject deathParticles;
    private MovementAlt moveAlt;
    private Rigidbody2D enemyRB;
    private AIPath aiPath;


    private void Awake()
    {
        playerObject = GameObject.Find("Player");
        playerLife = playerObject.GetComponent<PlayerLife>();
        moveAlt = playerObject.GetComponent<MovementAlt>();
        lightCheck = playerObject.GetComponentInChildren<LightCheck>();
        aiPath = GetComponent<AIPath>();
        enemyRB = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        //Debug.Log(gameObject.name + "'s inLight is: " + inLight);
        //Debug.Log(gameObject.name + "'s ShadowCheck is: " + ShadowCheck());
        //Debug.Log(gameObject.name + "'s lightCheck.toggle is: " + lightCheck.toggle);
        //Debug.Log(gameObject.name + "'s lightAllow is: " + (Time.time > lightAllow));

        ShadowCheck();
        //is it in light and not behind anything?

        if (gameObject.tag == "Kelpie")
        {
            Kelpie();
        }
        //kelpie behaviour
        if (gameObject.tag == "Limsect")
        {
            Limsect();
        }
        //shadows/limbs behaviour

        //if light is off, mark as false
        if (lightCheck.toggle == false)
        {
            inLight = false;
        }
    }

    public IEnumerator KelpieLoop()
    {
        //this runs when the coroutine starts
        //rising action
        //Debug.Log("KelpieLoop initial command ran!");

        if (Time.time < lightAllow)
        {
            yield return new WaitUntil(() => Time.time > lightAllow);
        }
        if (inLight == true && ShadowCheck() == true && lightCheck.toggle == true && Time.time > lightAllow)
        {
            Instantiate(splashParticles, transform.position, Quaternion.identity);
        }
        //this runs when the condition is true
        //action over time
        while (inLight == true && ShadowCheck() == true && lightCheck.toggle == true && Time.time > lightAllow)
        {
            //Debug.Log("KelpieLoop while is running!");
            Avoidance(kelpieSpeed, kelpieSideSpeed);
            //avoid light

            this.gameObject.GetComponent<AIPath>().enabled = false;
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            //this.gameObject.transform.Find("splash").gameObject.GetComponent<SpriteRenderer>().enabled = true;
            yield return null;
        }

        //this runs after the condition is false
        //falling action
        //Debug.Log("KelpieLoop has ended!");
        Instantiate(splashParticles, transform.position, Quaternion.identity);
        this.gameObject.GetComponent<AIPath>().enabled = true;
        this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        //this.gameObject.transform.Find("splash").gameObject.GetComponent<SpriteRenderer>().enabled = false;
        lightAllow = Time.time + lightCD;
        yield break;

    }
    public IEnumerator LimsectLoop()
    {
        //Debug.Log("LimesectLoop initial command ran!");
        if (Time.time < lightAllow)
        {
            //Debug.Log("LimesectLoop has detected lightAllow as false!");
            yield return new WaitUntil(() => Time.time > lightAllow);
        }
        while (inLight == true && ShadowCheck() == true && lightCheck.toggle == true && Time.time > lightAllow)
        {
            //Debug.Log("LimesectLoop while is running!");
            enemyHealth--;
            yield return null;
        }
        //Debug.Log("LimesectLoop has ended!");
        lightAllow = Time.time + lightCD;
        yield break;
    }
    void Kelpie()
    {
        //if reached player

        if (aiPath.remainingDistance < attachDistance && Time.time > attackAllow)
        {
            //deal dmg to player and do some fancy shit idk
            attackAllow = Time.time + attackCD;
            playerLife.KelpieAttack();
        }
    }
    void Limsect()
    {
        //if reached player
        bool once = false;
        if (aiPath.remainingDistance < attachDistance && once == false)
        {
            once = true;
            gameObject.transform.SetParent(playerObject.transform, true);
            this.gameObject.GetComponent<AIPath>().enabled = false;
            enemyRB.bodyType = RigidbodyType2D.Kinematic;
            playerLife.LimsectAttack();
        }
        //killed enemy
        if (enemyHealth < 0)
        {
            //play effect and kill the object
            Instantiate(deathParticles, transform.position, Quaternion.identity);

            Destroy(gameObject);
            if (enemyRB.bodyType == RigidbodyType2D.Kinematic)
            {
                moveAlt.speedModifier = moveAlt.speedModifier + playerLife.speedReduction;
                playerLife.health = playerLife.health + playerLife.limsectDamage;
            }
        }

        CrowdCheck();
        if (CrowdCheck() == true)
        {
            Avoidance(limsectSpeed, limsectSideSpeed);
        }
    }
    void Avoidance(float scitterSpeed, float sideSpeed)
    {
        //randomize avoidance
        if (Time.time > avoidAllow)
        {
            avoidAllow = avoidCD + Time.time;
            random = Random.Range(0, 2);
        }

        if (Time.time > lightAllow)
        {
            var playerDir = this.gameObject.transform.localPosition - playerObject.transform.position;

            float angleRad = Mathf.Atan2(playerObject.transform.position.y - transform.position.y, playerObject.transform.position.x - transform.position.x);
            // Get Angle in Degrees
            float angleDeg = 180 / Mathf.PI * angleRad;
            // Rotate Object
            transform.rotation = Quaternion.Euler(0, 0, angleDeg - 90);

            switch (random)
            {
                case 0:
                    //move right (x) and forward from local space when in light
                    this.gameObject.transform.localPosition += (transform.right * sideSpeed + -playerDir * scitterSpeed) * Time.deltaTime;
                    break;
                case 1:
                    this.gameObject.transform.localPosition += (-transform.right * sideSpeed + -playerDir * scitterSpeed) * Time.deltaTime;
                    break;
                case 2:
                    this.gameObject.transform.localPosition += (transform.up * sideSpeed + -playerDir * scitterSpeed) * Time.deltaTime;
                    break;
                case 3:
                    this.gameObject.transform.localPosition += (-transform.up * sideSpeed + -playerDir * scitterSpeed) * Time.deltaTime;
                    break;
            }
        }
    }
    private bool CrowdCheck()
    {
        if (hitData2.collider != null)
        {
            //Debug.Log(gameObject.name + "'s Ray hit: " + hitData2.collider.gameObject.name);
        }

        Ray2D ray = new Ray2D(this.transform.position, playerObject.gameObject.transform.position - this.transform.position);

        //show the ray
        Debug.DrawRay(ray.origin, ray.direction, Color.magenta, 0.1f, false);

        //shoot a ray from enemy position to player position, only hit the specified layer
        hitData2 = Physics2D.Raycast(ray.origin, ray.direction, 0.4f, enemyLayer);


        //if it hit something, run stuff
        if (hitData2.collider != null)
        {
            //yes it's behind an enemy
            return true;
        }
        else
        {
            //no it's not behind an enemy
            return false;
        }
    }
    private bool ShadowCheck()
    {
        if (hitData.collider != null)
        {
            //Debug.Log(gameObject.name + "'s Ray hit: " + hitData.collider.gameObject.name);
        }

        Ray2D ray = new Ray2D(this.transform.position, playerObject.gameObject.transform.position - this.transform.position);

        //show the ray
        Debug.DrawRay(ray.origin, ray.direction, Color.magenta, 0.1f, false);

        //shoot a ray from enemy position to player position, only hit the specified layer
        hitData = Physics2D.Raycast(ray.origin, ray.direction, distance, ~playerLayer);


        //if it hit something, run stuff
        if (hitData.collider != null && hitData.collider.name == "Player")
        {
            //yes it's not behind anything, ray reached player with no obstacles
            return true;
        }
        else
        {
            //no it's behind something, ray failed to reach the player
            return false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.Rendering.Universal;

public class EnemyInLight : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private float kelpieSpeed = 0.45f;
    [SerializeField] private float kelpieSideSpeed = 0.65f;
    [SerializeField] private float limsectSpeed = 0.9f;
    [SerializeField] private float limsectSideSpeed = 1.1f;
    [SerializeField] private int enemyHealth = 300;
    public bool inLight = false;

    private GameObject playerObject;
    private LightCheck lightCheck;
    private PlayerLife playerLife;
    private int maxHealth;
    private AIPath aiPath;
    private AIDestinationSetter aiTarget;
    private SpriteRenderer spriteRenderer;
    private ShadowCaster2D shadowCaster;
    private Animator animator;

    [Header("Raycast")]
    public RaycastHit2D hitData;
    public RaycastHit2D hitData2;
    public LayerMask playerLayer;
    public LayerMask enemyLayer;
    public float distance = 25f;

    [Header("Kelpie")]
    [SerializeField] private GameObject ripple;
    [SerializeField] private Animator animatorRipple;
    [SerializeField] private float resurfaceTimer = 1f;
    [SerializeField] private float avoidCD = 0.2f;
    private float avoidAllow = 0f;
    [SerializeField] private float attackCD = 3f;
    private float attackAllow = 0f;
    private int random;
    [SerializeField] private GameObject splashParticles;

    [SerializeField] private float internalSpeed = 1f, internalSlowed = 0.85f;
    private float internalModifier = 1f;

    [Header("Limsect")]
    [SerializeField] private float attachDistance = 0.15f;
    [SerializeField] private GameObject deathParticles;
    private MovementAlt moveAlt;
    private Rigidbody2D enemyRB;


    private void Awake()
    {
        playerObject = GameObject.Find("Player");
        playerLife = playerObject.GetComponent<PlayerLife>();
        moveAlt = playerObject.GetComponent<MovementAlt>();
        lightCheck = playerObject.GetComponentInChildren<LightCheck>();
        aiPath = GetComponent<AIPath>();
        aiTarget = GetComponent<AIDestinationSetter>();
        enemyRB = GetComponent<Rigidbody2D>();
        if (this.tag == "Kelpie")
        {
            animator = transform.parent.GetComponentInChildren<Animator>();
            shadowCaster = transform.parent.GetComponentInChildren<ShadowCaster2D>();
            spriteRenderer = transform.parent.Find("Sprite").GetComponent<SpriteRenderer>();
        }
        maxHealth = enemyHealth;
        aiPath.maxSpeed = internalSpeed;

        aiTarget.target = playerObject.transform;
    }
    private void FixedUpdate()
    {
    }
    private void Update()
    {
        //inLight = false;
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
        //limbsect behaviour

        //if light is off, mark as false
        if (lightCheck.toggle == false)
        {
            inLight = false;
        }

        if (this.tag == "Kelpie")
        {
            //keep the sprite in the right place
            spriteRenderer.transform.position = transform.position;
        }
        //calculate speed
        aiPath.maxSpeed = internalSpeed * internalModifier;
    }

    public IEnumerator KelpieLoop()
    {
        //this runs when the coroutine starts
        //rising action
        Debug.Log("KelpieLoop initial command ran!");
        bool once = false;

        //this runs when the condition is true
        //action over time
        while (inLight == true && ShadowCheck() == true && lightCheck.toggle == true)
        {
            if (enemyHealth > 0)
            {
                enemyHealth--;
            }
            else
            {
                //need this to run only once but it aint working
                if (once == false)
                {
                    once = true;
                    Debug.Log("Splash and ripple at Rising!");
                    Instantiate(splashParticles, transform.position, Quaternion.identity);
                    ripple.SetActive(true);
                    //animatorRipple.enabled = true;
                    //animatorRipple.StartPlayback();
                }

                //reduce speed by a %
                internalModifier = internalSlowed;

                //Debug.Log("KelpieLoop while is running!");
                Avoidance(kelpieSpeed, kelpieSideSpeed);
                //avoid light

                aiPath.enabled = false;
                spriteRenderer.enabled = false;
                shadowCaster.enabled = false;
            }
            yield return null;
        }

        //this runs after the condition is false
        //falling action
        Invoke("KelpieEnd", resurfaceTimer);
        yield break;

    }
    public void KelpieEnd()
    {
        Debug.Log("KelpieLoop has ended!");
        enemyHealth = maxHealth;
        internalModifier = 1f;

        ripple.SetActive(false);
        //animatorRipple.StopPlayback();
        //animatorRipple.enabled = false;
        Instantiate(splashParticles, transform.position, Quaternion.identity);
        aiPath.enabled = true;
        spriteRenderer.enabled = true;
        shadowCaster.enabled = true;
    }
    void Kelpie()
    {
        //if reached player

        if (aiPath.remainingDistance < attachDistance && Time.time > attackAllow)
        {
            //deal dmg to player and do some fancy shit idk
            attackAllow = Time.time + attackCD;
            playerLife.KelpieAttack();
            animator.SetTrigger("attack");
        }

        //flip in the direction of movement
        if (aiTarget.target != null)
        {
            spriteRenderer.flipX = (aiPath.desiredVelocity.x > 0.0f);
        }
    }
    public IEnumerator LimsectLoop()
    {
        //Debug.Log("LimesectLoop initial command ran!");

        while (inLight == true && ShadowCheck() == true && lightCheck.toggle == true)
        {
            //Debug.Log("LimesectLoop while is running!");
            enemyHealth--;
            yield return null;
        }
        //Debug.Log("LimesectLoop has ended!");
        yield break;
    }
    void Limsect()
    {
        //if reached player
        bool once = false;
        if (aiPath.remainingDistance < attachDistance && once == false)
        {
            once = true;
            gameObject.transform.SetParent(playerObject.transform, true);
            aiPath.enabled = false;
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
        if (hitData.collider != null && hitData.collider.name == "Player" && hitData.collider.isTrigger == false)
        {
            Debug.Log("yes it's not behind anything, ray reached player with no obstacles");

            return true;
        }
        else
        {
            Debug.Log("no it's behind something, ray failed to reach the player");
            return false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyInLight : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private float scitterSpeed = 0.65f;
    [SerializeField] private float sideSpeed = 0.85f;
    [SerializeField] private GameObject playerObject;
    [SerializeField] private LightCheck lightCheck;
    [SerializeField] private PlayerLife playerLife;
    public bool inLight = false;

    [Header("Raycast")]
    public RaycastHit2D hitData;
    public LayerMask playerLayer;
    public float distance = 25f;

    [Header("Kelpie")]
    [SerializeField] private float lightCD = 0.2f;
    private float lightAllow = 0f;
    [SerializeField] private float avoidCD = 0.2f;
    private float avoidAllow = 0f;
    [SerializeField] private float attackCD = 3f;
    private float attackAllow = 0f;
    private int random;

    [Header("Limsect")]
    [SerializeField] private int enemyHealth = 300;
    [SerializeField] private float attachDistance = 0.15f;
    [SerializeField] private ParticleSystem deathParticles;
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
    private void FixedUpdate()
    {
        inLight = false;
    }
    private void Update()
    {
        //Debug.Log(gameObject.name + "'s inLight is: " + inLight);
        //Debug.Log(gameObject.name + "'s ShadowCheck is: " + ShadowCheck());
        //Debug.Log(gameObject.name + "'s lightCheck.toggle is: " + lightCheck.toggle);

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

        //if light is not turned on, mark enemy as not being in light
        if (lightCheck.toggle == false)
        {
            inLight = false;
        }
    }
    void Kelpie()
    {
        //base lightcheck and behaviour
        if (inLight && ShadowCheck() == true && lightCheck.toggle == true && Time.time > lightAllow)
        {
            Debug.Log(gameObject.name + " is in the light!");
            Avoidance();
            //avoid light

            this.gameObject.GetComponent<AIPath>().enabled = false;
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            this.gameObject.transform.Find("splash").gameObject.GetComponent<SpriteRenderer>().enabled = true;

        }
        else if (hitData.collider != null && Time.time > lightAllow)
        {
            lightAllow = lightCD + Time.time;
            this.gameObject.GetComponent<AIPath>().enabled = true;
            this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            this.gameObject.transform.Find("splash").gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        //if reached player

        if (aiPath.remainingDistance < attachDistance && Time.time > attackAllow)
        {
            //deal dmg to player and do some fancy shit idk
            attackAllow = Time.time + attackCD;
            playerLife.KelpieAttack();
        }
    }
    void Avoidance()
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
                this.gameObject.transform.localPosition += (-transform.right * sideSpeed + -playerDir * scitterSpeed) * Time.deltaTime;
                break;
        }
    }
    void Limsect()
    {
        //base lightcheck and behaviour
        if (inLight && ShadowCheck() == true && lightCheck.toggle == true && Time.time > lightAllow)
        {
            Debug.Log(gameObject.name + " is in the light!");

            enemyHealth--;

        }
        else if (hitData.collider != null && Time.time > lightAllow)
        {
            lightAllow = lightCD + Time.time;

        }
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
            }
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

        //shoot a ray from player position to kelpie position, only hit the specified layer
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

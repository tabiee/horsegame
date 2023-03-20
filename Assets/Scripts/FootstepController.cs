using UnityEngine;

public class FootstepController : MonoBehaviour
{
    public AudioClip footstepSound; // a single footstep sound
    public float footstepDelay = 0.3f; // the delay between each footstep sound
    private float lastFootstepTime; // the time of the last footstep sound

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // check if the player is moving
        bool isMoving = Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f || Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f;

        // check if enough time has passed since the last footstep sound
        if (isMoving && Time.time - lastFootstepTime > footstepDelay)
        {
            // play the footstep sound
            audioSource.PlayOneShot(footstepSound);

            // set the time of the last footstep sound
            lastFootstepTime = Time.time;
        }
        else if (!isMoving)
        {
            // stop the footstep sound if the player has stopped moving
            audioSource.Stop();
        }
    }
}


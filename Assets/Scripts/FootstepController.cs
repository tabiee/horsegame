using UnityEngine;

public class FootstepController : MonoBehaviour
{
    public AudioClip footstepSound;
    public float footstepDelay = 0.3f;
    private float lastFootstepTime;

    private AudioSource audioSource;
    private bool isPlaying;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = footstepSound;
        audioSource.loop = true;
    }

    private void Update()
    {
        bool isMoving = Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f || Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f;

        if (isMoving && Time.time - lastFootstepTime > footstepDelay)
        {
            if (!isPlaying)
            {
                audioSource.Play();
                isPlaying = true;
            }

            lastFootstepTime = Time.time;
        }
        else if (!isMoving)
        {
            if (isPlaying)
            {
                audioSource.Stop();
                isPlaying = false;
            }
        }
    }
}


using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    public PlayerMovement PlayerMovement;

    [SerializeField] private float walkFootstepInterval = 0.7f;
    [SerializeField] private float runFootstepInterval = 0.4f;
    public AudioClip[] footstepSounds;

    private AudioSource audioSource;
    private float footstepInterval;
    private float nextFootstepTime;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        footstepInterval = walkFootstepInterval;
        nextFootstepTime = Time.time;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && PlayerMovement.isMoving)
            footstepInterval = runFootstepInterval;
        else
            footstepInterval = walkFootstepInterval;

        if (PlayerMovement.isMoving && Time.time >= nextFootstepTime)
        {
            PlayFootstepSound();
            nextFootstepTime = Time.time + footstepInterval;
        }
    }

    private void PlayFootstepSound()
    {
        if (footstepSounds.Length == 0) return;

        AudioClip randomFootstepSound = footstepSounds[Random.Range(0, footstepSounds.Length)];

        audioSource.clip = randomFootstepSound;
        audioSource.Play();
    }
}
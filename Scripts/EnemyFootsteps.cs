using UnityEngine;

public class EnemyFootsteps : MonoBehaviour
{
    [SerializeField] private float walkFootstepInterval = 0.7f;
    [SerializeField] private float runFootstepInterval = 0.15f;
    [SerializeField] private AudioClip[] footstepSounds;

    private AudioSource audioSource;
    private float footstepInterval;
    private float nextFootstepTime;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        footstepInterval = runFootstepInterval;
        nextFootstepTime = Time.time;
    }

    void Update()
    {
        if (gameObject.tag is "Monster")
        {
            if (GetComponent<EnemyController>().running)
                footstepInterval = runFootstepInterval;
            else
                footstepInterval = walkFootstepInterval;
        }
        if (gameObject.tag is "Bear")
        {
            if (GetComponent<BearScript>().isRunning)
            {
                GetComponent<AudioSource>().volume = 0.5f;
                footstepInterval = runFootstepInterval;
            }
            else
                GetComponent<AudioSource>().volume = 0f;
        }

        if (Time.time >= nextFootstepTime)
        {
            PlayFootstepSound();
            nextFootstepTime = Time.time + footstepInterval;
        }
    }

    private void PlayFootstepSound()
    {
        if (footstepSounds.Length == 0) return;

        AudioClip randomFootstepSound = footstepSounds[Random.Range(0, footstepSounds.Length)];

        audioSource.PlayOneShot(randomFootstepSound, 3f);
    }
}
using UnityEngine;

public class WaterCollider : MonoBehaviour
{
    [SerializeField] private GameObject player;

    public static bool inWater;

    public PlayerFootsteps PlayerFootsteps;

    public AudioClip[] waterFootstepSounds;
    public AudioClip[] grassFootstepSounds;

    private void Start()
    {       
        if (inWater)
            PlayerFootsteps.footstepSounds = waterFootstepSounds;
        else
            PlayerFootsteps.footstepSounds = grassFootstepSounds;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inWater = true;
            PlayerFootsteps.footstepSounds = waterFootstepSounds;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inWater = false;
            PlayerFootsteps.footstepSounds = grassFootstepSounds;
        }
    }
}
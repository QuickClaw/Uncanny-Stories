using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public UIPanels UIPanels;

    public float lookRadius = 10f;
    public float walkSpeed, runSpeed;

    [SerializeField] private Transform target, player;

    NavMeshAgent agent;

    [SerializeField] private AudioClip monsterRoar, monsterGrowl, heartBeat;

    [SerializeField] private GameObject mainCamera, jumpScareCam;
    [SerializeField] private GameObject darkPanel;

    [SerializeField] private AudioSource playerAS;

    public bool idle, walking, running;

    private bool roarHasPlayed, growlHasPlayed;

    private AudioClip clipToPlay, clipToSet;

    private bool jumpScared;

    private Vector3 playerStartPos;

    public DoorScript.Door Door;

    void Start()
    {
        playerStartPos = player.position;

        agent = GetComponentInParent<NavMeshAgent>();
    }

    void Update()
    {
        if (EnterHouse.inHouse)
            lookRadius = 0f;
        else
            lookRadius = 15f;

        if (idle)
            GetComponentInChildren<Animator>().Play("Idle");

        if (walking)
            GetComponentInChildren<Animator>().Play("crawl");

        if (running)
            GetComponentInChildren<Animator>().Play("crawl_fast");

        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius) // Canavar bizi kovalarken
        {
            idle = false;
            walking = false;
            running = true;

            if (roarHasPlayed == false)
            {
                GetComponent<AudioSource>().PlayOneShot(monsterRoar);
                Invoke(nameof(PlayHeartBeatDelayed), 2f);

                roarHasPlayed = true;
                growlHasPlayed = false;

                GetComponent<AudioSource>().Play();
            }

            gameObject.GetComponent<Patrol>().enabled = false;
            agent.speed = runSpeed;

            if (running && !EnterHouse.inHouse)
                agent.SetDestination(target.position);
        }
        if (distance <= agent.stoppingDistance)
        {
            if (!jumpScared)
            {
                player.gameObject.GetComponent<CharacterController>().enabled = false;
                player.gameObject.GetComponent<PlayerLook>().enabled = false;
                player.gameObject.GetComponent<PlayerMovement>().enabled = false;
                player.gameObject.GetComponent<PlayerFootsteps>().enabled = false;

                UIPanels.enabled = false;

                mainCamera.SetActive(false);
                jumpScareCam.SetActive(true);

                playerAS.PlayOneShot(monsterRoar, 4f);

                Invoke(nameof(PlayDarkPanel), 3f);
                Invoke(nameof(RestartFromCheckPoint), 10f);

                if (PlayerPrefs.HasKey("LastCharacterPosition" + "_x"))
                    player.position = new Vector3(PlayerPrefs.GetFloat("LastCharacterPosition" + "_x"), PlayerPrefs.GetFloat("LastCharacterPosition" + "_y"), PlayerPrefs.GetFloat("LastCharacterPosition" + "_z"));
                else
                    player.position = playerStartPos;

                jumpScared = true;
            }
            else
            {
                player.gameObject.GetComponent<CharacterController>().enabled = false;
                player.gameObject.GetComponent<PlayerLook>().enabled = false;
                player.gameObject.GetComponent<PlayerMovement>().enabled = false;
                player.gameObject.GetComponent<PlayerFootsteps>().enabled = false;

                UIPanels.enabled = false;

                playerAS.PlayOneShot(monsterRoar, 4f);
            }
        }
        if (distance > lookRadius || EnterHouse.inHouse)
        {
            idle = false;
            walking = true;
            running = false;

            GetComponent<Patrol>().enabled = true;
            agent.speed = walkSpeed;

            if (growlHasPlayed == false)
                GetComponent<AudioSource>().PlayOneShot(monsterGrowl);

            roarHasPlayed = false;
            growlHasPlayed = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name is "Door Interact Collider")
        {
            Door = other.GetComponentInParent<DoorScript.Door>();
            Debug.Log(name + " " + other.name);
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    void PlayDelayedSound(float delayInSeconds, AudioClip clip)
    {
        Invoke(nameof(PlaySound), delayInSeconds);

        clipToPlay = clip;
    }

    void PlaySound()
    {
        GetComponent<AudioSource>().PlayOneShot(clipToPlay);
    }

    void SetDelayedClip(float delayInSeconds, AudioClip clip)
    {
        Invoke(nameof(SetClip), delayInSeconds);

        clipToSet = clip;
    }

    void SetClip()
    {
        GetComponent<AudioSource>().clip = clipToSet;
    }

    void RestartFromCheckPoint()
    {
        player.gameObject.GetComponent<CharacterController>().enabled = true;
        player.gameObject.GetComponent<PlayerLook>().enabled = true;
        player.gameObject.GetComponent<PlayerMovement>().enabled = true;
        player.gameObject.GetComponent<PlayerFootsteps>().enabled = true;

        UIPanels.enabled = true;

        mainCamera.SetActive(true);
        jumpScareCam.SetActive(false);

        jumpScared = false;

        darkPanel.SetActive(false);
    }

    void PlayDarkPanel()
    {
        darkPanel.SetActive(true);
    }

    void PlayHeartBeatDelayed()
    {
        playerAS.PlayOneShot(heartBeat);
    }
}
using UnityEngine;
using UnityEngine.AI;

public class BearScript : MonoBehaviour
{
    public Transform player;

    public bool isSleeping, isRunning, isIdle;

    public bool isFollowingPlayer;

    private void Start()
    {
        GetComponent<NavMeshAgent>().stoppingDistance = 10f;
    }

    void Update()
    {      
        if (isSleeping)
            GetComponent<Animator>().Play("sleep");

        if (isIdle)
            GetComponent<Animator>().Play("idle");

        if (isRunning)
            GetComponent<Animator>().Play("run");

        if (!isSleeping)
        {
            if (Vector3.Distance(player.position, transform.position) > 10) // Ayý oyuncuyu takip ederken
            {
                isRunning = true;
                isFollowingPlayer = true;
                isIdle = false;

                GetComponent<NavMeshAgent>().isStopped = false;
                GetComponent<NavMeshAgent>().SetDestination(player.position);
            }
            else  // Ayý oyuncunun yanýndaysa
            {
                isRunning = false;
                isFollowingPlayer = false;
                isIdle = true;

                GetComponent<NavMeshAgent>().isStopped = true;
            }
        }
    }
}
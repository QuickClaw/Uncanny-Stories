using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{
    public NavMeshAgent agent;
    public float range; //radius of sphere
    public Transform centrePoint;

    private Vector3 currentDestination;
    public bool isWaiting;
    public float waitTime = 2f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetRandomDestination();
    }

    void Update()
    {
        if (gameObject.tag is "Monster")
        {
            if (isWaiting)
            {
                GetComponent<EnemyFootsteps>().enabled = false;
                GetComponent<EnemyController>().walking = false;
                GetComponent<EnemyController>().idle = true;
            }
            else
            {
                GetComponent<EnemyFootsteps>().enabled = true;
                GetComponent<EnemyController>().walking = true;
                GetComponent<EnemyController>().idle = false;
            }
        }
        if (gameObject.tag is "Bear")
        {
            if (isWaiting)
            {
                GetComponent<EnemyFootsteps>().enabled = false;
                GetComponent<BearScript>().isRunning = false;
                GetComponent<BearScript>().isIdle = true;
            }
            else
            {
                GetComponent<EnemyFootsteps>().enabled = true;
                GetComponent<BearScript>().isRunning = true;
                GetComponent<BearScript>().isIdle = false;
            }
        }       

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!isWaiting)
            {
                isWaiting = true;
                Invoke("SetRandomDestination", 2f);
            }
        }
        else
        {
            isWaiting = false;
        }
    }

    void SetRandomDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * range;
        randomDirection += centrePoint.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, range, 1);
        currentDestination = hit.position;
        agent.SetDestination(currentDestination);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(centrePoint.position, range);
    }
}
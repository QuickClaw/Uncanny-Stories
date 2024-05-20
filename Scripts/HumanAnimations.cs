using UnityEngine;
using UnityEngine.AI;

public class HumanAnimations : MonoBehaviour
{
    [SerializeField] private Transform target;

    public NavMeshAgent agent;

    public float destroyTime;

    void Start()
    {
        GetComponent<Animator>().Play("Idle");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag is "Player")
        {
            GotoTarget();
        }
    }

    void GotoTarget()
    {
        GetComponent<Animator>().Play("RunForward");
        agent.SetDestination(target.position);
        transform.LookAt(target);
        GetComponent<AudioSource>().Play();

        DestroyGameObject(destroyTime);
    }

    void DestroyGameObject(float time)
    {
        Destroy(gameObject, time);
    }
}

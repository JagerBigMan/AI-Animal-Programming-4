using UnityEngine;
using UnityEngine.AI;

public class PlayerBotWander : MonoBehaviour
{
    public float arriveDistance = 0.8f;

    private Vector3 minBounds = new Vector3(-20f, 0f, -20f);
    private Vector3 maxBounds = new Vector3(20f, 0f, 20f);

    private NavMeshAgent navAgent;
    private Vector3 targetPosition;


    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        AssignNewTarget();
    }

    // Update is called once per frame
    void Update()
    {
        if (navAgent.pathPending) return;

        if(navAgent.remainingDistance <= arriveDistance)
        {
            AssignNewTarget();
        }    
    }

    private void AssignNewTarget()
    {
        float x = Random.Range(minBounds.x, maxBounds.x);
        float z = Random.Range(minBounds.z, maxBounds.z);

        targetPosition = new Vector3(x, transform.position.y, z);
        navAgent.SetDestination(targetPosition);
    }
}

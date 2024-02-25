using UnityEngine;
using UnityEngine.AI;

public class NavMeshNavigation : MonoBehaviour
{
    public Transform targetObject;
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        targetObject = GameObject.FindGameObjectWithTag("Cheese").transform;

        if (targetObject == null)
        {
            Debug.LogError("Target object is not assigned to NavMeshNavigation script!");
        }

        navMeshAgent.baseOffset = 0.6f;
    }

    void Update()
    {
        if (targetObject != null)
        {
            // Set the destination of the NavMeshAgent to the position of the target object
            navMeshAgent.SetDestination(targetObject.position);
        }
    }
}

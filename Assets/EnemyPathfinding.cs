using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPathfinding : MonoBehaviour
{

    /*Credits: 
     * Slide tutorial: https://meganlaurajohns.blogspot.com/2019/03/patrolling-enemy-tutorial.html
     * Unity Documentation: https://docs.unity3d.com/Packages/com.unity.ai.navigation@1.1/manual/CreateNavMesh.html
     * GPT: https://chatgpt.com/g/g-Uz8I8oK3R-unity-helper/c/67296581-fe88-8007-bbdd-260a34e02543
     */

    private float close_enough = 2.5f;
    public GameObject[] waypoints;
    public NavMeshAgent agent;
    public int currentWaypointIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = waypoints[currentWaypointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 flatPosition = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 flatDestination = new Vector3(agent.destination.x, 0, agent.destination.z);

        if (Vector3.Distance(flatPosition, flatDestination) <= 2.5f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
            agent.destination = waypoints[currentWaypointIndex].transform.position;
        }
    }

    private void OnTriggerStay(Collider other)
    {

        Vector3 directionToOther = (other.transform.position - transform.position).normalized;
        if (other.gameObject.tag == "Player")
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, directionToOther, out hit))
            {
                if (hit.collider == other)
                {

                    agent.destination = other.transform.position;

                }
            }
        }
        else
        {
            currentWaypointIndex = 0;
            agent.destination = waypoints[currentWaypointIndex].transform.position;
        }

        if (Vector3.Distance(transform.position, agent.destination) <= close_enough) agent.destination = transform.position;
    }

}

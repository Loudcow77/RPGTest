using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour
{
    Transform target; //target to follow
    
    NavMeshAgent agent; //reference to our agent
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // If we have a target do this
        if (target != null)
        {
            // Moves our character to the position
            agent.SetDestination(target.position);
            faceTarget();
        }
    }

    public void MoveToPoint(Vector3 point)
    {
        agent.SetDestination(point);
    }
    
    public void followTarget (Interactable newTarget)
    {
        // Makes it so we stop just before our target object
        agent.stoppingDistance = newTarget.radius * 0.8f;
        agent.updateRotation = false;

        target = newTarget.interactionTransform;
    }

    // Makes us stop following our target
    public void stopFollowingTarget ()
    {
        agent.stoppingDistance = 0f;
        agent.updateRotation = true;

        target = null;
    }

    // Makes it so if the target spins and we are inside its radius that we spin with it
    void faceTarget()
    {
        // Gets our targets direction
        Vector3 direction = (target.position - transform.position).normalized;
        // Makes it so we face our targets X and Z
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        // Smooths out the look rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}

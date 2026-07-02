using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
public class NewMonoBehaviourScript : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    public Transform[] waypoints;

    private Rigidbody m_RigidBody;
    int m_CurrentwaypointIndex;
// Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_RigidBody=GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Transform currentwaypoint = waypoints[m_CurrentwaypointIndex];
        Vector3 currentToTarget = currentwaypoint.position - m_RigidBody.position;

        if (currentToTarget.magnitude < 0.1f)
        {
            m_CurrentwaypointIndex = (m_CurrentwaypointIndex + 1) % waypoints.Length;
        }
        Quaternion forwardRotation = Quaternion.LookRotation(currentToTarget);
        m_RigidBody.MoveRotation(forwardRotation);
        m_RigidBody.MovePosition(m_RigidBody.position + currentToTarget.normalized * moveSpeed * Time.deltaTime);
    }
}

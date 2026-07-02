using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using JetBrains.Annotations;
public class PlayerMovement : MonoBehaviour
{
    AudioSource m_AudioSource;
    public InputAction MoveAction;

    private float walkSpeed = 1.0f;
    private float turnSpeed = 20f;

    Rigidbody m_Rigidbody;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;
    Animator m_Animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
        m_Rigidbody = GetComponent<Rigidbody>();
        MoveAction.Enable();
        m_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var pos = MoveAction.ReadValue<Vector2>();

        float horizontal = pos.x;
        float vertical = pos.y;

        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);

        m_Rigidbody.MoveRotation(m_Rotation);
        m_Rigidbody.MovePosition(m_Rigidbody.position+m_Movement*walkSpeed*Time.deltaTime);

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        m_Animator.SetBool("IsWalking", isWalking);
        if (isWalking)
        {
            if (!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }
        }
        else
        {
            m_AudioSource.Stop();
        }
    }
}

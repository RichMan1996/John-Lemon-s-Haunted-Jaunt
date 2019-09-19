using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;
    Animator m_Animator;
    Rigidbody m_Rigibody;
    public float turnSpeed=20f;

    AudioSource m_AudioSource;
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigibody = GetComponent<Rigidbody>();
        m_AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();
        bool hashorizontalInput = !Mathf.Approximately(horizontal, 0);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hashorizontalInput || hasVerticalInput;
        m_Animator.SetBool("IsWalking", isWalking);
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);
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
    void OnAnimatorMove()
    {

        m_Rigibody.MovePosition(m_Rigibody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        m_Rigibody.MoveRotation(m_Rotation);
    }
}

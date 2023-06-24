using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Balloon : MonoBehaviour
{
    private Rigidbody m_Rigidbody;
    public bool isDetached = false;
    private AudioSource audioSource;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Rigidbody.isKinematic = true;
        m_Rigidbody.useGravity = false;

        isDetached = false;

        audioSource = GetComponent<AudioSource>();
    }

    public void Detach()
    {
        transform.SetParent(null);
        m_Rigidbody.isKinematic = false;
        var force = gameObject.AddComponent<ConstantForce>();
        
        force.force = Vector3.up;

        isDetached = true;

        audioSource.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "GameController" && !isDetached)
        {
            Detach();
        }
    }
}

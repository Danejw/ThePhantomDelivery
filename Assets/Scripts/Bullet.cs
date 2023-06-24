using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Bullet : MonoBehaviour
{
    public float lifeLength = 5;

    AudioSource audioSource;    

    private void OnEnable()
    {
        // destroy after so long
        Destroy(gameObject, lifeLength);
    }
}

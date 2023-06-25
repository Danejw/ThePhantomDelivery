using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Oar : MonoBehaviour
{
    [SerializeField] private Transform handle;
    [SerializeField] private Transform pivot;


    private void Update()
    {
        // Calculate the direction vector between the two targets
        Vector3 direction = (pivot.position - handle.position).normalized;

        // Make the object face the calculated direction
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = targetRotation;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace PhantomDelivery
{
    public class Fish : MonoBehaviour
    {
        [SerializeField] private XRSocketInteractor socket;
        [SerializeField] private XRBaseInteractable interactable;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Terrain")
            {
                Respawn();
            }
        }


        private void Respawn()
        {
            socket?.StartManualInteraction(interactable);
        }
    }
}
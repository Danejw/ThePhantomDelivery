using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace PhantomDelivery
{
    public class Net : MonoBehaviour
    {
        [SerializeField] AudioSource audioSource;
        [SerializeField] private XRSocketInteractor socket;
        [SerializeField] private XRBaseInteractable interactable;


        private void OnTriggerEnter(Collider other)
        {
            // pick up fish from water
            if (other.gameObject.tag == "Fish")
            {
                Destroy(other.gameObject);

                GameManager.Instance.AddFish(1);
            }


            if (other.gameObject.tag == "Water")
            {
                if (audioSource)
                {
                    if (!audioSource.isPlaying)
                    {
                        audioSource.Play();
                    }
                }
            }

            if (other.gameObject.tag == "Terrain")
            {
                Respawn();
            }
        }

        private void Update()
        {
            if (interactable.gameObject.transform.position.y < -10)
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

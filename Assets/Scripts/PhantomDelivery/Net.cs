using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace PhantomDelivery
{
    public class Net : MonoBehaviour
    {
        [SerializeField] public bool isFishInNet = false;
        [SerializeField] public GameObject fish;
        [SerializeField] private XRSocketInteractor socket;
        [SerializeField] private XRBaseInteractable interactable;

        private void Awake()
        {
            //interactable = GetComponent<XRBaseInteractable>();  
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Fish")
            {
                isFishInNet = true;
                Destroy(other.gameObject);
            }

            if (other.gameObject.tag == "Terrain")
            {
                Respawn();
            }
        }

        private void Update()
        {
            if (fish != null)
            {
                if (!isFishInNet)
                {
                    if (fish.gameObject.activeSelf) { fish.SetActive(false); }
                }

                if (isFishInNet)
                {
                    if (!fish.gameObject.activeSelf) { fish.SetActive(true); }
                }
            }
        }


        private void Respawn()
        {
            socket.StartManualInteraction(interactable);
        }
    }
}

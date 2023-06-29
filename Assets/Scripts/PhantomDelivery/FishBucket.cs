using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace PhantomDelivery
{
    public class FishBucket : MonoBehaviour
    {
        [SerializeField] private GameObject fish;
        [SerializeField] private XRSocketInteractor socket;
        public XRBaseInteractable interactable;

        private bool isDuplicating = false;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "GhostPalm")
            {
                GameManager.Instance.StealFish(1);
            }
        }

        private void Update()
        {
            // duplicate fish from the socket when the user has at least one fish
            if (GameManager.Instance.amountOfFish > 0)
            {
                if (!socket.hasSelection && !isDuplicating)
                {
                    DuplicateFish();
                }
            }
            else
            {
                if (socket.hasSelection)
                {
                    Destroy(socket.GetOldestInteractableSelected().transform.gameObject);
                }
            }
        }

        public void DuplicateFish()
        {
            StartCoroutine(DelayDuplicateFish(.1f));
        }

        public IEnumerator DelayDuplicateFish(float time)
        {
            isDuplicating = true;
            yield return new WaitForSeconds(time);
            var fishInstance = Instantiate(fish, transform);
            interactable = fishInstance.GetComponent<XRBaseInteractable>();
            socket.StartManualInteraction(interactable);
            isDuplicating = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace PhantomDelivery
{
    public class FishBucket : MonoBehaviour
    {
        [SerializeField] private GameObject fish;
        [SerializeField] private Net net;



        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Net")
            {
                other.gameObject.TryGetComponent<Net>(out net);

                if (net.isFishInNet)
                {
                    // add fish to bucket
                    GameManager.Instance.AddFish(1);
                    net.isFishInNet = false;

                }
                else
                {
                    if (GameManager.Instance.amountOfFish > 0)
                    {
                        // do something with fish if the net touches the bucket without a fish in the net

                    }
                }
            }
        }

        private void Update()
        {
            if (fish)
            {
                if (GameManager.Instance.amountOfFish > 0)
                {
                    if (!fish.gameObject.activeSelf)
                        fish.SetActive(true);
                }
                else
                {
                    if (fish.gameObject.activeSelf)
                        fish.SetActive(false);
                }
            }
        }
    }
}

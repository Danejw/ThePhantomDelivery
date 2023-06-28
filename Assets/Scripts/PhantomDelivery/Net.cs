using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PhantomDelivery
{
    public class Net : MonoBehaviour
    {
        [SerializeField] public bool isFishInNet = false;
        [SerializeField] public GameObject fish;


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Fish")
            {
                isFishInNet = true;
                Destroy(other.gameObject);
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
    }
}

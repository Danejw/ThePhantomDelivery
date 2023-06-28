using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PhantomDelivery
{
    public class FishBucket : MonoBehaviour
    {

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
                        // do something with fish

                    }
                }
            }
        }
    }
}

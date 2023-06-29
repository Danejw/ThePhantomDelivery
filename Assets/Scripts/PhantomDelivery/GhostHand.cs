using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PhantomDelivery
{
    public class GhostHand : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private FishBucket basket;
        [SerializeField] private bool isRevealed = false;

        private void Start()
        {
            if (!audioSource) audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            basket = FindObjectOfType<FishBucket>();
        }

        private void Update()
        {
            if (isRevealed)
            {
                transform.LookAt(basket.transform);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Boat")
            {
                if (!isRevealed)
                {
                    Reveal();
                }
                else
                {
                    StartGrabFishAnimation();
                }
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.tag == "Boat")
            {
                if (isRevealed)
                {
                    StartGrabFishAnimation();
                }
            }
        }

        private void Reveal()
        {
            animator?.SetTrigger("RevealHand");
            isRevealed = true;

            if (audioSource) audioSource.Play();
        }

        private void StartGrabFishAnimation()
        {
            animator?.SetTrigger("GrabFish");

            if (audioSource) audioSource.Play();
        }
    }
}

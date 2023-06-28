using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PhantomDelivery
{
    public class PhantomHouse : MonoBehaviour
    {
        public Timer timer;
        public int duration = 60;
        public int worth = 1;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Fish")
            {
                // Tells the game manager to send an event
                GameManager.Instance.SuccessfulDelivery(worth);
            }
        }

        private void OnEnable()
        {
            timer = new Timer(duration);
            timer.Start();
        }

        private void Update()
        {
            timer.Update();

            if (timer.RemainingTime <= 0)
            {
                FailedDelivery();
            }
        }

        private void FailedDelivery()
        {
            // Tell the gamemanager to throw an event
            GameManager.Instance.FailedDelivery();
        }
    }
}

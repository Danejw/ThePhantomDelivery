using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PhantomDelivery
{
    public class PhantomHouse : MonoBehaviour
    {
        [SerializeField] TMPro.TMP_Text uiTimer;

        public Timer timer;
        public int duration = 60;
        public int fishCount = 1;
        public int worth = 1;

        private bool success = false;

        public bool randomize;
        public Vector2 randomRange;

        public List<GameObject> fishList = new List<GameObject>();

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "CaughtFish")
            {
                // Tells the game manager to send an event
                if (!success)
                {
                    Destroy(other.gameObject);
                    GameManager.Instance.SuccessfulDelivery(worth);
                    success = true;
                }
            }
        }

        public void Init()
        {
            if (randomize)
            {
                fishCount = Random.Range((int)randomRange.x, (int)randomRange.y);
                worth = fishCount;
            }
                 

            for (int i=0; i < fishCount; i++)
            {
                var fish = GameManager.Instance.PlaceFish();
                fishList.Add(fish);
            }

            timer = new Timer(duration);
            timer.Start();
        }

        private void Update()
        {
            if (timer != null) timer.Update();

            if (uiTimer) if (uiTimer.text != timer.RemainingTime.ToString()) { uiTimer.text = Mathf.RoundToInt(timer.RemainingTime).ToString(); }

        }

        public void ClearFishList()
        {
            foreach (var fish in fishList)
            {
                Destroy(fish);
            }
            fishList.Clear();
        }
    }
}

using UnityEngine;

namespace PhantomDelivery
{
    public class Timer
    {
        public float duration;
        [SerializeField] private bool isRunning;
        [SerializeField] private float elapsedTime;

        public float Duration => duration;
        public bool IsRunning => isRunning;
        public float ElapsedTime => elapsedTime;
        public float RemainingTime => Mathf.Max(duration - elapsedTime, 0f);

        public Timer(float duration)
        {
            this.duration = duration;
            isRunning = false;
            elapsedTime = 0f;
        }

        public void Start()
        {
            isRunning = true;
        }

        public void Stop()
        {
            isRunning = false;
        }

        public void Reset()
        {
            isRunning = false;
            elapsedTime = 0f;
        }

        public void Update()
        {
            if (isRunning)
            {
                elapsedTime += Time.deltaTime;

                if (elapsedTime >= duration)
                {
                    Stop();
                }
            }
        }
    }
}


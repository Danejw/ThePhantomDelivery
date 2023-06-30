using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PhantomDelivery
{
    public class InGameUi : MonoBehaviour
    {
        [SerializeField] private ParticleSystem coinParticle;


        private void Awake()
        {
            GameManager.onGameStateChanged += GameState;
            GameManager.onSuccessfulDelivery += PlayCoinParticle;
        }

        private void OnDestroy()
        {
           GameManager.onGameStateChanged -= GameState;
            GameManager.onSuccessfulDelivery -= PlayCoinParticle;
        }

        private void PlayCoinParticle()
        {
            if (coinParticle) coinParticle.Play();
        }

        private void GameState(GameManager.GameState state)
        {
            switch (state)
            {
                case GameManager.GameState.InGame:
                    gameObject.SetActive(true); 
                    break;
                case GameManager.GameState.BeforeStart:
                    gameObject.SetActive(false);
                    break;
                case GameManager.GameState.EndGame:
                    gameObject.SetActive(true);
                    break;
            }
        }
    }
}

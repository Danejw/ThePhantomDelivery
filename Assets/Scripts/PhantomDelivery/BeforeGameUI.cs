using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PhantomDelivery
{
    public class BeforeGameUI : MonoBehaviour
    {

        private void Start()
        {
            GameManager.onGameStateChanged += GameState;
        }

        private void OnDestroy()
        {
           GameManager.onGameStateChanged -= GameState;
        }



        private void GameState(GameManager.GameState state)
        {
            switch (state)
            {
                case GameManager.GameState.InGame:
                    gameObject.SetActive(false); 
                    break;
                case GameManager.GameState.BeforeStart:
                    gameObject.SetActive(true);
                    break;
                case GameManager.GameState.EndGame:
                    gameObject.SetActive(false);
                    break;
            }
        }
    }
}

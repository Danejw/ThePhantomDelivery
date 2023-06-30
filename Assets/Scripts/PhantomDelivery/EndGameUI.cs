using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PhantomDelivery
{
    public class EndGameUI : MonoBehaviour
    {
        [SerializeField] private TMPro.TMP_Text text;

        [TextArea]
        [SerializeField] private string winText = "You Win!";
        [TextArea]
        [SerializeField] private string loseText = "You Lose!";


        private void Awake()
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
                    gameObject.SetActive(false);
                    break;
                case GameManager.GameState.EndGame:
                    if (GameManager.Instance.amountOfCoin >= GameManager.Instance.amountOfCoinToWin)
                    {
                        text.text = winText;
                    }
                    else
                    {
                        text.text = loseText;
                    }

                    gameObject.SetActive(true);
                    break;
            }
        }
    }
}

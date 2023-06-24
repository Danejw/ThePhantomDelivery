using PhantomDelivery;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;


namespace PhantomDelivery {

    public class GameManager : MonoBehaviour
    {
        public enum GameState
        {
            BeforeStart,
            InGame,
            EndGame,
        }

        [SerializeField] private GameState _gameState;

        public GameState gameState
        {
            get { return _gameState; }
            set 
            { 
                _gameState = value; 

                switch (gameState)
                {
                    case GameState.BeforeStart:
                        // Create the global timer if it doesnt already exist
                        if (globalTimer == null) globalTimer = new Timer(globalMaxTime);
                        globalTimer.Reset();

                        // reset coin
                        break;
                    case GameState.InGame:
                        // What happens when the game starts
                        globalTimer.Start();
                        break;
                    case GameState.EndGame:
                        // What happens when the game ends
                        break;
                }
            }
        }

        [SerializeField] private Timer globalTimer;
        [SerializeField] private int globalMaxTime = 100;

        // debug
        [SerializeField] private float currentGlobalTime;

        // amount of fish caught
        // amount of coin

        // ghost hand prefab

        [SerializeField] private List<Timer> requests = new List<Timer>();

        private void Start()
        {
            gameState = GameState.BeforeStart;
        }

        private void Update()
        {        
            // What happens when the game is in play
            if (gameState == GameState.InGame)
            {
                globalTimer.Update();

                // Randomly Create Requests

                // Randomly Place Fish

                // Randomly Place Ghost hands
            }

            // Condition to end the game
            if (globalTimer.RemainingTime <= 0) gameState = GameState.EndGame;


            // debug
            currentGlobalTime = globalTimer.ElapsedTime;
        }

        public void StartGame()
        {
            gameState = GameState.InGame;
        }

        public void EndGame()
        {
            gameState = GameState.EndGame;
        }

        public void ResetGame()
        {
            gameState = GameState.BeforeStart;
        }

        // Randomly Create Requests

        // Randomly Place Fish

        // Randomly Place Ghost hands

        // collect fish

        // remove fish

        // empty fish bucket

        // collect coin

        // reset coin
    }
}

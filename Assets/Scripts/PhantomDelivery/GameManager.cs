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

        [SerializeField] private int amountOfFish = 0;
        [SerializeField] private int amountOfCoin = 0;

        [SerializeField] private int minRadius = 5;
        [SerializeField] private int maxRadius = 20;
        [SerializeField] private GameObject fishPrefab;
        [SerializeField] private GameObject ghostHandPrefab;
        [SerializeField] private List<GameObject> fishList;
        [SerializeField] private List<GameObject> ghostHandList;

        [SerializeField] private List<Timer> requests = new List<Timer>();

        // debug
        [SerializeField] private float currentGlobalTime;

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

            ResetFish();
            ResetCoin();
        }


        // collect fish
        public void ChangeFish(int amount) => amountOfFish += amount;

        // empty fish bucket
        public void ResetFish() => amountOfFish = 0;

        // collect coin
        public void ChangeCoin(int amount) => amountOfCoin += amount;

        // reset coin
        public void ResetCoin() => amountOfCoin = 0;


        // Randomly Create Requests

        // Randomly Place Fish
        public void PlaceFish()
        {
            if (fishPrefab)
            {
                var fish = Instantiate(fishPrefab, RandomPositionWithinRange(Vector3.zero, minRadius, maxRadius), Quaternion.identity, transform);
                fishList.Add(fish);
            }
        }

        // Randomly Place Ghost hands
        public void PlaceGhostHands()
        {
            if (ghostHandPrefab)
            {
                var hand = Instantiate(ghostHandPrefab, RandomPositionWithinRange(Vector3.zero, minRadius, maxRadius), Quaternion.identity, transform);
                ghostHandList.Add(hand);
            }
        }

        public void ClearFishList()
        {
            foreach (var fish in fishList)
            {
                Destroy(fish);
            }
            fishList.Clear();
        }

        public void ClearGhostHandList()
        {
            foreach (var hand in ghostHandList)
            {
                Destroy(hand);
            }
            ghostHandList.Clear();
        }

        public Vector3 RandomPositionWithinRange(Vector3 center, float minDistance, float maxDistance)
        {
            float angle = Random.Range(0f, 360f);
            float distance = Random.Range(minDistance, maxDistance);

            // Calculate the x and z coordinates based on angle and distance
            float x = center.x + distance * Mathf.Cos(angle * Mathf.Deg2Rad);
            float z = center.z + distance * Mathf.Sin(angle * Mathf.Deg2Rad);

            // Create and return the resulting random position
            Vector3 randomPosition = new Vector3(x, center.y, z);
            return randomPosition;
        }

    }
}

using PhantomDelivery;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using TMPro;


namespace PhantomDelivery {

    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;
        public static GameManager Instance
        {
            get { return _instance; }
        }

        public enum GameState
        {
            BeforeStart,
            InGame,
            EndGame,
        }

        [Space(5)]
        [Header("Game State")]
        [SerializeField] private GameState _gameState;

        public GameState gameState
        {
            get { return _gameState; }
            set 
            { 
                _gameState = value; 

                onGameStateChanged?.Invoke(value);

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
                        StartCoroutine(RequestRoutine(1));
                        StartCoroutine(PlaceFishRoutine(10, 1));
                        break;
                    case GameState.EndGame:
                        // What happens when the game ends
                        break;
                }

                Debug.Log("The Game State has changed to the " + _gameState + " state.");
            }
        }

        // Events
        public delegate void OnGameStateChanged(GameState state);
        public static event OnGameStateChanged onGameStateChanged;

        public delegate void OnGameStart();
        public static event OnGameStart onGameStart;

        public delegate void OnGameEnd();
        public static event OnGameEnd onGameEnd;

        public delegate void OnGameReset();
        public static event OnGameReset onGameReset;

        public delegate void OnFishCaught();
        public static event OnFishCaught onFishCaught;

        public delegate void OnFishStolen();
        public static event OnFishStolen onFishStolen;

        public delegate void OnNewDeliveryRequest();
        public static event OnNewDeliveryRequest onNewDeliveryRequest;

        public delegate void OnSuccessfulDelivery();
        public static event OnSuccessfulDelivery onSuccessfulDelivery;

        public delegate void OnFailedDelivery();
        public static event OnFailedDelivery onFailedDelivery;

        [Space(5)]
        [Header("UI References")]
        [SerializeField] private TMP_Text globalTimeAmountText;
        [SerializeField] private TMP_Text fishAmountText;
        [SerializeField] private TMP_Text coinAmountText;
        [SerializeField] private TMP_Text requestAmountTimeText;


        [Space(5)]
        [Header("Game Values")]
        [SerializeField] private Timer globalTimer;
        [SerializeField] private int globalMaxTime = 100;
        [SerializeField] private float currentGlobalTime;

        [SerializeField] public int amountOfFish = 0;
        [SerializeField] private int amountOfCoin = 0;

        [SerializeField] private int minRadius = 5;
        [SerializeField] private int maxRadius = 20;

        [Space(5)]
        [Header("Prefab References")]
        [SerializeField] private GameObject fishPrefab;
        [SerializeField] private GameObject ghostHandPrefab;
        [SerializeField] private GameObject ghostHousePrefab;
        [SerializeField] private List<GameObject> fishList;
        [SerializeField] private List<GameObject> ghostHandList;

        [SerializeField] private PhantomHouse currentRequest;

        private void Awake()
        {
            // Check if an instance already exists
            if (_instance != null && _instance != this)
            {
                // Destroy the duplicate instance
                Destroy(this.gameObject);
                return;
            }

            // Assign the singleton instance
            _instance = this;
        }

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
            if (currentRequest.timer.RemainingTime <= 0) FailedDelivery();

            // debug
            currentGlobalTime = globalTimer.ElapsedTime;

            // Update UI
            if (fishAmountText && fishAmountText.text != amountOfFish.ToString()) { fishAmountText.text = amountOfFish.ToString(); }
            if (coinAmountText && coinAmountText.text != amountOfCoin.ToString()) { coinAmountText.text = amountOfCoin.ToString(); }
            if (globalTimeAmountText && globalTimeAmountText.text != globalTimer.RemainingTime.ToString()) { globalTimeAmountText.text = Mathf.RoundToInt(globalTimer.RemainingTime).ToString(); }
            if (currentRequest) if (requestAmountTimeText && requestAmountTimeText.text != currentRequest.timer.RemainingTime.ToString()) { requestAmountTimeText.text = Mathf.RoundToInt(currentRequest.timer.RemainingTime).ToString(); }
        }

        public void StartGame()
        {
            gameState = GameState.InGame;

            onGameStart?.Invoke();
        }

        public void EndGame()
        {
            gameState = GameState.EndGame;

            onGameEnd?.Invoke();
        }

        public void ResetGame()
        {
            gameState = GameState.BeforeStart;

            ResetFish();
            ResetCoin();

            ClearFishList();
            ghostHandList.Clear();

            currentGlobalTime = 0;

            onGameReset?.Invoke();
        }


        // collect fish
        public void AddFish(int amount)
        {
            amountOfFish += amount;

            onFishCaught?.Invoke();
        }

        public void StealFish(int amount)
        {
            if (amountOfFish == 0) return;

            amountOfFish -= amount;

            onFishStolen?.Invoke();
        }

        // empty fish bucket
        public void ResetFish()
        {
            amountOfFish = 0;
        }

        // collect coin
        public void ChangeCoin(int amount)
        {
            if (amount < 0 && amountOfCoin == 0) return;

            amountOfCoin += amount;
        }

        // reset coin
        public void ResetCoin()
        {
            amountOfCoin = 0;
        }

        public void SuccessfulDelivery(int coinAmount)
        {
            if (currentRequest) Destroy(currentRequest.gameObject);

            onSuccessfulDelivery?.Invoke();

            ChangeCoin(coinAmount);

            StartCoroutine(RequestRoutine(1));
        }

        public void FailedDelivery()
        {
            if (currentRequest) Destroy(currentRequest.gameObject);

            onFailedDelivery?.Invoke();

            StartCoroutine(RequestRoutine(3));
        }


        // Randomly Create Requests
        public PhantomHouse PlaceGhostHouse()
        {
            if (!ghostHousePrefab) return null;

            var house = Instantiate(ghostHousePrefab, RandomPositionWithinRange(Vector3.zero, maxRadius, maxRadius + 20),  Quaternion.identity, transform);
            house.transform.LookAt(Vector3.zero);

            return house.GetComponentInChildren<PhantomHouse>();
        }

        private IEnumerator RequestRoutine(int delay)
        {
            yield return new WaitForSeconds(delay);
            var house = PlaceGhostHouse();
            currentRequest = house;
        }


        // Randomly Place Fish
        public void PlaceFish()
        {
            if (fishPrefab)
            {
                var fish = Instantiate(fishPrefab, RandomPositionWithinRange(Vector3.zero, minRadius, maxRadius), Quaternion.identity, transform);
                fishList.Add(fish);
            }
        }

        public IEnumerator PlaceFishRoutine(int amount, float time)
        {
            for (int i = 0; i < amount; i++)
            {
                PlaceFish();
                yield return new WaitForSeconds(time);
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

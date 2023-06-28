using UnityEditor;
using UnityEngine;

namespace PhantomDelivery
{
    [CustomEditor(typeof(GameManager))]
    public class GameManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GameManager gameManager = (GameManager)target;

            if (GUILayout.Button("Start Game"))
                gameManager.StartGame();

            if (GUILayout.Button("End Game"))
                gameManager.EndGame();

            if (GUILayout.Button("Reset Game"))
                gameManager.ResetGame();

            if (GUILayout.Button("Place Fish"))
                gameManager.PlaceFish();

            if (GUILayout.Button("Place Ghost Hands"))
                gameManager.PlaceGhostHands();

            if (GUILayout.Button("Clear Fish List"))
                gameManager.ClearFishList();

            if (GUILayout.Button("Clear Ghost Hand List"))
                gameManager.ClearGhostHandList();

            if (GUILayout.Button("Add Fish"))
                gameManager.AddFish(1);

            if (GUILayout.Button("Steal Fish"))
                gameManager.StealFish(1);

            if (GUILayout.Button("Reset Fish"))
                gameManager.ResetFish();

            if (GUILayout.Button("Add Coin"))
                gameManager.ChangeCoin(1);

            if (GUILayout.Button("Reset Coin"))
                gameManager.ResetCoin();

            if (GUILayout.Button("Place Ghost House"))
                gameManager.PlaceGhostHouse();
        }
    }
}

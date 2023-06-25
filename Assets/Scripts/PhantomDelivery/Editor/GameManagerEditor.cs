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
        }
    }
}

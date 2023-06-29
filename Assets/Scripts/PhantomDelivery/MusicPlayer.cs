using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PhantomDelivery.GameManager;

namespace PhantomDelivery
{
    [RequireComponent(typeof(AudioSource))]
    public class MusicPlayer : MonoBehaviour
    {
        [SerializeField] private AudioClip gameBeforeStartClip;
        [SerializeField] private AudioClip gameStartClip;
        [SerializeField] private AudioClip gameEndClip;

        private AudioSource audioSource;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();

            GameManager.onGameStateChanged += GameStateChanged;
        }

        private void OnDestroy()
        {
            GameManager.onGameStateChanged -= GameStateChanged;

        }

        private void GameStateChanged(GameManager.GameState state)
        {
            switch (state)
            {
                case GameManager.GameState.BeforeStart:
                    if (gameBeforeStartClip) PlayAudioClip(gameBeforeStartClip);
                    break;
                case GameManager.GameState.InGame:
                    if (gameStartClip) PlayAudioClip(gameStartClip);
                    break;
                case GameManager.GameState.EndGame:
                    if (gameEndClip) PlayAudioClip(gameEndClip);
                    break;
            }
        }

        private void PlayAudioClip(AudioClip clip)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}
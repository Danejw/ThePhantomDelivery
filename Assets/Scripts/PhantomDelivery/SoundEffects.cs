using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PhantomDelivery
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundEffects : MonoBehaviour
    {
        [SerializeField] private AudioClip gameStartClip;
        [SerializeField] private AudioClip gameEndClip;

        [SerializeField] private AudioClip fishCaughtClip;
        [SerializeField] private AudioClip fishStolenClip;

        [SerializeField] private AudioClip newDeliveryRequestClip;
        [SerializeField] private AudioClip successfulDeliveryRequestClip;
        [SerializeField] private AudioClip failedDeliveryRequestClip;

        private AudioSource audioSource;


        private void Start()
        {
            audioSource = GetComponent<AudioSource>();

            GameManager.onGameStart += PlayGameStart;
            GameManager.onGameEnd += PlayGameEnd;

            GameManager.onFishCaught += PlayFishCaught;
            GameManager.onFishStolen += PlayFishStolen;

            GameManager.onNewDeliveryRequest += PlayNewDelivery;
            GameManager.onSuccessfulDelivery += PlaySuccesfulDelivery;
            GameManager.onFailedDelivery += PlayFailedDelivery;
        }

        private void OnDestroy()
        {

            GameManager.onGameStart -= PlayGameStart;
            GameManager.onGameEnd -= PlayGameEnd;

            GameManager.onFishCaught -= PlayFishCaught;
            GameManager.onFishStolen -= PlayFishStolen;

            GameManager.onNewDeliveryRequest -= PlayNewDelivery;
            GameManager.onSuccessfulDelivery -= PlaySuccesfulDelivery;
            GameManager.onFailedDelivery -= PlayFailedDelivery;
        }

        private void PlayFailedDelivery() 
        {
            if (failedDeliveryRequestClip) 
            {
                PlayAudioClip(failedDeliveryRequestClip); 
            }
        }

        private void PlaySuccesfulDelivery()
        {
            if (successfulDeliveryRequestClip)
            {
                PlayAudioClip(successfulDeliveryRequestClip);
            }
        }

        private void PlayNewDelivery()
        {
            if (newDeliveryRequestClip)
            {
                PlayAudioClip(newDeliveryRequestClip);
            }
        }

        private void PlayFishStolen()
        {
            if (fishStolenClip)
            {
                PlayAudioClip(fishStolenClip);
            }
        }

        private void PlayFishCaught()
        {
            if (fishCaughtClip)
            {
                PlayAudioClip(fishCaughtClip);
            }
        }

        private void PlayGameEnd()
        {
            if (gameEndClip)
            {
                PlayAudioClip(gameEndClip);
            }
        }

        private void PlayGameStart()
        {
            if (gameStartClip)
            {
                PlayAudioClip(gameStartClip);
            }
        }

        private void PlayAudioClip(AudioClip clip)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}

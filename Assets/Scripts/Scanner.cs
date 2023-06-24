using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;


public class Scanner : XRGrabInteractable
{
    [Header("Scanner Data")]
    public Animator animator;
    public LineRenderer laserRenderer;
    public AudioSource audioSource;
    public TextMeshProUGUI targetName;
    public TextMeshProUGUI targetPosition;


    [Header("Scanner Effects")]
    public AudioClip scanningAudioClip;
    public Material scanMaterial;

    protected override void Awake()
    {
        base.Awake();

        ScannerActivated(false);
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        animator.SetBool("Opened", true);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        animator.SetBool("Opened", false);
    }

    protected override void OnActivated(ActivateEventArgs args)
    {
        base.OnActivated(args);

        ScannerActivated(isActivated: true);
    }

    protected override void OnDeactivated(DeactivateEventArgs args)
    {
        base.OnDeactivated(args);

        ScannerActivated(false);
    }

    private void ScannerActivated(bool isActivated)
    {
        laserRenderer.gameObject.SetActive(isActivated);
        targetName.gameObject.SetActive(isActivated);
        targetPosition.gameObject.SetActive(isActivated);

        switch (isActivated)
        {
            case true:
                if (!audioSource.isPlaying)
                {
                    audioSource.clip = scanningAudioClip;
                    audioSource.Play();
                }
                break;
            case false:
                // stop audio
                if (audioSource.isPlaying)
                    audioSource.Stop();

                targetName.SetText("Ready To Scan");
                break;
        }
    }

    private void ScanForObjects()
    {
        RaycastHit hit;

        Vector3 worldHit = laserRenderer.transform.position + laserRenderer.transform.forward * 1000.0f;


        if (Physics.Raycast(laserRenderer.transform.position, laserRenderer.transform.forward, out hit))
        {         
            worldHit = hit.point;

            targetName.SetText(hit.collider.name);
            targetPosition.SetText(hit.transform.position.ToString());
            
        }

        laserRenderer.SetPosition(1, laserRenderer.transform.InverseTransformPoint(worldHit));
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        if (laserRenderer.gameObject.activeSelf)
            ScanForObjects();
    }
}

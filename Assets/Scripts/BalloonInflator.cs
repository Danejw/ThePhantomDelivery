using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BalloonInflator : XRGrabInteractable
{
    [Header("Balloon Data")]
    public Transform attachPoint;
    public Balloon balloonPrefab;
    public GameObject trigger;
    private AudioSource audioSource;

    private Balloon m_BalloonInstance;
    private XRBaseController m_Controller;

    protected override void Awake()
    {
        base.Awake();

        audioSource = GetComponent<AudioSource>();
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        m_BalloonInstance = Instantiate(balloonPrefab, attachPoint);

        var controllerInteractor = args.interactorObject as XRBaseControllerInteractor;
        m_Controller = controllerInteractor.xrController;

        m_Controller.SendHapticImpulse(1, 0.5f);
        Debug.Log(m_Controller);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        Destroy(m_BalloonInstance.gameObject);
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        if (isSelected && m_Controller != null)
        {
            if (!m_BalloonInstance.GetComponent<Balloon>().isDetached)
            {
                m_BalloonInstance.transform.localScale = Vector3.one * Mathf.Lerp(1.0f, 4.0f, m_Controller.activateInteractionState.value);
                
                // Play sounnds when trigger is down
                if (!audioSource.isPlaying && m_Controller.activateInteractionState.value > .3f)
                {
                    audioSource.loop = true;
                    audioSource.Play();
                }

                if (audioSource.isPlaying && m_Controller.activateInteractionState.value < .3f)
                {
                    audioSource.Pause();
                }
            }
            else
            {
                if (audioSource.isPlaying) audioSource.Pause();
            }

            trigger.transform.localEulerAngles = new Vector3( Mathf.Lerp(0, 10, m_Controller.activateInteractionState.value), 0, 0);
        }
    }
}

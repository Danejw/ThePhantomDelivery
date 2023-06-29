using System.Collections;
using System.Collections.Generic;
using Unity.XRContent.Interaction;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Motor : XRJoystick
{
    public bool debug;

    [Space(5)]
    [Header("References")]
    [SerializeField] private GameObject boat;
    [SerializeField] private GameObject meshContainer;

    [Space(5)]
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip startupClip;
    [SerializeField] private AudioClip idleClip;
    [SerializeField] private AudioClip runningClip;
    public bool useStartup = false;
    public bool useIdle = false;

    [Space(5)]
    [Header(header: "Forward Movement")]
    [SerializeField] private float throttleSensitivity = .5f;
    [SerializeField] private float maxSpeed = 5f; // max forward speed of the boat
    private float currentSpeed; // Current forward speed of the boat
    [SerializeField] private float acceleration = .5f; // speed of accerelation of the boat

    [Space(5)]
    [Header(header: "Rotational Movement")]
    [SerializeField] private float turnSpeed = 10f; // Turning speed of the boat

    [Space(5)]
    [Header(header: "Tilt")]
    [SerializeField] private float zRotationFactor = .5f;
    [SerializeField] private float xRotationFactor = 2f;
    private float currentRotationZ;
    private float currentRotationX;

    [SerializeField] private bool isActivated = false;
    private XRBaseController m_Controller;


    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        if (audioSource == null) { GetComponent<AudioSource>(); }
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        var controllerInteractor = args.interactorObject as XRBaseControllerInteractor;
        m_Controller = controllerInteractor.xrController;

        m_Controller.SendHapticImpulse(1, 0.5f);

        // play motor start up sound clip then when done, loop the idle sound
        if (useStartup) StartCoroutine(EngineStartUp());

        if (debug) Debug.Log(m_Controller);
        if (debug) Debug.Log("Selected");
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        // stop the audio source from playing
        StopPlayingMotorAudio();

        if (debug) Debug.Log("Select Exit");
    }

    protected override void OnActivated(ActivateEventArgs args)
    {
        base.OnActivated(args);

        isActivated = true;

        // haptics
        m_Controller.SendHapticImpulse(1, 20);

        // play and loop the motor running sound clip
        PlayMotorRunningLoop();

        if (debug) Debug.Log("Activated");
        if (debug) Debug.Log("TriggerValue: " + m_Controller.activateInteractionState.value);
    }

    protected override void OnDeactivated(DeactivateEventArgs args)
    {
        base.OnDeactivated(args);

        isActivated = false;

        // haptics (stops it)
        m_Controller.SendHapticImpulse(0, 0);

        // play and loop idle sound clip
        if (useIdle) PlayMotorIdleLoop();

        if (debug) Debug.Log("DeActivated");

    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        if (isActivated && isSelected)
        {
            MoveBoat(Value, m_Controller.activateInteractionState.value);
        }
        else
        {
            StopBoat();
        }
    }


    // Movement Stuff
    public void MoveBoat(Vector2 joystickValue, float triggerValue)
    {
        if (triggerValue >= throttleSensitivity)
        {
            // Calculate the target forward movement based on the input
            float targetForwardMovement = Mathf.Max(triggerValue, 0f) * maxSpeed;

            // Calculate the interpolation factor based on the acceleration and time
            float interpolationFactor = Mathf.Lerp(0f, 1f, acceleration * Time.deltaTime);

            // Gradually increase the current speed towards the target speed
            currentSpeed = Mathf.Lerp(currentSpeed, targetForwardMovement, interpolationFactor);

            // Calculate the boat's turning torque based on the input and speed
            float turningTorque = joystickValue.x * turnSpeed * currentSpeed;

            // Rotate the boat based on the turning torque
            boat.transform.Rotate(0f, -turningTorque * Time.deltaTime, 0f);

            // Calculate the rotation amount on the z-axis based on the turning torque
            currentRotationZ = turningTorque * zRotationFactor;
            // Calculate the rotation amount on the x-axis based on the turning torque
            currentRotationX = Mathf.Abs( currentSpeed * xRotationFactor );

            // Apply the rotation to the boat's transform
            meshContainer.transform.rotation = Quaternion.Euler(-currentRotationX, boat.transform.rotation.eulerAngles.y, currentRotationZ);

            // Move the boat forward
            boat.transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);

            if (debug) Debug.Log("Move boat:  " + triggerValue + " : " + currentSpeed + " : " + turningTorque);
        }      
    }

    private void StopBoat()
    {
        if (currentSpeed > 0)
        {
            // Calculate the deceleration factor based on time
            float decelerationFactor = Mathf.Lerp(0f, 1f, acceleration * Time.deltaTime);

            // Gradually decrease the current speed towards 0
            currentSpeed = Mathf.Lerp(currentSpeed, 0f, decelerationFactor);

            // Move the boat forward
            boat.transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);

            if (debug) Debug.Log("Move boat:  " + currentSpeed);
        }

        if (currentRotationZ != 0)
        {
            // Calculate the deceleration factor based on time
            float decelerationFactor = Mathf.Lerp(0f, 1f, acceleration * Time.deltaTime);

            // Gradually decrease the current speed towards 0
            currentRotationZ = Mathf.Lerp(currentRotationZ, 0f, decelerationFactor);

            // Apply the rotation to the boat's transform
            meshContainer.transform.rotation = Quaternion.Euler(-currentRotationX, boat.transform.rotation.eulerAngles.y, currentRotationZ);

            if (debug) Debug.Log("Boat Z Rotation:  " + currentRotationZ);
        }

        if (currentRotationX != 0)
        {
            // Calculate the deceleration factor based on time
            float decelerationFactor = Mathf.Lerp(0f, 1f, acceleration * Time.deltaTime);

            // Gradually decrease the current speed towards 0
            currentRotationX = Mathf.Lerp(currentRotationX, 0f, decelerationFactor);

            // Apply the rotation to the boat's transform
            meshContainer.transform.rotation = Quaternion.Euler(-currentRotationX, boat.transform.rotation.eulerAngles.y, currentRotationZ);

            if (debug) Debug.Log("Boat X Rotation:  " + currentRotationX);
        }
    }


    // Audio Stuff
    private IEnumerator EngineStartUp()
    {
        // play start up clip and wait for it to be done
        PlayMotorStartup();
        yield return new WaitForSeconds(audioSource.clip.length);
        // then loop the idle sound clip
        PlayMotorIdleLoop();
    }

    private void PlayMotorStartup()
    {
        if (audioSource && startupClip)
        {
            audioSource.clip = startupClip;
            audioSource.loop = false;
            audioSource.Play();
        }
    }

    private void PlayMotorRunningLoop()
    {
        if (audioSource && runningClip)
        {
            audioSource.clip = runningClip;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    private void PlayMotorIdleLoop()
    {
        if (audioSource && idleClip)
        {
            audioSource.clip = idleClip;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    private void StopPlayingMotorAudio()
    {
        if(audioSource && audioSource.isPlaying)
            audioSource.Stop();
    }
}

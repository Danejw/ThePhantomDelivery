using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class MyGrabbableObj : XRBaseInteractable
{
    Vector3 firstVector = new Vector3(-1, 0, 1);
    Vector3 secondVector = new Vector3(0, 1, 0);

    private void Start()
    {
        float dotProduct = Vector3.Dot(firstVector, secondVector);
        Debug.Log(dotProduct);
    }

    public override Transform GetAttachTransform(IXRInteractor interactor)
    {
        Debug.Log("Attach point on: " + transform.gameObject.name);
        return transform;
    }

}

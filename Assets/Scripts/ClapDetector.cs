using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ClapDetector : MonoBehaviour
{
    public OVRHand rightHand;
    public OVRHand leftHand;

    public float thresholdDistance = 1f;

    private Vector3 newPosRight;
    private Vector3 prevPosRight;
    private Vector3 rightHandVelocity;

    private Vector3 newPosLeft;
    private Vector3 prevPosLeft;
    private Vector3 leftHandVelocity;

    public UnityEvent onClap;
    // public GameObject clapFeedback;
    public AudioSource clapSound;

    private bool clapInvoked = false;
    void Start()
    {
        Debug.Log("ClapDetector Start() called");
    }

    void FixedUpdate()
    {
        newPosRight = rightHand.transform.position;
        rightHandVelocity = (newPosRight - prevPosRight) / Time.fixedDeltaTime;
        prevPosRight =  newPosRight;

        newPosLeft = leftHand.transform.position;
        leftHandVelocity = (newPosLeft - prevPosLeft) / Time.fixedDeltaTime;
        prevPosLeft = newPosLeft;
    }

    void Update()
    {
        Debug.Log(Vector3.Distance(rightHand.transform.position, leftHand.transform.position));
        if (rightHand.IsTracked && leftHand.IsTracked)
        {
            if (!clapInvoked && Vector3.Distance(rightHand.transform.position, leftHand.transform.position) <= thresholdDistance
                && rightHandVelocity.x < -0.15f && leftHandVelocity.x > 0.15f)
            {
                // onClap.Invoke();
                clapSound.Play();
                // Instantiate(clapFeedback, rightHand.transform.position, Quaternion.identity);
                clapInvoked = true;
                Debug.Log("Clap!");
            }

            if (Vector3.Distance(rightHand.transform.position, leftHand.transform.position) > thresholdDistance * 2)
            {
                clapInvoked = false;
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Presenter : MonoBehaviour
{
    [SerializeField]
    Transform cameraFollow;

    [SerializeField]
    MainCamera spectatorCamera;

    private Animator cameraBackdropCanvas;

    private void Start()
    {
        // Find object named "BackdropCanvas" in the hierarchy
        cameraBackdropCanvas = GameObject.Find("Camera Backdrop Canvas").GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        // follow player by default, in fixed update to be called first, may be overridden
        //UpdateCameraFollow(transform);
        cameraFollow = transform;
    }

    public void UpdateCameraFollow(Transform target)
    {
        cameraFollow = target;

        // if target is camera, switch out of 2D mode
        /*if (cameraFollow == transform && spectatorCamera.Is2D)
        {
            spectatorCamera.SwitchFOV(false);
        }*/
    }

    void LateUpdate()
    {
        if (cameraFollow == transform && spectatorCamera.Is2D)
        {
            spectatorCamera.SwitchFOV(false);
        }
    }
    public Transform GetCameraFollow()
    {
        return cameraFollow;
    }

    public void StartFireworks()
    {
        if (cameraFollow != transform)
        {
            cameraFollow.GetComponent<Screen>().PlayFireworks();
        }
    }

    public void NextSlide()
    {
        cameraFollow.GetComponent<Screen>().NextSlide();
        cameraBackdropCanvas.SetTrigger("NextSlide");
    }

    public void PreviousSlide()
    {
        cameraFollow.GetComponent<Screen>().PreviousSlide();
        cameraBackdropCanvas.SetTrigger("PreviousSlide");
    }
}

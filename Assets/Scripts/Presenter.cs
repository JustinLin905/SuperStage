using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Presenter : MonoBehaviour
{
    [SerializeField]
    Transform cameraFollow;

    [SerializeField]
    MainCamera spectatorCamera;

    void Start()
    {
        
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

    void LateUpdate() {
        if (cameraFollow == transform && spectatorCamera.Is2D)
        {
            spectatorCamera.SwitchFOV(false);
        }
    }
    public Transform GetCameraFollow()
    {
        return cameraFollow;
    }

}

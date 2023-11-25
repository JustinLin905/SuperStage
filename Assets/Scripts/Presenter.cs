using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Presenter : MonoBehaviour
{
    [SerializeField]
    Transform cameraFollow;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        // follow player by default, in fixed update to be called first, may be overridden
        cameraFollow = transform;
    }

    public void UpdateCameraFollow(Transform target)
    {
        cameraFollow = target;
    }

    public Transform GetCameraFollow()
    {
        return cameraFollow;
    }

}

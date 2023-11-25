using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;

public class FingerGun : MonoBehaviour
{

    [SerializeField]
    List<ActiveStateSelector> poses;

    [SerializeField]
    Transform fingertip;

    [SerializeField]
    float rayLength = 100f;
    float maxRayLength = 100f;

    [SerializeField]
    GameObject hitMarker;

    public LayerMask layer; // Specify the layer to ignore

    [SerializeField]
    PoseDetector poseDetector;

    Ray ray;
    RaycastHit hit;


    void Update()
    {
        if (poseDetector.curPose == "FingerGunRight")
        {
            ray = new Ray(fingertip.position, fingertip.right);

            rayLength = maxRayLength;
            if (Physics.Raycast(ray, out hit, maxRayLength, layer))
            {
                rayLength = Vector3.Magnitude(ray.origin - hit.point);
            }

            hitMarker.transform.position = fingertip.position + fingertip.right * rayLength;
            hitMarker.SetActive(rayLength < maxRayLength);
        } 
        else 
        {
            hitMarker.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        if (poseDetector.curPose == "FingerGunRight")
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(fingertip.position, fingertip.position + fingertip.right * rayLength);
        }
        
    }
}

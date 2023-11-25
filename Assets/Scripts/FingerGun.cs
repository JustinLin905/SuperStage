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


    void Update()
    {
    }

    private void OnDrawGizmos()
    {
        Ray ray = new Ray(fingertip.position, fingertip.right);
        RaycastHit hit;

        rayLength = maxRayLength;
        if (Physics.Raycast(ray, out hit, maxRayLength, layer))
        {
            // Log information about the hit
            Debug.Log("Hit object: " + hit.collider.gameObject.name);
            Debug.Log("Hit point: " + hit.point);
            Debug.Log("Hit normal: " + hit.normal);

            rayLength = Vector3.Magnitude(ray.origin - hit.point);
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(fingertip.position, fingertip.position + fingertip.right * rayLength);
        
        hitMarker.transform.position = fingertip.position + fingertip.right * rayLength;
        hitMarker.SetActive(rayLength < maxRayLength);

    }
}

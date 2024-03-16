using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField]
    private GameObject centerEyeAnchor;

    // Store the original global position of the presenter
    private Vector3 originalGlobalPosition;
    // Store the original local position of the Center Eye Anchor
    private Vector3 originalLocalPositionCenterEyeAnchor;
    // Store the original local rotation of centerEyeAnchor
    private Quaternion originalLocalRotation;

    void Start()
    {
        // Store the original global position of the presenter
        originalGlobalPosition = transform.position;
        // Store the original local position of the Center Eye Anchor
        originalLocalPositionCenterEyeAnchor = centerEyeAnchor.transform.localPosition;
    }

    public void ResetPresenterPosition()
    {
        // Calculate the offset needed to return the Center Eye Anchor to its original local position
        Vector3 currentLocalPosition = centerEyeAnchor.transform.localPosition;
        Vector3 offsetToLocalize = originalLocalPositionCenterEyeAnchor - currentLocalPosition;

        // Apply this offset in the global space of the presenter to ensure it moves back to original position
        Vector3 globalOffset = transform.TransformDirection(offsetToLocalize);
        transform.position = originalGlobalPosition + globalOffset;

        // Reset Y rotation
        // ResetYRotation();
        // RotateY(18);

        Debug.Log("Teleporting to original position: " + transform.position + ", with Y rotation reset.");
    }

    private void ResetYRotation()
    {
        Quaternion currentLocalRotation = centerEyeAnchor.transform.localRotation;
        Quaternion rotationDifference = Quaternion.Inverse(currentLocalRotation);

        Vector3 eulerDifference = rotationDifference.eulerAngles;
        eulerDifference.x = 0;
        eulerDifference.y = NormalizeAngle(eulerDifference.y);
        eulerDifference.z = 0;

        transform.Rotate(0, eulerDifference.y, 0, Space.World);
    }

    public void RotateY(float angleDegrees)
    {
        transform.Rotate(0, angleDegrees, 0, Space.World);
    }

    private float NormalizeAngle(float angleDegrees)
    {
        while (angleDegrees > 180) angleDegrees -= 360;
        while (angleDegrees < -180) angleDegrees += 360;
        return angleDegrees;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystem : MonoBehaviour
{
    private bool rotate = true;
    private float rotationSpeed = 25f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (rotate) {
            // Rotate in the y axis
            transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
        }
    }

    public void StopRotation() {
        rotate = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{

    [SerializeField]
    Presenter presenter;

    [SerializeField]
    float baseRotSpeed = 0.7f;

    Camera camera;

    int fov2D = 23;
    int fov3D = 35;

    [SerializeField]
    float fovSpeed = 2.5f;

    void Start()
    {
        camera = GetComponent<Camera>();
        SwitchFOV(false);
    }


    bool temp = false;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            temp = !temp;
            SwitchFOV(temp);
        }
        
        RotateFollow(presenter.GetCameraFollow());
    }

    void RotateFollow(Transform target)
    {
        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);

        float rotDiff = Vector3.Angle((target.position - transform.position), transform.forward); // rotational diff between player and camera point angle
        float followRotSpeed = Mathf.Max(0.4f, rotDiff * baseRotSpeed); //min 0.4f rotational speed

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, followRotSpeed * Time.deltaTime);
    }

    void SwitchFOV(bool is2D)
    {
        if (presenter.GetCameraFollow() != presenter.transform) // only zoom in if focused on specific screen
        {
            if (is2D)
            {
                StartCoroutine(FovTransition(fov3D, fov2D));
                //camera.cullingMask = camera.cullingMask & ~(1 << 6);
            }
            else
            {
                StartCoroutine(FovTransition(fov2D, fov3D));
                camera.cullingMask = camera.cullingMask | (1 << 6);

            }
        }
    }

    IEnumerator FovTransition(int from, int to)
    {
        float t = 0f;
        while(t < 1f)
        {
            t += fovSpeed * Time.deltaTime;
            camera.fieldOfView = Mathf.SmoothStep(from, to, t); // follows Hermite interpolation curve - smooth zoom in/out
            yield return null;
        }

        if (to == fov2D)
        {
            camera.cullingMask = camera.cullingMask & ~(1 << 6);
        } else
        {
            //camera.cullingMask = camera.cullingMask | (1 << 6);
        }
    }

}

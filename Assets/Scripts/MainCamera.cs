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


    public bool Is2D = false;
    int fov2D = 17;
    int fov3D = 33;

    [SerializeField]
    float fovSpeed = 2.5f;

    void Start()
    {
        camera = GetComponent<Camera>();
        SwitchFOV(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SwitchFOV(!Is2D);
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

    public void SwitchFOV(bool is2D)
    {
        if (is2D == Is2D) return; // no need to switch FOV if already in the desired mode

        if (is2D)
        {
            if (presenter.GetCameraFollow() != presenter.transform)
            {
                Is2D = true;
                StartCoroutine(FovTransition(fov3D, fov2D));
                //camera.cullingMask = camera.cullingMask & ~(1 << 6);
            }
        }
        else
        {
            Is2D = false;
            StartCoroutine(FovTransition(fov2D, fov3D));
            camera.cullingMask = camera.cullingMask | (1 << 6);
            camera.cullingMask = camera.cullingMask | (1 << 8);
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
            camera.cullingMask = camera.cullingMask & ~(1 << 8);

        } else
        {
            //camera.cullingMask = camera.cullingMask | (1 << 6);
            //camera.cullingMask = camera.cullingMask | (1 << 8);

        }
    }

}

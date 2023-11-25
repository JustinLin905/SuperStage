using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{

    [SerializeField]
    Presenter presenter;

    [SerializeField]
    float baseRotSpeed = 0.7f;

    void Start()
    {

    }

    void Update()
    {
        RotateFollow(presenter.GetCameraFollow());
    }

    void RotateFollow(Transform target)
    {
        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);

        float rotDiff = Vector3.Angle((target.position - transform.position), transform.forward); // rotational diff between player and camera point angle
        float followRotSpeed = Mathf.Max(0.2f, rotDiff * baseRotSpeed); //min 0.2f rotational speed

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, followRotSpeed * Time.deltaTime);
    }
}

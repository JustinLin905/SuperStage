using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField]
    Transform presenter;

    [SerializeField]
    float baseRotSpeed = 0.7f;

    void Start()
    {
        
    }

    void Update()
    {
        if (presenter) {


            RotateFollowPresenter();

        }
    }

    void RotateFollowPresenter()
    {
        Quaternion targetRotation = Quaternion.LookRotation(presenter.position - transform.position);

        float rotDiff = Vector3.Angle((presenter.position - transform.position), transform.forward); // rotational diff between player and camera point angle
        float followRotSpeed = Mathf.Max(0.2f, rotDiff * baseRotSpeed); //min 0.2f rotational speed

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, followRotSpeed * Time.deltaTime);
    }
}

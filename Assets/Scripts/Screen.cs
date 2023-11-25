using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screen : MonoBehaviour
{

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Presenter"))
        {
            Presenter presenter = other.gameObject.GetComponent<Presenter>();
            if (presenter)
            {
                presenter.UpdateCameraFollow(transform);
            }
        }        
    }

}

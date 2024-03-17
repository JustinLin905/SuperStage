using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screen : MonoBehaviour
{

    [SerializeField]
    GameObject fireworks;
    [SerializeField]
    Slideshow slideshow;
    // Canvas which sits behind the slide, used to display arrows when hand gestures used to change slides
    [SerializeField]
    Animator backdropCanvas;

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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward*(-5f));
    }

    public void PlayFireworks() {
        Debug.Log("Playing fireworks");
        fireworks.GetComponent<ParticleSystem>().Play();
    }
    
    public void NextSlide() {
        slideshow.NextSlide();
        backdropCanvas.SetTrigger("NextSlide");
    }

    public void PreviousSlide() {
        Debug.Log("Screen.cs: PreviousSlide()");
        slideshow.PreviousSlide();
        backdropCanvas.SetTrigger("PreviousSlide");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceEnvironment : MonoBehaviour
{
    [SerializeField]
    Material mat;

    [SerializeField]
    float fadeSpeed = 1f;

    [SerializeField]
    float scaleSpeed = 2f;

    [SerializeField]
    GameObject earthMoonObj;

    bool curState = false;

    void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
    }

    public void TriggerSpaceEnvironemnt(bool shouldAppear) {
        if (shouldAppear != curState) {
            curState = shouldAppear;
            StartCoroutine(EnvironmentTransition(shouldAppear));
            StartCoroutine(SolarSystemTransition(shouldAppear));
        }

    }

    IEnumerator EnvironmentTransition(bool shouldAppear)
    {
        float t = 0f;
        float from = shouldAppear ? 0 : 1f;
        float to = shouldAppear ? 1f : 0;

        while(t < 1f)
        {
            t += fadeSpeed * Time.deltaTime;

            Color col = mat.color;
            col.a = Mathf.SmoothStep(from, to, t);
            mat.color = col;

            yield return null;
        }
    }

    IEnumerator SolarSystemTransition(bool shouldAppear)
    {
        float t = 0f;
        float from = shouldAppear ? 0 : 1f;
        float to = shouldAppear ? 1f : 0;

        if (shouldAppear) {
            earthMoonObj.SetActive(true);
        }

        while(t < 1f)
        {
            t += scaleSpeed * Time.deltaTime;

            earthMoonObj.transform.localScale = Vector3.Lerp(
                new Vector3(from, from, from),
                new Vector3(to, to, to),
                Mathf.Clamp(t*1.5f, 0f, 1f));

            yield return null;
        }

        if (!shouldAppear) {
            earthMoonObj.SetActive(false);
        }
    }

}

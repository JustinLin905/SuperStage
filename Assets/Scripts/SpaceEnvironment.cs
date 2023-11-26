using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceEnvironment : MonoBehaviour
{
    [SerializeField]
    Material mat;

    [SerializeField]
    float fadeSpeed = 1f;

    bool curState = false;

    void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
    }

    public void TriggerSpaceEnvironemnt(bool shouldAppear) {
        if (shouldAppear != curState) {
            curState = shouldAppear;
            StartCoroutine(EnvironmentTransition(shouldAppear));
        }

    }

    IEnumerator EnvironmentTransition(bool shouldAppear)
    {
        
        float t = 0f;
        int from = shouldAppear ? 0 : 1;
        int to = shouldAppear ? 1 : 0;

        while(t < 1f)
        {
            t += fadeSpeed * Time.deltaTime;

            Color col = mat.color;
            col.a = Mathf.SmoothStep(from, to, t);
            mat.color = col;
            yield return null;
        }
    }

}

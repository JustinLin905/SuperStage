using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brayden : MonoBehaviour
{
    private bool curState = false;

    public void TriggerBrayden(bool shouldAppear)
    {
        if (shouldAppear != curState)
        {
            curState = shouldAppear;
            gameObject.GetComponent<Animator>().SetTrigger("BraydenAppear");
        }
    }
}

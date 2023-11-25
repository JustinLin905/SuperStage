using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;

public class PoseDetector : MonoBehaviour
{
    [SerializeField]
    List<ActiveStateSelector> poses;

    [SerializeField]
    GameObject oculusCursor;

    public string curPose = "";

    void Start()
    {
        foreach(var pose in poses)
        {
            pose.WhenSelected += () => PoseUpdated(pose.gameObject.name);
            pose.WhenUnselected += () => PoseUpdated("");
        }
    }

    void PoseUpdated(string poseName)
    {
        curPose = poseName;
        //text.text = poseName;
        Debug.Log("POSE UPDATED: " + poseName);

        if(poseName == "FingerGunRight")
        {
            oculusCursor.SetActive(false);
        }
        else
        {
            oculusCursor.SetActive(true);
        }
    }
}

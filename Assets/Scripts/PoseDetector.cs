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

    [SerializeField]
    MainCamera spectatorCamera;

    [SerializeField]
    Presenter presenter;

    [SerializeField]
    SoundBoard soundBoard;

    [SerializeField]
    Teleport teleport;

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
        Debug.Log("POSE UPDATED: " + poseName);

        if(poseName == "FingerGunRight")
        {
            oculusCursor.SetActive(false);
        }
        else
        {
            oculusCursor.SetActive(true);
        }

        if (poseName == "Grab") {
            spectatorCamera.SwitchFOV(!spectatorCamera.Is2D);
        }

        if (poseName == "ThumbsUp") {
            presenter.StartFireworks();
            teleport.ResetPresenterPosition();
        }

        if (poseName == "FramePoseTwoHanded") {
            // Toggle animator between show and hide
            if (soundBoard.isShowing)
            {
                soundBoard.Hide();
            }
            else
            {
                soundBoard.Show();
            }
        }

        curPose = poseName;
    }
}

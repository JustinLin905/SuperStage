// Model for loading audio files from a folder and storing them in a list for use by ViewModels/Scripts.

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class SoundEffect
{
    public string name;
    public AudioClip clip;

    public SoundEffect(string name, AudioClip clip)
    {
        this.clip = clip;
        this.name = name;
    }
}

public class SoundEffectLoader : MonoBehaviour
{
    private string folderName;
    private string folderPath;

    public static List<SoundEffect> queue { private set; get; } = new List<SoundEffect>();
    public SoundBoard soundBoard;

    // Start is called before the first frame update
    void Start()
    {
        folderName = "SuperStageSoundEffects";
        folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), folderName);

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        StartCoroutine(InitializeAudioFiles());
    }

    private IEnumerator InitializeAudioFiles()
    {
        yield return LoadAllAudioFiles();
        soundBoard.Initialize();
    }

    private IEnumerator LoadAllAudioFiles()
    {
        Debug.Log("LOADING AUDIO FILES FROM: " + folderPath);
        string[] fileExtensions = { "*.wav", "*.ogg", "*.mp3" };

        foreach (string extension in fileExtensions)
        {
            string[] files = Directory.GetFiles(folderPath, extension);
            foreach (string file in files)
            {
                yield return StartCoroutine(LoadAudioClip(file));
            }
        }

        // Log names of all audio clips
        foreach (SoundEffect clip in queue)
        {
            Debug.Log("Loaded Sound Effect name: " + clip.name);
        }
    }

    IEnumerator LoadAudioClip(string filePath)
    {
        string url = "file://" + filePath;
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, GetAudioType(filePath)))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                AudioClip clip = DownloadHandlerAudioClip.GetContent(www);
                string fileName = Path.GetFileNameWithoutExtension(filePath); // Get the file name without extension
                queue.Add(new SoundEffect(fileName, clip));
                Debug.Log("SUCCESSFULLY LOADED: " + filePath);
            }
            else
            {
                Debug.LogError(www.error);
            }
        }
    }

    private AudioType GetAudioType(string filePath)
    {
        string extension = Path.GetExtension(filePath).ToLower();
        switch (extension)
        {
            case ".wav":
                return AudioType.WAV;
            case ".ogg":
                return AudioType.OGGVORBIS;
            case ".mp3":
                return AudioType.MPEG;
            default:
                Debug.LogError("Unsupported audio type: " + extension);
                return AudioType.UNKNOWN;
        }
    }
}
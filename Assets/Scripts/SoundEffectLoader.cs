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
    public int slideNumber;

    public SoundEffect(string name, AudioClip clip)
    {
        this.clip = clip;
        
        // Extract slide number from name
        // The name is formatted like "Sound01 SoundEffectName"
        string[] splitName = name.Split(' ');
        this.name = splitName.Length > 1 ? splitName[1] : "Untitled";
        this.slideNumber = int.Parse(splitName[0].Substring(5)); 
    }
}

public class SoundEffectLoader : MonoBehaviour
{
    private string folderName;
    private string folderPath;

    public static List<SoundEffect> queue { private set; get; } = new List<SoundEffect>();

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
            Debug.Log("Loaded Sound Effect name: " + clip.name + " for slide " + clip.slideNumber);
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
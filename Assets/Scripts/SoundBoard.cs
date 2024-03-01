// ViewModel used to spawn Sound Effect Containers

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBoard : MonoBehaviour
{
    [SerializeField]
    private GameObject soundEffectContainerPrefab;
    [SerializeField]
    private GameObject scrollViewContent;
    [SerializeField]
    private Animator animator;
    public static GameObject universalAudioSource { get; private set; }
    public bool isShowing { get; private set; }

    void Start()
    {
        // Find GameObject in Scene named "Universal Audiosource"
        universalAudioSource = GameObject.Find("Universal Audiosource");
    }

    public void OnPressStop()
    {
        // Stop all sounds from universalAudioSource
        universalAudioSource.GetComponent<AudioSource>().Stop();
    }

    public void Initialize()
    {
        // Generate buttons for each sound effect
        for (int i = 0; i < SoundEffectLoader.queue.Count; i++)
        {
            // Instantiate a sound effect container prefab under scrollViewContent
            GameObject soundEffectContainer = Instantiate(soundEffectContainerPrefab, scrollViewContent.transform);

            // Call the Instantiate method on the soundEffectContainerPrefab
            soundEffectContainer.GetComponent<SoundEffectContainer>().Instantiate(SoundEffectLoader.queue[i].name, i);
        }

        isShowing = true;
    }

    public void Show()
    {
        animator.SetTrigger("Show");
        isShowing = true;
    }

    public void Hide()
    {
        animator.SetTrigger("Hide");
        isShowing = false;
    }
}

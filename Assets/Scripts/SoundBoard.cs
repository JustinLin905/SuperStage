using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBoard : MonoBehaviour
{
    private GameObject universalAudioSource;
    private List<SoundEffect> soundEffects;

    // Start is called before the first frame update
    void Start()
    {
        // Find GameObject in Scene named "Universal Audiosource"
        universalAudioSource = GameObject.Find("Universal Audiosource");

        // Get the relevant sound clips for this slide number from SoundEffectLoader
        soundEffects = SoundEffectLoader.queue.FindAll(se => se.slideNumber == Slideshow.currentItem);
        Debug.Log("SoundEffect count for slide " + Slideshow.currentItem + ": " + soundEffects.Count);

        // Log the names of the sound effects
        foreach (SoundEffect se in soundEffects)
        {
            Debug.Log(se.name);
        }
    }

    public void OnPressPlaySoundEffect(int index)
    {
        // Play the sound effect at the given index
        universalAudioSource.GetComponent<AudioSource>().PlayOneShot(SoundEffectLoader.queue[index].clip);
    }
}

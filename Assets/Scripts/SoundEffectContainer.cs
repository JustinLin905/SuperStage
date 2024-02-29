using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SoundEffectContainer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI soundEffectName;
    [SerializeField]
    private Button playButton;
    private int soundIndex;
    private List<SoundEffect> soundEffects;

    public void OnPressPlaySoundEffect(int index)
    {
        // Play the sound effect at the given index
        SoundBoard.universalAudioSource.GetComponent<AudioSource>().PlayOneShot(SoundEffectLoader.queue[index].clip);
    }

    public void Instantiate(string name, int index)
    {
        // Get the text component of the sound effect container
        soundEffectName.text = name;

        // Set the index of the sound effect
        soundIndex = index;

        // Add the OnClick event to the play button
        playButton.onClick.AddListener(() => OnPressPlaySoundEffect(index));
    }
}

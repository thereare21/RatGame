using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlayRatKill(AudioSource original) {

        // Check if the original AudioSource is null
        if (original == null)
        {
            Debug.LogError("Original AudioSource is null.");
        }

        // Create a new GameObject to hold the duplicated AudioSource
        GameObject newGameObject = new GameObject("DuplicatedAudioSource");
        GameObject soundManager = GameObject.FindGameObjectWithTag("SoundManager");

        newGameObject.transform.SetParent(soundManager.transform);
        AudioSource newAudioSource = newGameObject.AddComponent<AudioSource>();

        // Copy properties from the original AudioSource to the new one
        newAudioSource.clip = original.clip;

        int numSoundsPlaying = soundManager.transform.childCount;

        if (numSoundsPlaying > 10)
        {
            newAudioSource.volume = 0f;
        } else
        {
            newAudioSource.volume = Mathf.Max(1f - 0.1f * numSoundsPlaying, 0.1f);

        }

        newAudioSource.pitch = original.pitch;
        newAudioSource.loop = original.loop;
        newAudioSource.spatialBlend = original.spatialBlend;
        newAudioSource.panStereo = original.panStereo;

        // You can copy other properties as needed

        // Start playing the duplicated AudioSource
        newAudioSource.Play();

        Destroy(newGameObject, 0.2f);

    }

}

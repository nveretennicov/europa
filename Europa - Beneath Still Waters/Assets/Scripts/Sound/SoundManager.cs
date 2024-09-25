using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static void PlaySound(AudioClip audioClip, Vector3 position, float pitchVariation = 0f, float volume = 1f)
    {
        GameObject soundGameObject = new GameObject("Sound");
        soundGameObject.transform.position = position;
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = audioClip;

        // Changing sound properties        
        audioSource.pitch += Random.Range(-pitchVariation, pitchVariation);
        audioSource.volume = volume;
        
        // Play clip and queue its destruction
        audioSource.Play();
        Destroy(soundGameObject, audioClip.length);
    }
}
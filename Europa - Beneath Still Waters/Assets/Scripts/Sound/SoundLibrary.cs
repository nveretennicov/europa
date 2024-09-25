using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundLibrary : MonoBehaviour
{
    public static SoundLibrary Singleton;


    // Sounds
    public AudioClip metalFootstep;

    private void Start()
    {
        Singleton = this;
    }
}

using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public string name;

    [HideInInspector]
    public AudioSource source;
    
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    [Range(.3f, 1f)]
    public float pitch;
}

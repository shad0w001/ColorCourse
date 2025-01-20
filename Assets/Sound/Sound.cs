using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)] // adds a slider
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;
    [Range(0f, 1f)]
    public float spatialblend;

    public bool playOnAwake;

    [HideInInspector]
    public AudioSource source;

    public bool loop;
}

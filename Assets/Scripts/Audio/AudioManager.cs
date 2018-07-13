using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private List<SFX> sfxs;

    private AudioSource audioSource;

    public static AudioManager instance;

    private void OnEnable()
    {
        if (instance == null)
            instance = this;
    }

    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnDisable()
    {
        if (instance == this)
            instance = null;
    }

    public void PlayOneShot(string clipName)
    {
        var sfx = sfxs.Find(s => s.name == clipName);
        if (sfx != null)
        {
            audioSource.volume = sfx.volume;
            audioSource.pitch = sfx.pitch;
            audioSource.clip = sfx.clip;
            audioSource.Play();
        }
    }
}

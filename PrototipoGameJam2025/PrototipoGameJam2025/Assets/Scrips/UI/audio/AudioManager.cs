using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    [SerializeField] private AudioSource AudioSource;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetVolume(float volume)
    {
        if (AudioSource != null)
        {
            AudioSource.volume = volume;
        }
    }

    public float GetVolume()
    {
        return AudioSource != null ? AudioSource.volume : 0f;
    }


}

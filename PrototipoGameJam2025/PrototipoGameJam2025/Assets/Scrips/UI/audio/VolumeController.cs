using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private AudioSource AudioManager;

    private void Start()
    {
        volumeSlider.onValueChanged.AddListener((v) => {
            AudioManager.volume = v;
            Debug.Log("AudioManager.volume" + v);
            VolumeManager.Instance.SaveVolume(v); // Cambia el volumen a 0.5 y lo guarda en JSON.
            float currentVolume = VolumeManager.Instance.GlobalVolume; // Obtén el volumen actual.
            VolumeManager.Instance.ApplyGlobalVolume();
        });

    }
}
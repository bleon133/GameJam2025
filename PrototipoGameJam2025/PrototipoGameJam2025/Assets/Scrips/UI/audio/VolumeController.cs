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
            Debug.Log(AudioManager.volume);
        Debug.Log(v);


        });

    }
}

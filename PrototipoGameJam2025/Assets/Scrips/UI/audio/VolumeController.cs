using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Conecta un Slider con el volumen global de la música (AudioManager).
/// Se puede usar tanto en el menú como en la configuración del nivel.
/// </summary>
public class VolumeController : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;

    private void OnEnable()
    {
        if (volumeSlider == null) volumeSlider = GetComponent<Slider>();
        if (volumeSlider == null) return;

        // Muestra el volumen ya elegido sin disparar el cambio.
        volumeSlider.SetValueWithoutNotify(AudioManager.Volume);
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
    }

    private void OnDisable()
    {
        if (volumeSlider != null) volumeSlider.onValueChanged.RemoveListener(OnVolumeChanged);
    }

    private void OnVolumeChanged(float value)
    {
        AudioManager.ApplyVolume(value);
    }
}

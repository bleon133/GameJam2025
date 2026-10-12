using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Administra el volumen de la música.
///
/// El volumen es un valor global (estático, guardado en PlayerPrefs) que se
/// aplica a la música de la escena activa (objeto MusicaFondo) y al AudioSource
/// de este objeto.
///
/// El objeto sobrevive a los cambios de escena. Su AudioSource lleva la música
/// del menú: suena solo en la escena del menú y se detiene en las demás, donde
/// suena la música propia de cada escena.
/// </summary>
public class AudioManager : MonoBehaviour
{
    private const string VolumePrefsKey = "music_volume";
    private const string SceneMusicObjectName = "MusicaFondo";

    private static AudioManager instance;
    private static float volume = 1f;
    private static bool volumeLoaded;

    [SerializeField] private AudioSource AudioSource;
    [SerializeField] private string menuSceneName = "Menu";

    public static AudioManager Instance { get { return instance; } }

    /// <summary>Volumen actual de la música (0 a 1).</summary>
    public static float Volume
    {
        get
        {
            if (!volumeLoaded)
            {
                volume = Mathf.Clamp01(PlayerPrefs.GetFloat(VolumePrefsKey, 1f));
                volumeLoaded = true;
            }
            return volume;
        }
    }

    /// <summary>Guarda el volumen y lo aplica a la música de la escena activa.</summary>
    public static void ApplyVolume(float newVolume)
    {
        volume = Mathf.Clamp01(newVolume);
        volumeLoaded = true;
        PlayerPrefs.SetFloat(VolumePrefsKey, volume);

        if (instance != null && instance.AudioSource != null)
        {
            instance.AudioSource.volume = volume;
        }

        ApplyVolumeToSceneMusic();
    }

    // Se engancha a la carga de escenas aunque no exista un AudioManager
    // (por ejemplo, si se abre el nivel directamente).
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Initialize()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnAnySceneLoaded;
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnAnySceneLoaded;
        ApplyVolumeToSceneMusic();
    }

    private static void OnAnySceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ApplyVolumeToSceneMusic();
    }

    private static void ApplyVolumeToSceneMusic()
    {
        GameObject sceneMusic = GameObject.Find(SceneMusicObjectName);
        if (sceneMusic == null)
        {
            return;
        }

        AudioSource source = sceneMusic.GetComponent<AudioSource>();
        if (source != null)
        {
            source.volume = Volume;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        if (AudioSource != null)
        {
            AudioSource.volume = Volume;
        }
    }

    private void OnEnable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Un duplicado (al volver al menú) se destruye; solo la instancia activa decide.
        if (instance != this || AudioSource == null)
        {
            return;
        }

        if (scene.name == menuSceneName)
        {
            if (!AudioSource.isPlaying)
            {
                AudioSource.Play();
            }
        }
        else if (AudioSource.isPlaying)
        {
            AudioSource.Stop();
        }
    }

    public void SetVolume(float newVolume)
    {
        ApplyVolume(newVolume);
    }

    public float GetVolume()
    {
        return Volume;
    }
}

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VolumeManager : MonoBehaviour
{
    public static VolumeManager Instance { get; private set; } // Singleton global

    public float GlobalVolume { get; private set; } = 1.0f; // Volumen global inicial
    private string variablesFilePath;

    private void Awake()
    {
        // Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Carga el volumen desde variables.json
        variablesFilePath = Path.Combine(Application.streamingAssetsPath, "variables.json");
        LoadVolume();
    }

    /// <summary>
    /// Carga el volumen desde el archivo JSON.
    /// </summary>
    private void LoadVolume()
    {
        if (File.Exists(variablesFilePath))
        {
            string jsonContent = File.ReadAllText(variablesFilePath);
            VariablesData variables = JsonUtility.FromJson<VariablesData>(jsonContent);
            GlobalVolume = variables.volume;
        }
        else
        {
            Debug.LogWarning("Archivo de configuración no encontrado. Usando volumen predeterminado.");
        }
    }

    /// <summary>
    /// Guarda el volumen en el archivo JSON.
    /// </summary>
    public void SaveVolume(float volume)
    {
        GlobalVolume = volume;

        VariablesData variables = new VariablesData
        {
            volume = GlobalVolume,
            language = "es" // Mantén el idioma si es necesario
        };

        string jsonContent = JsonUtility.ToJson(variables, true);
        File.WriteAllText(variablesFilePath, jsonContent);
    }

    /// <summary>
    /// Aplica el volumen global a todos los audios en la escena.
    /// </summary>
    public void ApplyGlobalVolume()
    {
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource source in allAudioSources)
        {
            source.volume = GlobalVolume;
        }
        //UnityEngine.SceneManagement.SceneManager.sceneLoaded
    }

    [System.Serializable]
    private class VariablesData
    {
        public float volume;
        public string language;
    }
}
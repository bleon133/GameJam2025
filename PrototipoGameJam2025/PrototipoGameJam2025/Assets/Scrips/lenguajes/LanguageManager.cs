using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LanguageManager : MonoBehaviour
{
    public static LanguageManager Instance; // Singleton para acceder desde otros scripts.

    public Text titleText; // Referencia al texto del título.
    public Text startButtonText; // Referencia al texto del botón "Comenzar".
    public Text exitButtonText; // Referencia al texto del botón "Salir".

    private Dictionary<string, string> localizedTexts; // Diccionario para almacenar los textos traducidos.
    private string currentLanguage = "es"; // Idioma por defecto.

    private void Awake()
    {
        // Implementamos el patrón Singleton.
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Evita que este objeto se destruya al cargar nuevas escenas.
        }
        else
        {
            Destroy(gameObject);
        }

        LoadLanguage(currentLanguage); // Cargar idioma por defecto.
    }

    // Método para cargar el idioma desde un archivo JSON.
    public void LoadLanguage(string languageCode)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, $"{languageCode}-lang.json");

        if (File.Exists(filePath))
        {
            string jsonContent = File.ReadAllText(filePath);
            localizedTexts = JsonUtility.FromJson<LocalizationData>(jsonContent).ToDictionary();

            ApplyLocalization(); // Actualiza los textos en la UI.
        }
        else
        {
            Debug.LogError($"Archivo de idioma no encontrado: {filePath}");
        }
    }

    // Aplica los textos localizados a los elementos de la UI.
    private void ApplyLocalization()
    {
        if (localizedTexts != null)
        {
            titleText.text = GetLocalizedText("title");
            startButtonText.text = GetLocalizedText("startButton");
            exitButtonText.text = GetLocalizedText("exitButton");
        }
    }

    // Devuelve el texto localizado para una clave específica.
    public string GetLocalizedText(string key)
    {
        if (localizedTexts != null && localizedTexts.TryGetValue(key, out string value))
        {
            return value;
        }

        Debug.LogWarning($"Clave de texto no encontrada: {key}");
        return key; // Devuelve la clave como fallback.
    }
}

// Clase auxiliar para deserializar el JSON.
[System.Serializable]
public class LocalizationData
{
    public List<LocalizationItem> items;

    public Dictionary<string, string> ToDictionary()
    {
        Dictionary<string, string> dictionary = new Dictionary<string, string>();
        foreach (var item in items)
        {
            dictionary[item.key] = item.value;
        }
        return dictionary;
    }
}

[System.Serializable]
public class LocalizationItem
{
    public string key;
    public string value;
}

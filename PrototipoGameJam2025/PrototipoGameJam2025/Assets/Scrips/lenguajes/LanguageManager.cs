using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LanguageManager : MonoBehaviour
{
    public static LanguageManager Instance; // Singleton para acceder desde otros scripts.

    public TextMeshProUGUI configbtn; // Referencia al texto del título.
    public TextMeshProUGUI startButtonText; // Referencia al texto del botón "Comenzar".
    public TextMeshProUGUI exitButtonText; // Referencia al texto del botón "Salir".
    public TextMeshProUGUI idiomatxt; // Referencia al texto del botón "idioma".
    public TextMeshProUGUI volumentxt; // Referencia al texto del botón "volumen".
    public TextMeshProUGUI tutorialBtn; // Referencia al texto del botón "tutotial".
    public TextMeshProUGUI goBackBtn; // Referencia al texto del botón "volver".
    public TextMeshProUGUI scene1; // Referencia al texto del botón "scene1".
    public TextMeshProUGUI scene2; // Referencia al texto del botón "scene2".
    public TextMeshProUGUI scene3; // Referencia al texto del botón "scene3".
    public TextMeshProUGUI scene4; // Referencia al texto del botón "scene4".
    public TextMeshProUGUI scene5; // Referencia al texto del botón "scene5".
    public TextMeshProUGUI reanudarbtn; // Referencia al texto del botón "reanudar".

    public TMP_FontAsset fontES;  // Fuente para español (y similares)
    public TMP_FontAsset fontCRS; // Fuente para coreano
    public TMP_FontAsset fontARAB; // Fuente para árabe

    private Dictionary<string, string> localizedTexts; // Diccionario para almacenar los textos traducidos.
    private string currentLanguage = "es"; // Idioma por defecto.
    private string variablesFilePath; // Ruta al archivo variables.json.

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
            return;
        }

        // Ruta del archivo variables.json.
        variablesFilePath = Path.Combine(Application.streamingAssetsPath, "variables.json");

        LoadVariables(); // Cargar el idioma desde variables.json.
        LoadLanguage(currentLanguage); // Cargar el idioma por defecto.
    }

    private void LoadVariables()
    {
        if (File.Exists(variablesFilePath))
        {
            string jsonContent = File.ReadAllText(variablesFilePath);
            VariablesData variables = JsonUtility.FromJson<VariablesData>(jsonContent);
            currentLanguage = variables.language; // Establecer el idioma desde variables.json.
        }
        else
        {
            Debug.LogWarning($"Archivo variables.json no encontrado. Usando idioma por defecto: {currentLanguage}");
        }
    }

    private void SaveVariables()
    {
        VariablesData variables = new VariablesData
        {
            volume = 100, // Puedes agregar lógica para obtener el volumen dinámicamente si lo necesitas.
            language = currentLanguage
        };

        string jsonContent = JsonUtility.ToJson(variables, true);
        File.WriteAllText(variablesFilePath, jsonContent);
    }

    private void ApplyFont()
    {
        TMP_FontAsset selectedFont = fontES; // Fuente por defecto

        switch (currentLanguage)
        {
            case "es":
                selectedFont = fontES; // Español o similar
                break;
            case "en":
                selectedFont = fontES; // Español o similar
                break;
            case "crs":
                selectedFont = fontCRS; // Coreano
                break;
            case "arab":
                selectedFont = fontARAB; // Árabe
                break;
            default:
                Debug.LogWarning($"Idioma no reconocido: {currentLanguage}, usando fuente por defecto.");
                break;
        }

        // Asigna la fuente a los textos
        configbtn.font = selectedFont;
        startButtonText.font = selectedFont;
        exitButtonText.font = selectedFont;
        configbtn.font = selectedFont;
        startButtonText.font = selectedFont;
        exitButtonText.font = selectedFont;
        idiomatxt.font = selectedFont;
        volumentxt.font = selectedFont;
        tutorialBtn.font = selectedFont;
        goBackBtn.font = selectedFont;
    }

    // Método para cargar el idioma desde un archivo JSON.
    public void LoadLanguage(string languageCode)
    {
        currentLanguage = languageCode; // Actualizar el idioma actual.
        SaveVariables(); // Guardar el idioma en variables.json.

        string filePath = Path.Combine(Application.streamingAssetsPath, $"{languageCode}-lang.json");

        if (File.Exists(filePath))
        {
            string jsonContent = File.ReadAllText(filePath);
            LocalizationData localizationData = JsonUtility.FromJson<LocalizationData>(jsonContent);
            localizedTexts = localizationData.ToDictionary(); // Convierte a diccionario.

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
            configbtn.text = GetLocalizedText("configuration");
            startButtonText.text = GetLocalizedText("startButton");
            exitButtonText.text = GetLocalizedText("exitButton");
            idiomatxt.text = GetLocalizedText("language");
            volumentxt.text = GetLocalizedText("volume");
            tutorialBtn.text = GetLocalizedText("tutorial");
            goBackBtn.text = GetLocalizedText("regresar");

            ApplyFont(); // Cambia la fuente según el idioma
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

// Clase para manejar los datos en variables.json.
[System.Serializable]
public class VariablesData
{
    public int volume; // Volumen del juego.
    public string language; // Idioma actual.
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
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

/// <summary>
/// Gestiona el idioma del juego.
///
/// Hay un LanguageManager en cada escena, con los textos de esa escena
/// asignados en el Inspector. El idioma elegido se guarda en una variable
/// estática y en PlayerPrefs, así que se mantiene al cambiar de escena y
/// entre sesiones.
///
/// Los textos se leen de StreamingAssets/{codigo}-lang.json, con formato de
/// claves planas: { "menu-btn-jugar": "Jugar", ... }
/// </summary>
public class LanguageManager : MonoBehaviour
{
    private const string PrefsKey = "game_language";

    /// <summary>Manager de la escena activa.</summary>
    public static LanguageManager Instance;

    [Header("Textos del menú principal")]
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI startButtonText;
    public TextMeshProUGUI configButtonText;
    public TextMeshProUGUI exitButtonText;

    [Header("Etiquetas y botones de configuración")]
    public TextMeshProUGUI musicLabel;
    public TextMeshProUGUI languageLabel;
    public TextMeshProUGUI tutorialText;
    public TextMeshProUGUI tutorialBodyText;
    public TextMeshProUGUI backButtonText;

    [Header("Menú de pausa")]
    public TextMeshProUGUI pauseResumeText;
    public TextMeshProUGUI pauseConfigText;
    public TextMeshProUGUI pauseExitButtonText;
    public TextMeshProUGUI pauseRestartText;
    public TextMeshProUGUI pauseExitText;

    [Header("Cinemática (un texto por escena)")]
    public TextMeshProUGUI[] cinematicParagraphs = new TextMeshProUGUI[5];

    [Header("Ajustes")]
    [Tooltip("Idioma que se usa la primera vez que se abre el juego.")]
    public string defaultLanguage = "es";

    /// <summary>Idiomas soportados, en el orden del selector.</summary>
    public static readonly string[] SupportedLanguages = { "es", "en" };

    private static Dictionary<string, string> localizedTexts;
    private static string currentLanguage;

    /// <summary>Idioma activo en este momento.</summary>
    public string CurrentLanguage
    {
        get { return string.IsNullOrEmpty(currentLanguage) ? defaultLanguage : currentLanguage; }
    }

    private void Awake()
    {
        Instance = this;

        // Se conserva el idioma ya elegido; si no hay, el guardado o el de por defecto.
        string language = currentLanguage;
        if (string.IsNullOrEmpty(language))
        {
            language = PlayerPrefs.GetString(PrefsKey, defaultLanguage);
        }

        LoadLanguage(language, false);
    }

    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }

    /// <summary>Carga el idioma indicado ("es" o "en") y lo guarda como preferencia.</summary>
    public void LoadLanguage(string languageCode)
    {
        LoadLanguage(languageCode, true);
    }

    private void LoadLanguage(string languageCode, bool save)
    {
        if (string.IsNullOrEmpty(languageCode)) languageCode = defaultLanguage;

        string filePath = Path.Combine(Application.streamingAssetsPath, languageCode + "-lang.json");

        if (File.Exists(filePath))
        {
            localizedTexts = ParseLocalization(File.ReadAllText(filePath));
            currentLanguage = languageCode;

            if (save) PlayerPrefs.SetString(PrefsKey, languageCode);

            ApplyLocalization();
        }
        else
        {
            Debug.LogError("Archivo de idioma no encontrado: " + filePath);

            // Si el idioma guardado ya no existe, se vuelve al de por defecto.
            if (languageCode != defaultLanguage) LoadLanguage(defaultLanguage, save);
        }
    }

    // ------------------------------------------------------------------
    //  Parseo del JSON (JsonUtility no soporta diccionarios)
    // ------------------------------------------------------------------

    private Dictionary<string, string> ParseLocalization(string json)
    {
        var result = new Dictionary<string, string>();

        int i = 0;
        while (i < json.Length)
        {
            int keyStart = json.IndexOf('"', i);
            if (keyStart < 0) break;

            int keyEnd = FindStringEnd(json, keyStart + 1);
            if (keyEnd < 0) break;

            string key = Unescape(json.Substring(keyStart + 1, keyEnd - keyStart - 1));

            int colon = json.IndexOf(':', keyEnd);
            if (colon < 0) break;

            int valueStart = json.IndexOf('"', colon);
            if (valueStart < 0) break;

            int valueEnd = FindStringEnd(json, valueStart + 1);
            if (valueEnd < 0) break;

            result[key] = Unescape(json.Substring(valueStart + 1, valueEnd - valueStart - 1));

            i = valueEnd + 1;
        }

        return result;
    }

    private int FindStringEnd(string s, int from)
    {
        for (int i = from; i < s.Length; i++)
        {
            if (s[i] == '\\') { i++; continue; }
            if (s[i] == '"') return i;
        }
        return -1;
    }

    private string Unescape(string s)
    {
        return s.Replace("\\n", "\n")
                .Replace("\\t", "\t")
                .Replace("\\\"", "\"")
                .Replace("\\\\", "\\");
    }

    // ------------------------------------------------------------------
    //  Aplicación de textos
    // ------------------------------------------------------------------

    /// <summary>Aplica los textos localizados a los elementos asignados.</summary>
    private void ApplyLocalization()
    {
        if (localizedTexts == null) return;

        // Menú principal
        SetIfPresent(startButtonText, "menu-btn-jugar");
        SetIfPresent(configButtonText, "menu-btn-config");
        SetIfPresent(exitButtonText, "menu-btn-salir");

        // Configuración
        SetIfPresent(languageLabel, "menu-label-idioma");
        SetIfPresent(musicLabel, "menu-label-musica");
        SetIfPresent(tutorialText, "config-btn-tutorial");
        SetIfPresent(tutorialBodyText, "tutorial-texto");
        SetIfPresent(backButtonText, "config-btn-regresar");

        // Pausa y fin de partida
        SetIfPresent(pauseResumeText, "play-btn-reanuar");
        SetIfPresent(pauseConfigText, "play-btn-config");
        SetIfPresent(pauseExitButtonText, "play-btn-salir");
        SetIfPresent(pauseRestartText, "play-btn-reiniciar");
        SetIfPresent(pauseExitText, "play-btn-salir");

        // Cinemática
        if (cinematicParagraphs != null)
        {
            for (int i = 0; i < cinematicParagraphs.Length; i++)
                SetIfPresent(cinematicParagraphs[i], "cinematica-parrafo" + (i + 1));
        }
    }

    private void SetIfPresent(TextMeshProUGUI target, string key)
    {
        if (target == null) return;

        string value;
        if (localizedTexts.TryGetValue(key, out value) && !string.IsNullOrEmpty(value))
            target.text = value;
        else
            Debug.LogWarning("Clave de texto no encontrada: " + key);
    }

    /// <summary>Devuelve el texto localizado para una clave.</summary>
    public string GetLocalizedText(string key)
    {
        string value;
        if (localizedTexts != null && localizedTexts.TryGetValue(key, out value))
            return value;

        Debug.LogWarning("Clave de texto no encontrada: " + key);
        return key;
    }
}

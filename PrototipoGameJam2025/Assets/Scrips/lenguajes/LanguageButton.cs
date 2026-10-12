using System;
using TMPro;
using UnityEngine;

/// <summary>
/// Conecta el selector de idioma con el LanguageManager.
/// Se puede usar de dos formas:
///  - Desde un Dropdown: asigna este script al objeto del dropdown y
///    llama a ChangeLanguageByIndex con su índice de opción. Al iniciar,
///    el dropdown se sincroniza con el idioma activo.
///  - Desde botones sueltos: llama a ChangeLanguage("es") o ("en").
/// </summary>
public class LanguageButton : MonoBehaviour
{
    [Tooltip("Si se deja vacío, se busca el LanguageManager de la escena.")]
    public LanguageManager languageManager;

    private void Start()
    {
        ResolveManager();

        // El dropdown se crea con el valor del editor en cada escena; se alinea
        // con el idioma activo sin disparar el cambio de idioma.
        var dropdown = GetComponent<TMP_Dropdown>();
        if (dropdown == null || languageManager == null) return;

        int index = Array.IndexOf(LanguageManager.SupportedLanguages, languageManager.CurrentLanguage);
        if (index >= 0) dropdown.SetValueWithoutNotify(index);
    }

    /// <summary>Cambia el idioma indicando su código: "es" o "en".</summary>
    public void ChangeLanguage(string languageCode)
    {
        ResolveManager();
        if (languageManager != null) languageManager.LoadLanguage(languageCode);
    }

    /// <summary>Cambia el idioma según el índice del dropdown.</summary>
    public void ChangeLanguageByIndex(int index)
    {
        ResolveManager();
        if (languageManager == null) return;

        if (index >= 0 && index < LanguageManager.SupportedLanguages.Length)
            languageManager.LoadLanguage(LanguageManager.SupportedLanguages[index]);
    }

    private void ResolveManager()
    {
        if (languageManager == null) languageManager = LanguageManager.Instance;
        if (languageManager == null) languageManager = FindObjectOfType<LanguageManager>();
    }
}

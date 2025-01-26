using UnityEngine;

public class LanguageButton : MonoBehaviour
{
    public void ChangeLanguage(string languageCode)
    {
        LanguageManager.Instance.LoadLanguage(languageCode);
    }
}
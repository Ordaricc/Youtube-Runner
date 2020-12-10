using System;
using UnityEngine;

public class ChangeLanguageManager : MonoBehaviour
{
    public static Language.Languages currentLanguage;

    public const string prefLanguage = "prefLanguage";

    private void Awake()
    {
        bool isValidString = Enum.TryParse(PlayerPrefs.GetString(prefLanguage), true, out Language.Languages result);
        if (isValidString)
            currentLanguage = result;
    }

    public static void SwitchLanguage()
    {
        int languagesLenght = Enum.GetValues(typeof(Language.Languages)).Length;
        if ((int)currentLanguage + 1 < languagesLenght)
            currentLanguage++;
        else
            currentLanguage = 0;

        UpdateAllTexts();
    }

    private static void UpdateAllTexts()
    {
        NewTextChanger[] textsToUpdate = FindObjectsOfType<NewTextChanger>();
        foreach (var t in textsToUpdate)
        {
            t.UpdateText();
        }
    }

    public static void ChangeLanguageTo(string languageEnumInString, bool saveLanguagePref)
    {
        bool isValidString = Enum.TryParse(languageEnumInString, true, out Language.Languages result);
        if (isValidString)
            ChangeLanguageTo(result, saveLanguagePref);
        else
            Debug.LogWarning("Invalid string format");
    }

    public static void ChangeLanguageTo(Language.Languages languageToChangeTo, bool saveLanguagePref)
    {
        currentLanguage = languageToChangeTo;

        UpdateAllTexts();

        if (saveLanguagePref)
            PlayerPrefs.SetString(prefLanguage, languageToChangeTo.ToString());
    }
}
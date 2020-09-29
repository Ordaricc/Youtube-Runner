using System;
using UnityEngine;

public class NewLanguageManager : MonoBehaviour
{
    public static NewLanguageManager Instance;

    public Language.Languages currentLanguage;
    public Language language;

    private void Awake()
    {
        Instance = this;
    }

    public string GetLine(LineCategoryClass.LineCategory lineCategory, string lineID)
    {
        try
        {
            LineCategoryClass rightCategory = Array.Find(language.categories, dummyCategory => dummyCategory.category == lineCategory);
            LineIDClass rightLineID = Array.Find(rightCategory.lines, dummyLineID => dummyLineID.lineID == lineID);
            Translation rightTranslation = Array.Find(rightLineID.translations, dummyTranslation => dummyTranslation.language == currentLanguage);
            return rightTranslation.line;
        }
        catch (Exception)
        {
            Debug.LogWarning("Missing translation");
            return "placeholder";
        }
    }

    public void SwitchLanguage()
    {
        int languagesLenght = Enum.GetValues(typeof(Language.Languages)).Length;
        if ((int)currentLanguage + 1 < languagesLenght)
            currentLanguage++;
        else
            currentLanguage = 0;

        UpdateAllTexts();
    }

    private void UpdateAllTexts()
    {
        NewTextChanger[] textsToUpdate = FindObjectsOfType<NewTextChanger>();
        foreach (var t in textsToUpdate)
        {
            t.UpdateText();
        }
    }

    public void ChangeLanguageTo(string languageEnumInString)
    {
        bool isValidString = Enum.TryParse(languageEnumInString, true, out Language.Languages result);
        if (isValidString)
            currentLanguage = result;
        else
            Debug.LogWarning("Invalid string format");

        UpdateAllTexts();
    }
}
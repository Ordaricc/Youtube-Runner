using System;
using UnityEngine;

public class NewLanguageManager : MonoBehaviour
{
    public static NewLanguageManager Instance;

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
            Translation rightTranslation = Array.Find(rightLineID.translations, dummyTranslation => dummyTranslation.language == ChangeLanguageManager.currentLanguage);
            return rightTranslation.line;
        }
        catch (Exception)
        {
            Debug.LogWarning("Missing translation");
            return "placeholder";
        }
    }
}
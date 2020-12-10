using UnityEngine;

public class ChooseLanguageManager : MonoBehaviour
{
    [SerializeField] private GameObject confirmButtonGO;
    private Language.Languages currentLanguage;

    public void SelectLanguage(Language.Languages languageSelected)
    {
        currentLanguage = languageSelected;
        ChangeLanguageManager.ChangeLanguageTo(languageSelected, false);
        confirmButtonGO.SetActive(true);
    }

    public void ConfirmLanguage()
    {
        ChangeLanguageManager.ChangeLanguageTo(currentLanguage, true);
        PlayerPrefs.SetInt(MySceneManager.prefHasChosenLanguage, 1);
        SceneFunctions.ChangeSceneByAddingIndex(1);
    }
}
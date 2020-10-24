using UnityEngine;

public class SelectLanguageButton : MonoBehaviour
{
    [SerializeField] private Language.Languages languageToPass;

    public void CallChooseLanguageManager()
    {
        FindObjectOfType<ChooseLanguageManager>().SelectLanguage(languageToPass);
    }
}
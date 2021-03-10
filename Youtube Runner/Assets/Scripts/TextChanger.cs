using UnityEngine;
using TMPro;

public class TextChanger : MonoBehaviour
{
    private TextMeshProUGUI textToChange;

    public LineCategoryClass.LineCategory lineCategory;
    public string lineID;

    private void Awake()
    {
        textToChange = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        if (LanguageManager.Instance != null)
            UpdateText();
        else
            Invoke("UpdateText", 0.0001f);
    }

    public void UpdateText()
    {
        textToChange.text = LanguageManager.Instance.GetLine(lineCategory, lineID);
    }
}
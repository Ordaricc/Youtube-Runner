using UnityEngine;
using TMPro;

public class NewTextChanger : MonoBehaviour
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
        if (NewLanguageManager.Instance != null)
            UpdateText();
        else
            Invoke("UpdateText", 0.0001f);
    }

    public void UpdateText()
    {
        textToChange.text = NewLanguageManager.Instance.GetLine(lineCategory, lineID);
    }
}
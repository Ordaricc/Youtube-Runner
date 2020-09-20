using UnityEngine;
using TMPro;

public class SetTextToCurrentVersion : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<TextMeshProUGUI>().text = "Current version: " + Application.version;
    }
}
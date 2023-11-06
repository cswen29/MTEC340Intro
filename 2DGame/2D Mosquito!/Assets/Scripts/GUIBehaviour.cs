using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GUIBehaviour : MonoBehaviour
{
    public static GUIBehaviour Instance;

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this; //Singleton pattern
    }

    private void ToggleGUIVisibility(TextMeshProUGUI textgui)
    {

        textgui.enabled = !textgui.enabled;
    }

    public void UpdateMessageGUI(TextMeshProUGUI textMeshProUGUI)
    {
        ToggleGUIVisibility(textMeshProUGUI);
    }
}
using TMPro;
using UnityEngine;
using System;

//features:
//vertical screen compatibility
//font changing
//better framing
//on idle, show infographic

public class MainController : MonoBehaviour
{
    public TextMeshProUGUI oldLines;
    public TextMeshProUGUI newLine;
    public TMP_InputField inputField;

    // 0 = zuhui
    // 1 = alex
    bool currentProfile = false;

    [SerializeField] private Color textColor;
    [SerializeField] private Font textFont;
    private string currentColor = "EMPTY";

    private DateTime currentDateTime;

    void Start()
    {
        inputField.Select();
        InstantiateProfile();
    }

    void Update()
    {
        currentDateTime = DateTime.Now;

        if (Input.GetKeyDown(KeyCode.Tab))
            SwitchProfile();
    }

    public void NewLine()
    {
        string currentTime = currentDateTime.TimeOfDay.ToString();
        oldLines.text += "\n (" + currentTime.Substring(0, 8) + ") " + currentColor + inputField.text + "</color>";
        inputField.text = "";
        inputField.Select();
        inputField.ActivateInputField();
    }

    public void InstantiateProfile()
    {
        if (currentProfile)
        {
            textColor = new Color(0, 1, 0);
            currentColor = "<color=#00ff00>";
        }
        else if (!currentProfile)
        {
            textColor = new Color(1, 0, 0);
            currentColor = "<color=#ff0000>";
        }
        newLine.color = textColor;
    }

    public void SwitchProfile()
    {
        currentProfile = !currentProfile;
        if (!string.IsNullOrEmpty(inputField.text))
            NewLine();
        InstantiateProfile();
    }
}

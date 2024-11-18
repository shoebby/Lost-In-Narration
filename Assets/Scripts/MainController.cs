using TMPro;
using UnityEngine;
using System;

public class MainController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI logText;
    [SerializeField] private TextMeshProUGUI newLine;
    [SerializeField] private TMP_InputField inputField;
    
    [Header("Zuhui Variables")]
    [SerializeField] private Color zu_textColor;
    [SerializeField] private TMP_FontAsset zu_textFont;
    [SerializeField] private string zu_colorHex;

    [Header("Lexie Variables")]
    [SerializeField] private Color le_textColor;
    [SerializeField] private TMP_FontAsset le_textFont;
    [SerializeField] private string le_colorHex;

    [Header("Idle Timer Variables")]
    [SerializeField] private GameObject aboutGraphic;
    [SerializeField] private Animator aboutGraphicAnim;
    [SerializeField] private float timer_max = 5f;
    [SerializeField] private float timer_current;

    [Header("Timestamp Variables")]
    [SerializeField] private string timestampColorHex;

    private string currentHexColor = "EMPTY";
    private string currentFont = "EMPTY";
    private DateTime currentDateTime;
    bool isZuhui = false;
    bool aboutIsOpen = false;

    private void Awake()
    {
        aboutGraphicAnim = aboutGraphic.GetComponent<Animator>();
    }

    void Start()
    {
        inputField.Select();
        SetWriterVariables();
        timer_current = timer_max;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        inputField.Select();

        currentDateTime = DateTime.Now;

        if (Input.GetKeyDown(KeyCode.Tab))
            SwitchWriter();

        if (timer_current > 0)
        {
            if (Input.anyKey)
                timer_current = timer_max;

            timer_current -= Time.deltaTime;
            return;
        }
            
        if (timer_current <= 0)
        {
            if (Input.anyKey)
            {
                timer_current = timer_max;
                aboutGraphicAnim.Play("about_fade-out");
                aboutIsOpen = false;
                return;
            }

            if (!aboutIsOpen)
            {
                aboutGraphicAnim.Play("about_fade-in");
                aboutIsOpen = true;
            }
        }
    }

    public void NewLine()
    {
        string currentTime = currentDateTime.TimeOfDay.ToString();
        if (isZuhui)
            logText.text += "\n <size=20><color=#" + timestampColorHex + ">(" + currentTime.Substring(0, 8) + ")</color></size> <size=36>" + currentHexColor + currentFont + inputField.text + "</font>" + "</size></color>";
        else if (!isZuhui)
            logText.text += "\n <size=20><color=#" + timestampColorHex + ">(" + currentTime.Substring(0, 8) + ")</color></size> " + currentHexColor + currentFont + inputField.text + "</font>" + "</color>";
        inputField.text = "";
        inputField.Select();
        inputField.ActivateInputField();
    }

    public void SetWriterVariables()
    {
        if (isZuhui)
        {
            newLine.color = zu_textColor;
            newLine.font = zu_textFont;
            currentHexColor = "<color=#" + zu_colorHex + ">";
            currentFont = "<font=\"" + zu_textFont.name + "\">";
        }
        else if (!isZuhui)
        {
            newLine.color = le_textColor;
            newLine.font = le_textFont;
            currentHexColor = "<color=#" + le_colorHex + ">";
            currentFont = "<font=\"" + le_textFont.name + "\">";
        }
        Debug.Log(currentFont);
    }

    public void SwitchWriter()
    {
        isZuhui = !isZuhui;
        if (!string.IsNullOrEmpty(inputField.text))
            NewLine();
        SetWriterVariables();
    }
}

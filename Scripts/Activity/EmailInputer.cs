using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System;
using UnityEngine.UI;
using surfm.tool.i18n;

public class EmailInputer : MonoBehaviour {
    private static readonly string KEY_FORMATE_REEOR = "fomateError";
    public Image alertImg;
    private InputField inputField;
    private Color orgTextColor;
    public Text alertText;

    void Awake() {
        inputField = GetComponent<InputField>();
        orgTextColor = inputField.textComponent.color;
    }

    public void onValueChanged(string s) {
        validate();
    }

    internal void alert(string msg) {
        bool wrong = !string.IsNullOrEmpty(msg);
        alertImg.gameObject.SetActive(wrong);
        alertText.text= msg;
        inputField.textComponent.color = wrong ? Color.red : orgTextColor;
    }

    internal bool validate() {
        bool b = isValidEmail(inputField.text);
        string msg = b ? null : I18n.get(KEY_FORMATE_REEOR);
        alert(msg);
        return b;
    }



    public void onValueChange() { 
        alert(null);
    }

    public void clear() {
        inputField.text = "";
        alert(null);
    }

    public string getValue() {
        return inputField.text;
    }


    public bool isValidEmail(string strIn) {
        if (string.IsNullOrEmpty(strIn))
            return false;
        // Return true if strIn is in valid e-mail format.
        try {
            return Regex.IsMatch(strIn,
                  @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                  @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                  RegexOptions.IgnoreCase);
        } catch (Exception e) {
            return false;
        }
    }
}

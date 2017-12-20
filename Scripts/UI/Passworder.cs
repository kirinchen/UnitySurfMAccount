using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using surfm.tool.i18n;

public class Passworder : MonoBehaviour {
    private static readonly string KEY_EMPTY_PASS = "PassNotEmpty";
    private static readonly string KEY_LENGHT_LESS_PASS = "PassLengthLess";
    private InputField inputField;
    public Text alertText;
    public Image alertImg;
    public int lessPassLength = 6;

    void Awake() {
        inputField = GetComponentInChildren<InputField>();
    }

    public void onValueEditEnd(string s) {
        try {
            vaildate(s);
        } catch (PassException pe) {
            alert(pe.msg);
        }
    }

    public void onValueChange() {
        alert("");
    }

    internal bool validate() {
        try {
            vaildate(inputField.text);
            return true;
        } catch (PassException pe) {
            alert(pe.msg);
            return false;
        }
    }

    public void clear() {
        inputField.text = "";
    }

    public string getValue() {
        return inputField.text;
    }

    public void alert(string msg) {
        alertText.text = msg;
        alertImg.gameObject.SetActive(!string.IsNullOrEmpty(msg));
    }

    private void vaildate(string s) {
        if (string.IsNullOrEmpty(s)) {
            throw new PassException(I18n.get(KEY_EMPTY_PASS));
        } else if (s.Length < lessPassLength) {
            string msg = string.Format(I18n.get(KEY_LENGHT_LESS_PASS), lessPassLength);
            throw new PassException(msg);
        } else {
            vaildateOthers(s);
        }
    }

    internal virtual void vaildateOthers(string s) { }

    public class PassException : Exception {
        internal string msg;
        public PassException(string m) {
            msg = m;
        }
    }

}

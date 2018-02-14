using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneInputer : MonoBehaviour {
    private InputField inputField;
    public Text alertText;
    public Image alertImg;

    void Awake() {
        inputField = GetComponentInChildren<InputField>();
        inputField.onEndEdit.AddListener(onValueEditEnd);
        inputField.onValueChanged.AddListener(s => { alert(""); });
    }

    public void onValueEditEnd(string s) {

        try {
            vaildate(s);
        } catch (PhoneException pe) {
            alert(pe.msg);
        }
    }

    public void alert(string msg) {
        alertText.text = msg;
        alertImg.gameObject.SetActive(!string.IsNullOrEmpty(msg));
    }

    internal bool validate() {
        try {
            vaildate(inputField.text);
            return true;
        } catch (PhoneException pe) {
            alert(pe.msg);
            return false;
        }
    }

    public void vaildate(string s) {
        if (s.Length != 10)
            throw new PhoneException("phone number not 10 " + s.Length);
    }

    public class PhoneException : Exception {
        internal string msg;
        public PhoneException(string m) {
            msg = m;
        }
    }

    internal int getValue() {
        return int.Parse(inputField.text);
    }
}

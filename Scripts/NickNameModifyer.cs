using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace com.surfm.account {
    public class NickNameModifyer : MonoBehaviour {

        private InputField nameInput;

        void Awake() {
            nameInput = GetComponentInChildren<InputField>();
            nameInput.onEndEdit.AddListener(onEndEdit);
        }

        private void onEndEdit(string arg0) {
            Account a = AccountLoader.getAccount();
            a.userTitle = arg0;
            AccountLoader.save();
        }

        void Start() {
            Account a = AccountLoader.getAccount();
            nameInput.text = a.userTitle;
        }
    }
}

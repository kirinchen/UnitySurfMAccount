using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace com.surfm.account {
    public class AccountDataPlayerPrefsLoad : AccountDataLoad {
        public bool isExistAccountData() {
            return PlayerPrefs.HasKey(AccountConstant.KEY_SAVE_PREFS);
        }

        public string loadAccountFile() {
            return PlayerPrefs.GetString(AccountConstant.KEY_SAVE_PREFS);
        }

        public void saveAccountFile(string s) {
            PlayerPrefs.SetString(AccountConstant.KEY_SAVE_PREFS, s);
        }
    }
}

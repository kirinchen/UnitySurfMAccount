using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
namespace com.surfm.account {
    public class AccountConstant : MonoBehaviour {
        public static readonly string KEY_SAVE_PREFS = "@<Account>";
        public static bool DEV { get { return get().dev; } }
        public static string ACCOUNT_LOAD_SUFFIX { get { return get().accountLoadSuffix; } }

        public static string PATH { get { return get().accountSavePath; } }

        private static AccountConstant instance;
        public bool dev;
        public string accountLoadSuffix;
        public string accountSavePath = "/sdcard/.Yoar/Account.ya";

        public static string getAccountLoadPath(string op) {
            if (DEV) {
                return ACCOUNT_LOAD_SUFFIX + op;
            }
            return op;
        }

        internal static void saveAccountFile(string s) {
#if UNITY_WEBGL
            PlayerPrefs.SetString(KEY_SAVE_PREFS, s);
#else
            string p = getAccountLoadPath(PATH);
            FileInfo file = new FileInfo(p);
            file.Directory.Create();
            File.WriteAllText(p, s);
#endif
        }

        internal static string loadAccountFile() {
#if UNITY_WEBGL
            return PlayerPrefs.GetString(KEY_SAVE_PREFS);
#else
            return File.ReadAllText(getAccountLoadPath(PATH));
#endif
        }

        /*@TODO 加入 comm config to set save where */

        public static AccountConstant get() {
            if (instance == null) {
                instance = Resources.Load<AccountConstant>("@_AccountConstant");
            }
            return instance;
        }

        internal static bool isExistAccountData() {
#if UNITY_WEBGL
            return PlayerPrefs.HasKey(KEY_SAVE_PREFS);
#else
                    FileInfo fi = new FileInfo(getAccountLoadPath(PATH));
            return fi.Exists;
#endif
        }


    }
}

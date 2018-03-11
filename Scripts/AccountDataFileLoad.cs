using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
namespace com.surfm.account {
    public class AccountDataFileLoad : AccountDataLoad {
        public string loadAccountFile() {
            return File.ReadAllText(getAccountLoadPath(AccountConstant.PATH));
        }

        private string getAccountLoadPath(string op) {

#if UNITY_EDITOR_WIN
            if (AccountConstant.DEV) {
                return AccountConstant.ACCOUNT_LOAD_SUFFIX + op;
            }
#elif UNITY_STANDALONE_WIN
            string root = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            root = root.Replace(".exe", "_AccData/");
            op = root + op;
#else
            if (AccountConstant.DEV) {
                return AccountConstant.ACCOUNT_LOAD_SUFFIX + op;
            }
#endif
            return op;
        }

        public void saveAccountFile(string s) {
            string p = getAccountLoadPath(AccountConstant.PATH);
            FileInfo file = new FileInfo(p);
            file.Directory.Create();
            File.WriteAllText(p, s);
        }

        public bool isExistAccountData() {
            FileInfo fi = new FileInfo(getAccountLoadPath(AccountConstant.PATH));
            return fi.Exists;
        }

    }
}

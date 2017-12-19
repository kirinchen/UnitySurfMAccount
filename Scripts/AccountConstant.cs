using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace com.surfm.account {
    public class AccountConstant : MonoBehaviour {

        public static bool DEV { get { return get().dev; } }
        public static string ACCOUNT_LOAD_SUFFIX { get { return get().accountLoadSuffix; } }

        private static AccountConstant instance;
        public bool dev;
        public string accountLoadSuffix;

        public static string getAccountLoadPath(string op) {
            if (DEV) {
                return ACCOUNT_LOAD_SUFFIX + op;
            }
            return op;
        }

        public static AccountConstant get() {
            if (instance == null) {
                instance = Resources.Load<AccountConstant>("@_AccountConstant");
            }
            return instance;
        }
    }
}

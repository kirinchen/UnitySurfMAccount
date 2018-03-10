using UnityEngine;
namespace com.surfm.account {
    public class AccountConstant : MonoBehaviour {
        public static readonly string KEY_SAVE_PREFS = "@<Account>";
        public static bool DEV { get { return get().dev; } }
        public static string ACCOUNT_LOAD_SUFFIX { get { return get().accountLoadSuffix; } }
        public static bool LOAD_PLAYERPREF { get { return get().loadPlayerPref; } }
        public static string SMS_SERVICE { get { return get().smsService; } }
        public static bool PHONE_VAILD { get { return get().phoneVaild; } }

        public static string PATH { get { return get().accountSavePath; } }

        private static AccountConstant instance;
        public bool dev;
        public string accountLoadSuffix;
        public string accountSavePath = "/sdcard/.Yoar/Account.ya";
        public bool loadPlayerPref;
        public string smsService = "@SMSMitakeService";
        public bool phoneVaild;
        private AccountDataLoad dataLoad;

        private AccountDataLoad getDataLoad() {
            if (dataLoad == null) {
                if (loadPlayerPref) {
                    dataLoad = new AccountDataPlayerPrefsLoad();
                } else {
#if UNITY_WEBGL  || UNITY_IOS     || UNITY_EDITOR_OSX
                    dataLoad = new AccountDataPlayerPrefsLoad();
#else
                    dataLoad = new AccountDataFileLoad();
#endif
                }
            }
            return dataLoad;
        }


        internal static void saveAccountFile(string s) {
            get().getDataLoad().saveAccountFile(s);
        }

        internal static string loadAccountFile() {
            return get().getDataLoad().loadAccountFile();
        }

        /*@TODO 加入 comm config to set save where */

        public static AccountConstant get() {
            if (instance == null) {
                instance = Resources.Load<AccountConstant>("@_AccountConstant");
            }
            return instance;
        }

        internal static bool isExistAccountData() {
            return get().getDataLoad().isExistAccountData();
        }


    }
}

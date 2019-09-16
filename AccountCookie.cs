using UnityEngine;
using UnityEditor;
using Newtonsoft.Json;
using CodeStage.AntiCheat.Storage;

namespace com.surfm.account {
    public class AccountCookie  {

        private static  AccountCookie _instance = null;

        public static readonly string KEY_LOGIN_SESSION = "@LoginResult";

        private LoginResultDto cache = null;

        private AccountCookie() { }

        public void clean() {
            cache = null;
            ObscuredPrefs.DeleteKey(KEY_LOGIN_SESSION);
        }

        public void saveLoginSession(LoginResultDto dto) {
            clean();
            string js = JsonConvert.SerializeObject(dto);
            if (string.IsNullOrWhiteSpace(dto.jSessionId)) throw new System.Exception("not set jSessionId :"+ js);
            ObscuredPrefs.SetString(KEY_LOGIN_SESSION, js);
        }

        public LoginResultDto loadLoginSession() {
            if (cache == null) {
                cache = JsonConvert.DeserializeObject<LoginResultDto>(ObscuredPrefs.GetString(KEY_LOGIN_SESSION));
            }
            return cache;
        }

        public string optSession() {
            return ObscuredPrefs.HasKey(KEY_LOGIN_SESSION) ? loadLoginSession().jSessionId : null;
        }

        public static AccountCookie instance {
            get {
                if (_instance == null) {
                    _instance = new AccountCookie();
                }
                return _instance;
            }
        }


    }
}
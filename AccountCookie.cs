using UnityEngine;
using UnityEditor;
using Newtonsoft.Json;
using CodeStage.AntiCheat.ObscuredTypes;

namespace com.surfm.account {
    public class AccountCookie  {

        public static readonly AccountCookie instance = new AccountCookie();

        public static readonly string KEY_LOGIN_SESSION = "@LoginResult";

        private AccountCookie() { }

        public void saveLoginSession(LoginResultDto dto) {
            string js = JsonConvert.SerializeObject(dto);
            if (string.IsNullOrWhiteSpace(dto.jSessionId)) throw new System.Exception("not set jSessionId :"+ js);
            ObscuredPrefs.SetString(KEY_LOGIN_SESSION, js);
        }

        public LoginResultDto loadLoginSession() {
            return JsonConvert.DeserializeObject<LoginResultDto>( ObscuredPrefs.GetString(KEY_LOGIN_SESSION));
        }

        public string optSession() {
            return ObscuredPrefs.HasKey(KEY_LOGIN_SESSION) ? loadLoginSession().jSessionId : null;
        }


    }
}
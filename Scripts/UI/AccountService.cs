using UnityEngine;
using System.Collections;
using System;
using Newtonsoft.Json;
using BestHTTP;
using System.Collections.Generic;
namespace com.surfm.account {
    public class AccountService : MonoBehaviour {
        public static readonly string KEY_LOGIN_DATA = "KEY_LOGIN_DATA";

        private static AccountService instance;
        private URestApi rest;
        private string session;
        private Dictionary<XGunServiceHandle.Mode, XGunServiceHandle> _hmap = new Dictionary<XGunServiceHandle.Mode, XGunServiceHandle>();

        void Awake() {
            instance = this;
            rest = GetComponent<URestApi>();

            loadSession(loadLoginResult());

            foreach (XGunServiceHandle x in GetComponents<XGunServiceHandle>()) {
                _hmap.Add(x.mode, x);
            }
        }

        public LoginResultDto loadLoginResult() {
            if (PlayerPrefs.HasKey(KEY_LOGIN_DATA)) {
                try {
                    return JsonConvert.DeserializeObject<LoginResultDto>(PlayerPrefs.GetString(KEY_LOGIN_DATA));
                } catch (Exception e) {
                    Debug.LogException(e);
                }
            }
            return null;
        }

        public void syncData(SyncRequestDto d, RestHandler<SyncPlayerData> h, Action onForbiddenAction) {
            rest.postJson("/api/v1/xgun/player/sync", d, (msg) => {
                SyncPlayerData sd = JsonConvert.DeserializeObject<SyncPlayerData>(msg);
                h.onOk(sd);
            }, (m, s, r, e) => {
                handleError(m, r, h, onForbiddenAction);
            });
        }

        public void signup(UserSignupFormDto dto, LoginHandler sh) {
            rest.postJson("/api/v1/public/signup", dto, (msg) => {
                handleLogined(XGunServiceHandle.Mode.Base, dto, msg, sh);
            }, (m, s, r, e) => {
                handleError(m, r, sh);
            });
        }


        private void handleError<T>(string m, HTTPResponse resp, RestHandler<T> sh, Action onForbiddenAction = null) {
            try {
                SurfMErrorDto d = SurfMErrorDto.parse(resp.DataAsText);
                if (onForbiddenAction != null && (d.getSurfMError() == SurfMErrorDto.SurfMError.ApiKeyFailException || d.getSurfMError() == SurfMErrorDto.SurfMError.NoSuchElementException)) {
                    onForbiddenAction();
                } else {
                    sh.onException(d);
                }
            } catch (Exception e) {
                sh.onError(e);
            }
        }

        internal void abortAll() {
            rest.abortAll();
        }

        private void handleLogined(XGunServiceHandle.Mode m, object d, string msg, LoginHandler sh) {
            LoginResultDto l = JsonConvert.DeserializeObject<LoginResultDto>(msg);
            saveSession(l);
            getHandle(m).saveUserAndPass(d);
            sh.onOk(l);
        }

        public bool isLogin() {
            return !string.IsNullOrEmpty(session);
        }

        internal bool isSelf() {
            LoginResultDto d = loadLoginResult();
            return d.accountType == LoginType.SLEF;
        }

        public void logout() {
            deleteLoginData();
            rest.get("/login?logout", (m) => {
                Debug.Log("Logout ok");
            }, (m, s, r, e) => {
                Debug.Log("error logout =" + m);
            });
        }

        private void deleteLoginData() {
            session = "";
            rest.authorization = "";
            PlayerPrefs.SetString(KEY_LOGIN_DATA, session);
        }

        internal XGunServiceHandle getHandle(XGunServiceHandle.Mode m) {
            return _hmap[m];
        }


        public void login(XGunServiceHandle.Mode mode, object d, LoginHandler sh) {
            deleteLoginData();
            XGunServiceHandle h = getHandle(mode);
            rest.postJson(h.loginUrl, h.setupLoginDto(d), (msg) => {
                handleLogined(mode, d, msg, sh);
            }, (m, s, r, e) => {
                handleError(m, r, sh);
            });
        }

        public void relogin(XGunServiceHandle.Mode m, LoginHandler sh) {
            object d = getHandle(m).getUserLoginFormDto();
            login(m, d, sh);
        }

        public void createData(PlayerUpdateDto dto, RestHandler<string> rh) {
            rest.postJson("/api/v1/xgun/player/create", dto, (msg) => {
                rh.onOk(msg);
            }, (m, s, r, e) => {
                handleError(m, r, rh);
            });
        }

        public void updateData(PlayerUpdateDto d, RestHandler<string> rh, Action onForbiddenAction = null) {
            rest.postJson("/api/v1/xgun/player/update", d, (msg) => {
                rh.onOk(msg);
            }, (m, s, r, e) => {
                handleError(m, r, rh, onForbiddenAction);
            });
        }

        private void saveSession(LoginResultDto d) {
            if (d != null) {
                string s = JsonConvert.SerializeObject(d);
                PlayerPrefs.SetString(KEY_LOGIN_DATA, s);
                loadSession(d);
            }
        }

        private void loadSession(LoginResultDto d) {
            if (d != null) {
                session = d.jSessionId;
                rest.authorization = session;
            }
        }
        public class SyncPlayerData {
            public enum State {
                NONE, TOO_OLD, TOO_NEW, NOT_CREATED
            }
            public State state;
            public string data;
            public int coin;
            public int dataVersion;
            public int schemaVersion;
            public string uid;
        }
        public enum Era {
            Mobile, Standalone
        }
        public class SyncRequestDto {
            public Era era = Era.Standalone;
            public int dataVersion;
            public string uid;
        }
        public class PlayerUpdateDto {
            public Era era = Era.Standalone;
            public string uid;
            public string data;
            public int coin;
            public int dataVersion;
            public int schemaVersion;
        }
        public class UserSignupFormDto : UserLoginFormDto {
            public string botCodeValue;
            public string botCodeKey;
        }
        public class UserLoginFormDto {

            public string email;
            public string password;

            public LoginType bindType = LoginType.NONE;
            public string bindUid;
        }


        public interface LoginHandler : RestHandler<LoginResultDto> { }

        public interface RestHandler<T> {
            void onOk(T dto);
            void onException(SurfMErrorDto dto);
            void onError(Exception e);
        }

        public static AccountService getInstance() {
            return instance;
        }

    }
}

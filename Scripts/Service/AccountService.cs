using UnityEngine;
using System.Collections;
using System;
using Newtonsoft.Json;
using BestHTTP;
using System.Collections.Generic;
namespace com.surfm.account {
    public class AccountService : MonoBehaviour {

        private static AccountService instance;
        private URestApi rest;
        private Dictionary<ServiceHandle.Mode, ServiceHandle> _hmap = new Dictionary<ServiceHandle.Mode, ServiceHandle>();
        private ItemService itemService;

        void Awake() {
            if (instance != null) {
                Destroy(gameObject);
                return;
            }
            instance = this;
            DontDestroyOnLoad(gameObject);
            rest = GetComponent<URestApi>();
            loadSession();
            foreach (ServiceHandle x in GetComponents<ServiceHandle>()) {
                _hmap.Add(x.mode, x);
            }
        }



        public void signup(UserSignupFormDto dto, LoginHandler sh) {
            ErrorAction ea = new ErrorAction(eb => { sh.onError(eb.e); }).setSessionFailAction(d => {
                sh.onException(d);
            }).setServerErrorAction(d => {
                sh.onException(d);
            });
            rest.postJson("/api/v1/public/signup", dto, (msg) => {
                handleLogined(ServiceHandle.Mode.Base, dto, msg, sh);
            }, ea.onError);
        }

        public void loadDebotCode(string key, Action<Texture2D> cb, URestApi.OnError onError) {
            rest.loadRes("/api/v1/public/derobotcode?key=" + key, (w) => {
                cb(w.DataAsTexture2D);
            }, onError);
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

        private void handleLogined(ServiceHandle.Mode m, object d, string msg, LoginHandler sh) {
            LoginResultDto l = JsonConvert.DeserializeObject<LoginResultDto>(msg);
            getHandle(m).saveUserAndPass(l, d);
            sh.onOk(l);
        }

        public bool isLogin() {
            return AccountLoader.getAccount().isLogined();
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
            rest.authorization = "";
            AccountLoader.removeData();
        }

        internal ServiceHandle getHandle(ServiceHandle.Mode m) {
            return _hmap[m];
        }


        public void login(ServiceHandle.Mode mode, object d, LoginHandler sh) {
            deleteLoginData();

            ErrorAction ea = new ErrorAction(eb => { sh.onError(eb.e); }).setSessionFailAction(dto => {
                sh.onException(dto);
            }).setServerErrorAction(dto => {
                sh.onException(dto);
            });

            ServiceHandle h = getHandle(mode);
            rest.postJson(h.loginUrl, h.setupLoginDto(d), (msg) => {
                handleLogined(mode, d, msg, sh);
            }, ea.onError);
        }

        public void relogin(LoginHandler sh) {
            ServiceHandle csh = getCurrentHandle();
            object d = csh.getUserLoginFormDto();
            login(csh.mode, d, sh);
        }

        private ServiceHandle getCurrentHandle() {
            foreach (ServiceHandle sh in _hmap.Values) {
                bool b = sh.isLoaded();
                Debug.Log("b=" + b);
                if (b) {
                    Debug.Log("sh.isLoaded()");
                    return sh;
                }
            }
            throw new NullReferenceException("not find any service handler");

        }

        public ItemService getItemService() {
            if (!isLogin()) throw new NullReferenceException("not login");
            if (itemService == null) {
                itemService = gameObject.AddComponent<ItemService>();
                itemService.init(rest, relogin);
            }
            return itemService;
        }


        private void loadSession() {
            Account a = AccountLoader.getAccount();
            if (a.isLogined()) {
                rest.authorization = a.loginResult.jSessionId;
            }
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

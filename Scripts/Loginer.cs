using System;
using System.Collections;
using System.Collections.Generic;
using com.surfm.rest;
using Newtonsoft.Json;
using surfm.dreamon;
using surfm.tool;
using UnityEngine;
using static com.surfm.rest.URestApi;

namespace com.surfm.account {
    public class Loginer {

        private PublicRest publicRest { get { return PublicRest.getInstance(); } }  
        private static Loginer _instance;
        public static Loginer instance {
            get {
                if (_instance == null) _instance = new Loginer();
                return _instance;
            }
        }

        private Handler handler { get { return BeansRepo.bean<Handler>(); } }


        private Loginer() { }

        public void login(string email,string pass, Action< TypeResult<LoginResultDto>> cb = null) {
            UserLoginFormDto dto = new UserLoginFormDto();
            dto.email = email;
            dto.password = pass;
            publicRest.loginForGeneral(dto, _r=> {
                onResult(_r);
                cb?.Invoke(_r);
            } );
        }

        private void onResult(TypeResult<LoginResultDto> r) {
            Debug.Log("onResult: "+r.result.succeed+" "+r.result.response.DataAsText+" " + r.result.errorMsg);
            if (r.result.succeed) {
                try {
                    LoginResultDto rDto =  r.getBody();
                    AccountCookie.instance.saveLoginSession(rDto);
                    handler.onLogined(rDto);
                } catch (Exception e) {
                    Debug.Log("onResult: "+ AccountCookie.instance+" e:"+e.ToString());
                }
            } else {
                handler.onLoginFail(r.result);
            }
        }



        public interface Handler {
            void onLogined(LoginResultDto rDto);
            void onLoginFail(Result result);
        }

        public class DebugHandler : Handler {
            public void onLogined(LoginResultDto rDto) {
                Debug.Log("onLogined="+JsonConvert.SerializeObject(rDto,ObscuredValueConverter.DEFAULT));
            }

            public void onLoginFail(Result result) {
                Debug.LogError("onLoginFail=" + JsonConvert.SerializeObject(result, ObscuredValueConverter.DEFAULT));
            }
        }

    }
}

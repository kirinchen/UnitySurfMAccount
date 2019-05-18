using System.Collections;
using System.Collections.Generic;
using com.surfm.rest;
using surfm.dreamon;
using UnityEngine;
using static com.surfm.rest.URestApi;

namespace com.surfm.account {
    public class Loginer {

        private PublicRest publicRest = PublicRest.getInstance();
        private Handler handler;

        public Loginer(Handler s) {
            handler = s;
        }



        public void login() {

            UserLoginFormDto dto = new UserLoginFormDto();
            dto.email = handler.getEmail();
            dto.password = handler.getPassword();

            publicRest.loginForGeneral(dto, onResult);


        }

        private void onResult(RestResult<LoginResultDto> r) {
            Debug.Log("onResult: " + r.result.errorMsg);
            if (r.result.succeed) {
                LoginResultDto rDto =  r.getBody();
                AccountCookie.instance.saveLoginSession(rDto);
                handler.onLogined(rDto);
            } else {
                handler.onLoginFail(r.result);
            }
        }



        public interface Handler {
            string getEmail();
            string getPassword();
            void onLogined(LoginResultDto rDto);
            void onLoginFail(Result result);
        }

    }
}

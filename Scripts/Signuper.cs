using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static com.surfm.rest.URestApi;

namespace com.surfm.account {
    public class Signuper  {

        private PublicRest publicRest = PublicRest.getInstance();
        private Handler handler;
        public Signuper(Handler s) {
            handler = s;
        }


        public void signup() {

            UserSignupFormDto dto = new UserSignupFormDto();
            dto.email = handler.getEmail();
            dto.password = handler.getPassword();

            publicRest.signUp(dto, onResult);


        }

        private void onResult(Result r) {
            Debug.Log("onResult: " + r.errorMsg);
        }



        public interface Handler {
            string getEmail();
            string getPassword();
        }

    }
}

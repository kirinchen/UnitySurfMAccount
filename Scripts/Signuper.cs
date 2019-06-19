using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static com.surfm.rest.URestApi;

namespace com.surfm.account {
    public class Signuper  {

        private static Signuper _instance;
        public static Signuper instance {
            get {
                if(_instance == null) _instance = new Signuper();
                return _instance;
            }
        }

        private PublicRest publicRest = PublicRest.getInstance();
        private Signuper() {        }


        public void signup(string email,string pass,Action<Result> cb = null) {
            UserSignupFormDto dto = new UserSignupFormDto();
            dto.email = email;
            dto.password = pass;
            publicRest.signUp(dto, r=> {
                onResult(r);
                cb?.Invoke(r);
            } );
        }

        private void onResult(Result r) {
            Debug.Log("onResult: " + JsonConvert.SerializeObject(r));
        }




    }
}

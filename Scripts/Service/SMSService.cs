using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace com.surfm.account {
    public abstract class SMSService : MonoBehaviour {

        private static SMSService instance;

        public enum ErrorKind {
            Other, phoneNULL,
            phoneLength, bodyNULL,
            ApiAuth
        }


        internal virtual void init() {
        }

        public void sned(string phone, string body, Action done, Action<ErrorKind> errorCB) {
            if (string.IsNullOrEmpty(phone)) {
                errorCB(ErrorKind.phoneNULL);
                return;
            }
            if (phone.Length != 10) {
                errorCB(ErrorKind.phoneLength);
                return;
            }
            if (string.IsNullOrEmpty(body)) {
                errorCB(ErrorKind.bodyNULL);
                return;
            }
            sendToRemote(phone, body, done, errorCB);
        }

        internal abstract void sendToRemote(string phone, string body, Action done, Action<ErrorKind> errorCB);


        public static SMSService getInstance() {
            if (instance == null) {
                string path = AccountConstant.SMS_SERVICE;
                instance = Resources.Load<SMSService>(path);
                instance.init();
            }
            return instance;
        }


    }
}

using com.surfm.rest;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
namespace com.surfm.account {
    public class SMSMitakeService : SMSService {
        public string password;
        public string account;

        private URestApi api;

        internal override void init() {
            api = GetComponent<URestApi>();
        }


        internal override void sendToRemote(string phone, string body, Action done, Action<ErrorKind> errorCB) {
            if (string.IsNullOrEmpty(password)) {
                errorCB(ErrorKind.ApiAuth);
                return;
            }
            if (string.IsNullOrEmpty(account)) {
                errorCB(ErrorKind.ApiAuth);
                return;
            }
            StringBuilder url = new StringBuilder("/SmSendGet.asp?");
            url.Append("username=").Append(password);
            url.Append("&password=").Append(account);
            url.Append("&encoding=utf-16be");
            url.Append("&dstaddr=").Append(phone);
            url.Append("&smbody=").Append(WWW.EscapeURL(body));
            //api.get(url.ToString(), d => { done(); }, new URestApi.OnErrorB(b => {
            //    errorCB(ErrorKind.Other);
            //}).onError);
        }
    }
}

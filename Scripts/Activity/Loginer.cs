﻿using UnityEngine;
using System.Collections;
using System;
using surfm.tool;
using surfm.tool.i18n;

namespace com.surfm.account {
    public class Loginer : PageHandler, AccountService.LoginHandler {

        public static readonly string KEY_USER_PASS_WRONG = "AccoutPassWrong";
        private AccountManager am { get { return AccountManager.getInstance(); } }
        public EmailInputer email;
        public Passworder password;



        internal override void show() {
            base.show();
            reflesh();
        }

        private void reflesh() {
            am.backButton.gameObject.SetActive(false);
            am.backButton.onClick.RemoveAllListeners();
        }

        public void login() {
            doLogin();
           /* if (AccountLoader.isEmpty()) {
                doLogin();
            } else {

                if (AccountLoader.isEmpty()) {
                    doLogin();
                } else {
                    doLogin();
                    //am.dm.get<YesNoDialog>().show(I18n.get("IfOverwriteData"), (b) => {
                    //    if (b) {
                    //        doLogin();
                    //    }
                    //});
                }
            }    */
        }

        private void doLogin() {
            Loading.getInstance().show(true);
            UserLoginFormDto d = new UserLoginFormDto();
            d.email = email.getValue();
            d.password = password.getValue();
            AccountService.getInstance().login(ServiceHandle.Mode.Base, d, this);
        }

        public void onError(Exception e) {
            Loading.getInstance().show(false);
            Toast.getInstance().show(I18n.get(Signuper.KEY_UNNOWN_FAIL));
        }

        public void onException(SurfMErrorDto dto) {
            Loading.getInstance().show(false);
            Toast.getInstance().show(I18n.get(KEY_USER_PASS_WRONG));
        }

        public void onOk(LoginResultDto dto) {
            Loading.getInstance().show(false);
            Debug.Log("onOk=" + dto);
            am.switchHelloPage();
        }

        public void goToSignup() {
            am.switchSignupPage();
        }
    }
}

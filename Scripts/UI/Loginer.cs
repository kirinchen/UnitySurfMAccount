using UnityEngine;
using System.Collections;
using System;
using surfm.tool;
using surfm.tool.i18n;

namespace com.surfm.account {
    public class Loginer : PageHandler, AccountService.LoginHandler {

        public static readonly string KEY_USER_PASS_WRONG = "AccoutPassWrong";
        public DialogManager dm;
        private AccountManager am;
        public EmailInputer email;
        public Passworder password;

        new void Awake() {
            base.Awake();
            am = FindObjectOfType<AccountManager>();
        }


        public void login() {
            if (AccountLoader.isEmpty()) {
                doLogin();
            } else {
                dm.get<YesNoDialog>().show( I18n.get("IfOverwriteData"), (b) => {
                    if (b) {
                        doLogin();
                    }
                });
            }
        }

        private void doLogin() {
            Loading.getInstance().show(true);
            AccountService.UserLoginFormDto d = new AccountService.UserLoginFormDto();
            d.email = email.getValue();
            d.password = password.getValue();
            AccountService.getInstance().login(XGunServiceHandle.Mode.Base, d, this);
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
          
            /* TODO 資料寫入到account */

            Debug.Log("onOk=" + dto);
            am.switchPage(am.helloPage);
        }

        public void goToSignup() {
            am.switchPage(am.signupPage);
        }
    }
}

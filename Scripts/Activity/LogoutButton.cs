using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using com.surfm.account;
using surfm.tool;

namespace com.surfm.account {
    public class LogoutButton : MonoBehaviour {

        private AccountManager am;
        public GameObject button;

        void Start() {
            am = FindObjectOfType<AccountManager>();
            refleshUserName();
        }

        public void refleshUserName() {
            bool selfLogined = false;
            LoginResultDto rd = null;
            if (AccountService.getInstance().isLogin()) {
                rd = AccountLoader.getAccount().loginResult;
                if (rd.accountType == LoginType.SLEF) {
                    selfLogined = true;
                }
            }
            button.SetActive(selfLogined);
        }


        public void logout() {
            AccountLoader.removeData();
            AccountService.getInstance().logout();
            AccountManager.getInstance().switchLoginPage();
        }


    }
}

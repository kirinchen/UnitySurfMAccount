using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
namespace com.surfm.account {
    public class AccountButton : MonoBehaviour {

        public Image icon;
        private Text text;
        public int loggedTextSize;
        public int loggingTextSize;


        void Awake() {
            text = GetComponentInChildren<Text>();
        }

        void Start() {
            /* @TODO if (IndexManager.getInstance() != null) {
                 IndexManager.getInstance().addOnInitAction(() => {
                     reflesh();
                 });
             } else {
                 TDSUtility.delayInvoke(this, reflesh, 0.5f);
             } */
        }

        private void reflesh() {
            if (AccountService.getInstance().isLogin()) {
                showLogged();
            } else {
                showNotLogin();
            }
        }

        private void showLogged() {

            // @TODO text.text = PlatformIniterFactory.getAccountName();
            text.fontSize = loggedTextSize;
        }

        private void showNotLogin() {
            // @TODO   text.text = LanguageIniter.getI18n("Login");
            text.fontSize = loggingTextSize;
        }

        public void goToAccountPage() {
            AccountManager.goAccoutPage();
        }


    }
}

using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using surfm.tool;
using surfm.tool.i18n;

namespace com.surfm.account {
    public class AccountManager : MonoBehaviour {
        private static AccountManager instance;
        private static string goBackPage = string.Empty;
        public DialogManager dm;
        public PageHandler loginPage;
        public PageHandler signupPage;
        public PageHandler helloPage;

        private PageHandler[] pages;

        private PageHandler currentPage;
        internal Action refleshCB = () => { };

        void Awake() {
            instance = this;
            if (enabled) {
                GSui.Instance.m_AutoAnimation = false;
            }
            pages = new PageHandler[] { loginPage, signupPage, helloPage };
            foreach (PageHandler g in pages) {
                g.gameObject.SetActive(true);
            }

        }

        internal void switchPage(PageHandler loginPage) {
            currentPage = loginPage;
            refleshPage();
        }


        public void switchLoginPage() { switchPage(loginPage); }
        public void switchSignupPage() { switchPage(signupPage); }


        void Start() {
            refleshForLogin();
        }

        private void refleshForLogin() {
            if (AccountService.getInstance().isLogin()) {
                currentPage = helloPage;
            } else {
                currentPage = loginPage;
            }
            refleshPage();
        }

        public void resetDataAndExit() {
            dm.get<YesNoDialog>().show(I18n.get("ResetRecord"), (b) => {
                if (b) {
                    AccountLoader.removeData();
                    Application.CancelQuit();
                }
            });
        }

        private void refleshPage() {
            foreach (PageHandler g in pages) {
                if (g == currentPage) {
                    g.show();
                } else {
                    g.hide();
                }
            }
            refleshCB();
        }

        public void exit() {
            AccountService.getInstance().abortAll();
            SceneManager.LoadScene(goBackPage);
            goBackPage = string.Empty;
        }

        public static void goAccoutPage() {
            Scene scene = SceneManager.GetActiveScene();
            goBackPage = scene.name;
            SceneManager.LoadScene("Account");
        }

        public static AccountManager getInstance() {
            return instance;
        }


    }
}

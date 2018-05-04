using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using surfm.tool;
using surfm.tool.i18n;
using UnityEngine.UI;
using GUIAnimator;

namespace com.surfm.account {
    public class AccountManager : MonoBehaviour {
        private static AccountManager instance;
        internal static ActivityConfig config { get;  set; }
        public DialogManager dm;
        public PageHandler loginPage;
        public PageHandler signupPage;
        public PageHandler helloPage;
        public Button backButton;

        private PageHandler[] pages;

        private PageHandler currentPage;
        internal Action refleshCB = () => { };

        void Awake() {
            instance = this;
            if (config==null) config = ActivityConfig.LoadFromRepo();
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
        public void switchHelloPage() { switchPage(helloPage); }


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

        public void cleanAccount() {
            PlayerPrefs.DeleteAll();
            AccountLoader.removeData();
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



        public static void goAccoutPage(ActivityConfig ac) {
            Scene scene = SceneManager.GetActiveScene();
            ac.goBackPage = scene.name;
            SceneManager.LoadScene("Account");
        }

        public static AccountManager getInstance() {
            return instance;
        }


    }
}

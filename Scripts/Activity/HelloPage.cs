using surfm.tool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace com.surfm.account {
    public class HelloPage : PageHandler {

        public InputFieldBG userTitleField;
        public Image avatarImg;
        public Button playButton;
        public Text userUid;
        private AccountManager am { get { return AccountManager.getInstance(); } }

        void Start() {
            playButton.onClick.AddListener(goToPlay);
        }

        private void goToPlay() {
            Loading.getInstance().show(true);
            SceneManager.LoadSceneAsync(AccountManager.config.playIntentScene);
        }

        internal override void show() {
            base.show();
            reflesh();
        }

        public void save() {
            Account account = AccountLoader.getAccount();
            account.userTitle = userTitleField.text;
            AccountLoader.save();
            reflesh();
        }

        public void reflesh() {
            Account a = AccountLoader.getAccount();
            userTitleField.text = a.userTitle;
            a.setupAvatar(avatarImg);
            if (userUid != null) {
                userUid.text = a.pid;
            }
            if (string.IsNullOrEmpty(AccountManager.config.goBackPage)) {
                am.backButton.gameObject.SetActive(false);
            } else {
                am.backButton.gameObject.SetActive(true);
                am.backButton.onClick.RemoveAllListeners();
                am.backButton.onClick.AddListener(exit);
            }
        }

        private void exit() {
            AccountService.getInstance().abortAll();
            SceneManager.LoadScene(AccountManager.config.goBackPage);
            AccountManager.config = new ActivityConfig();
        }
    }
}

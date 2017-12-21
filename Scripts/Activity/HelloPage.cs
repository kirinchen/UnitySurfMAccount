using surfm.tool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace com.surfm.account {
    public class HelloPage : PageHandler {

        public Text nameText;
        public Image avatarImg;
        public Button playButton;

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

        private void reflesh() {
            Account a = AccountLoader.getAccount();
            nameText.text = a.userTitle;
            a.setupAvatar(avatarImg);
        }
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStomp;
using System;
using BestHTTP;
namespace com.surfm.account {
    public class DBotCoder : MonoBehaviour {
        private URestApi rest;
        private RawImage img;
        private string key;
        public Image LoadingImg;
        public Button reloadButton;
        private Texture orgNoneTexture;
        private Botcoder coder;

        void Awake() {
            rest = FindObjectOfType<URestApi>();
            img = GetComponentInChildren<RawImage>();
            coder = GetComponentInChildren<Botcoder>();
            orgNoneTexture = img.texture;
            key = UidUtils.getRandomString(5);
        }

        public void reloadBotCode() {
            setupLoadingUi(true);
            rest.loadRes("/api/v1/public/derobotcode?key=" + key, (w) => {
                setupLoadingUi(false);
                img.texture = w.DataAsTexture2D;
            }, onError);
        }

        internal bool validate() {
            return coder.validate();
        }

        private void setupLoadingUi(bool loading) {
            reloadButton.gameObject.SetActive(!loading);
            img.texture = loading ? orgNoneTexture : img.texture;
            LoadingImg.gameObject.SetActive(loading);
        }

        private void onError(string error, HTTPRequestStates s, HTTPResponse resp, Exception e) {
            Debug.Log("DeBotCodeImg error=" + error);
        }

        public string getKey() {
            return key;
        }

        public string getValue() {
            return coder.getValue();
        }

        public void alert(string msg) {
            coder.alert(msg);
        }

    }
}
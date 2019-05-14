using surfm.tool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace com.surfm.account {
    public class Account {

        public static readonly string EMAIL_SHA1_PREFIX = "<SurfMonster>";

        public enum AvatarMode {
            NONE, Default, Http
        }

        public string pid;
        public string userTitle;
        public string avatar;
        public string email, password;
        public LoginResultDto loginResult;

        internal Account(string email, string password, LoginResultDto lr) {
            this.email = email;
            this.password = password;
            this.loginResult = lr;
            pid = lr.uid;
            userTitle = pid;
        }

        internal Account() {
            pid = UidUtils.getRandomString(12);
            userTitle = pid;
            setAvatar(DefaultAvatarUtils.getDefault());
        }

        public void setAvatar(DefaultAvatarUtils.Bundle target) {
            avatar = string.Format("@{0}", target.path);
        }



        public static AvatarMode parseAvatarMode(string s) {
            if (string.IsNullOrEmpty(s)) return AvatarMode.NONE;
            if (s.IndexOf("@") == 0) return AvatarMode.Default;
            return AvatarMode.NONE;
        }

        public void setupAvatar(Image img) {
            setupAvatar(img, avatar);
        }

        public Sprite getAvatarSpite() {
            return loadSprite(avatar);
        }

        public static Sprite loadSprite(string ad) {
            AvatarMode am = parseAvatarMode(ad);
            if (am == AvatarMode.Default) {
                string path = ad.Replace("@", "");
                return DefaultAvatarUtils.get(path).sprite;
            }
            return DefaultAvatarUtils.getDefault().sprite;
        }

        public static void setupAvatar(Image img, string ad) {
            img.sprite = loadSprite(ad);
        }

        public static string getPidByMail(string m) {
            return CommUtils.getSha1(EMAIL_SHA1_PREFIX + m).Substring(0, 12);
        }

        internal bool isLogined() {
            if (string.IsNullOrEmpty(email)) return false;
            if (string.IsNullOrEmpty(password)) return false;
            if (loginResult == null) return false;
            return true;
        }

        public static bool isSelf(string pid) {
            return string.Equals(AccountLoader.getAccount().pid, pid);
        }

    }
}

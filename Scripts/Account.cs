using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStomp;

namespace com.surfm.account {
    public class Account {

        public enum AvatarMode {
            NONE, Default, Http
        }

        public string pid;
        public string userTitle;
        public string avatar;

        internal Account() {
            pid = UidUtils.getRandomString(12);
            userTitle = pid;
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
            setupAvatar(img,avatar);
        }

        public static void setupAvatar(Image img,string ad) {
            AvatarMode am = parseAvatarMode(ad);
            if (am == AvatarMode.Default) {
                string path = ad.Replace("@", "");
                img.sprite = DefaultAvatarUtils.get(path).sprite;
            }
        }
    }
}

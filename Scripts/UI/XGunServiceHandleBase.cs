using surfm.tool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace com.surfm.account {
    public abstract class XGunServiceHandle : MonoBehaviour {

        public enum Mode {
            Base, Steam, FB
        }

        public string loginUrl = "/api/v1/public/login?type=REST_API";
        public Mode mode = Mode.Base;

        internal abstract void saveUserAndPass(object d);

        internal abstract object getUserLoginFormDto();

        internal abstract object setupLoginDto(object d);
    }

    public class XGunServiceHandleBase : XGunServiceHandle {

        private static readonly string KEY_USERNAME = "KEY_USERNAME";
        private static readonly string KEY_PASSWORD = "KEY_PASSWORD";

        internal override void saveUserAndPass(object dd) {
            AccountService.UserLoginFormDto d = dd as AccountService.UserLoginFormDto;
            PlayerPrefs.SetString(KEY_USERNAME, d.email);
            //TODO 防護駭客
            PlayerPrefs.SetString(KEY_PASSWORD, d.password);
        }

        internal override object getUserLoginFormDto() {
            AccountService.UserLoginFormDto ans = new AccountService.UserLoginFormDto();
            ans.email = PlayerPrefs.GetString(KEY_USERNAME);
            //TODO 防護駭客
            ans.password = PlayerPrefs.GetString(KEY_PASSWORD);
            return ans;
        }

        internal override object setupLoginDto(object d) {
            return d;
        }
    }
}

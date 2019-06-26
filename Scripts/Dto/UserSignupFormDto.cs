using surfm.tool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace com.surfm.account {
    public class UserSignupFormDto : UserLoginFormDto {

        public string recaptchaResponse ="xxx";
        public string phone =UidUtils.getRandomNumber(10);
        public string skipCode = "ffff";

    }
}

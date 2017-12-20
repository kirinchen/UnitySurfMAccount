using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace com.surfm.account {
    public class UserLoginFormDto {

        public string email;
        public string password;

        public LoginType bindType = LoginType.NONE;
        public string bindUid;
    }
}

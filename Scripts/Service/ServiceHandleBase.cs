using surfm.tool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace com.surfm.account {


    public class ServiceHandleBase : ServiceHandle {



        internal override void saveUserAndPass(LoginResultDto l, object dd) {
            UserLoginFormDto d = dd as UserLoginFormDto;
            Account a = new Account(d.email, d.password, l);
            AccountLoader.instance.setAccount(a);
        }

        internal override object getUserLoginFormDto() {
            UserLoginFormDto ans = new UserLoginFormDto();
            Account a = AccountLoader.getAccount();
            ans.email = a.email;
            //TODO 防護駭客
            ans.password = a.password;
            return ans;
        }

        internal override object setupLoginDto(object d) {
            return d;
        }

        internal override bool isLoaded() {
            return !AccountLoader.isEmpty();
        }
    }
}

using com.surfm.rest;
using surfm.tool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace com.surfm.account {
    public class AppBeans : Beans {

        [BeanAttribute( name = "AccountServer" )]
        public URestApi accountRest() {
            string env = ConstantRepo.getInstance().opt<string>("env", "");
            env = string.IsNullOrEmpty(env) ? "." : "." + env + ".";
            URestApi ans = new URestApi();
            ans.host = ConstantRepo.getInstance().get<string>("account.server" + env + "host");
            ans.port = ConstantRepo.getInstance().get<string>("account.server" + env + "port");
            return ans;

        }

    }
}
                                          
using surfm.tool;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
namespace com.surfm.account {
    public class ActivityConfig {


        [ConstAttribute("account.playIntentScene")]
        public string playIntentScene;
        [ConstAttribute("account.goBackPage")]
        public string goBackPage;

        internal static ActivityConfig LoadFromRepo() {
            ActivityConfig ans = new ActivityConfig();
            ConstantRepo r = ConstantRepo.getInstance();
            Type t = typeof(ActivityConfig);
            FieldInfo[] fis = t.GetFields();
            foreach (FieldInfo fi in fis) {
                ConstAttribute ca = (ConstAttribute)Attribute.GetCustomAttribute(fi, typeof(ConstAttribute));
                if (ca != null) {
                    object o = r.opt(ca.key, null);
                    if (o != null) fi.SetValue(ans, o);
                }
            }
            return ans;
        }
    }
}

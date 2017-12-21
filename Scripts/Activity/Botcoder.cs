using UnityEngine;
using System.Collections;
using surfm.tool.i18n;
namespace com.surfm.account {
    public class Botcoder : Passworder {

        public int maxPassLength = 5;

        public static readonly string KEY_MAX_CODE = "PassLengthMax";

        internal override void vaildateOthers(string s) {
            if (s.Length > 5) {
                string msg = string.Format(I18n.get(KEY_MAX_CODE), maxPassLength);
                throw new Passworder.PassException(msg);
            }
        }

    }
}

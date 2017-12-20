using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using surfm.tool.i18n;

public class RePassworder : Passworder {

    private static readonly string KEY_PASS_NOT_SAME = "passNotSame";
    public InputField checkPassField;

    internal override void vaildateOthers(string s) {
        if (!s.Equals(checkPassField.text)) {
            throw new Passworder.PassException(I18n.get( KEY_PASS_NOT_SAME));
        }
    }


}

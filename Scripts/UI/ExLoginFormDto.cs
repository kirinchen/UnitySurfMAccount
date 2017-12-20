using surfm.tool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExLoginFormDto {

    public string uid;
    public string protectCode;

    public ExLoginFormDto() { }

    public ExLoginFormDto(string id) {
        uid = id;
        protectCode = CommUtils.getSha1(uid + "@S7j33GHcTsk82r");
    }
}

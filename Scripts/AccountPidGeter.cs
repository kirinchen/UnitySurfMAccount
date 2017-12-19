using RFNEet;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace com.surfm.account {
    public class AccountPidGeter : PidGeter {
        internal override string fetchPid() {
            return AccountLoader.getAccount().pid;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace com.surfm.account {
    public interface AccountDataLoad {

        string loadAccountFile();

        void saveAccountFile(string s);

         bool isExistAccountData();

    }
}

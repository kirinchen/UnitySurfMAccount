using com.surfm.account;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace com.surfm.account {
    public class UserObjDto  {
        public string raceUid;
        public string uid;
        public int updateNum;
        public DateTime updateAt;
        public DateTime createAt;
        public bool systemed;
        public bool combined;
        public JsonMap data;
        public double amount;
    }
}

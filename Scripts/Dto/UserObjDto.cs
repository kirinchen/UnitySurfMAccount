using com.surfm.account;
using surfm.dreamon;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static surfm.dreamon.PlayerDao;

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


        public PlayerTObj toPlayerTObj() {
            return new PlayerTObj(this);
        }

        public ETCItemTObj toETCItemTObj() {
            return new ETCItemTObj(this);
        }

    }
}

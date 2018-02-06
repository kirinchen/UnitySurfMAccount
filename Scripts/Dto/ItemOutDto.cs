using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace com.surfm.account {
    public class ItemOutDto  {
        public string uid;
        public string data;
        public int dataVersion;
        public int amount;

        public override string ToString() {
                  return "ItemOutDto [uid=" + uid + ", data=" + data + ", dataVersion=" + dataVersion + ", amount=" + amount
                + "]";
        }

    }
}

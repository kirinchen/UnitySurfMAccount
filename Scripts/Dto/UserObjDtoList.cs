using System.Collections.Generic;
using UnityEngine;

namespace com.surfm.account {

    public class UserObjDtoList : List<UserObjDto> {

        public UserObjDto findOneByRaceUid(string raceUid) {
            List<UserObjDto> uos = FindAll(u => {
                Debug.Log("u="+u);
                return string.Equals(raceUid, u.raceUid);
            });
            if (uos.Count == 0) return null;
            if (uos.Count == 1) return uos[0];
            throw new System.Exception("find geter 2 elemnt =" + uos);
        }

    }
}
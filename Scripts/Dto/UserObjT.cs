using UnityEngine;
using System.Collections;
namespace com.surfm.account {
    public class UserObjT<T> {

        private UserObjDto raw;
        private T obj;

        public UserObjT(UserObjDto r) {
            raw = r;
        }

        public T getObj() {
            if (obj == null) obj = raw.data.toObjByJson<T>();
            return obj;
        }

        public UserObjDto getRaw() {
            return raw;
        }

    }
}

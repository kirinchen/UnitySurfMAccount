namespace com.surfm.account {
    public class UserObjT<T> {

        public UserObjDto raw;
        public T obj;

        public UserObjT() { }

        public UserObjT(UserObjDto r) {
            raw = r;

        }

        public T getObj() {
            if (obj == null) obj = raw.data.toObjByJson<T>();
            return obj;
        }

        public void saveByObj() {
            raw.data = JsonMap.toJsonMap(obj);
        }


        public UserObjDto getRaw() {
            return raw;
        }

    }
}

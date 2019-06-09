using Newtonsoft.Json;
using surfm.tool;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace com.surfm.account {
    public class JsonMap : Dictionary<string, object> {

        private static ObscuredValueConverter obscuredValueConverter = new ObscuredValueConverter();

        public object toObjByJson(Type t) {
            string json = JsonConvert.SerializeObject(this, Formatting.None, obscuredValueConverter);
            return JsonConvert.DeserializeObject(json, t, obscuredValueConverter);
        }

        public T toObjByJson<T>() {
            string json = JsonConvert.SerializeObject(this, Formatting.None, obscuredValueConverter);
            return JsonConvert.DeserializeObject<T>(json, obscuredValueConverter);
        }

        public T toObj<T>(T t, Func<FieldInfo, object, object> valueFunc = null) {
            toObjRecursively(this, t, valueFunc);
            return t;
        }

        public void toObjRecursively(Dictionary<string, object> d, object o, Func<FieldInfo, object, object> valueFunc = null) {
            Type t = o.GetType();
            FieldInfo[] fs = t.GetFields();

            foreach (string ks in d.Keys) {

                object value = d[ks];
                FieldInfo fd = t.GetField(ks);
                if (value is Dictionary<string, object>) {
                    toObjRecursively((Dictionary<string, object>)value, fd.GetValue(o), valueFunc);
                } else {
                    fd.SetValue(o, getObj(fd, value, valueFunc));
                }

            }
        }

        private object getObj(FieldInfo fd, object value, Func<FieldInfo, object, object> valueFunc) {
            if (valueFunc != null) return valueFunc(fd, value);
            if (fd.FieldType.IsEnum && value is string) return Enum.Parse(fd.FieldType, (string)value);
            return value;
        }
    }
}
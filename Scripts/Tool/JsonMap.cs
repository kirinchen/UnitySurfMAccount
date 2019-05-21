using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace com.surfm.account {
    public class JsonMap : Dictionary<string, object> {



        public T toObj<T>(T t, Func<FieldInfo, object, object> valueFunc = null) {
            toObjRecursively(this, t, valueFunc);
            return t;
        }


        public void toObjRecursively(Dictionary<string, object> d, object o, Func<FieldInfo, object, object> valueFunc = null) {
            Type t = o.GetType();
            FieldInfo[] fs = t.GetFields();

            foreach (string ks in d.Keys) {

                object value = d[ks];
                FieldInfo fd = t.GetField(ks );
                if (value is Dictionary<string, object>) {
                    toObjRecursively((Dictionary<string, object>)value, fd.GetValue(o), valueFunc);
                } else {
                    object _v = valueFunc == null ? value : valueFunc(fd, value);
                    fd.SetValue(o, _v);
                }

            }
        }
    }
}
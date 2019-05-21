using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;

namespace com.surfm.account {
    public class JsonMap : Dictionary<string,object> {



        public T toObj<T>(  T t) {

            return t;
        }



        public void toObjRecursively<T>(Dictionary<string, object> d, T t) {

            


        }



    }
}
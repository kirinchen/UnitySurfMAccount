using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace com.surfm.account {
    public class DefaultAvatarUtils {

        public readonly static DefaultAvatarUtils instance = new DefaultAvatarUtils();
        public static string FOLDER = "Avatar";

        public struct Bundle {
            public Sprite sprite;
            public string path;
        }

        private List<Bundle> cache = null;

        private List<Bundle> _load() {
            if (cache == null) {
                cache = new List<Bundle>();
                Sprite[] ss = Resources.LoadAll<Sprite>(FOLDER);
                foreach (Sprite s in ss) {
                    Bundle b = new Bundle() {
                        sprite = s,
                        path = FOLDER + "/" + s.name
                    };
                    cache.Add(b);
                }
            }
            return cache;
        }


        public static List<Bundle> load() {
            return instance._load();
        }

        private Bundle _get(string path) {
            return load().Find(b => { return string.Equals(path, b.path); });
        }

        internal static Bundle get(string path) {
            return instance._get(path);
        }
    }
}

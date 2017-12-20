
using Newtonsoft.Json;
using surfm.tool;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
namespace com.surfm.account {
    public class AccountLoader {
        private static readonly string BYTE_KEY = "87654321";
        public static readonly string PATH = AccountConstant.getAccountLoadPath( "/sdcard/.Yoar/Account.ya");
        public readonly static AccountLoader instance = new AccountLoader();
        private Account account;
        private List<Action> observers = new List<Action>();

        private AccountLoader() { }


        private void saveCurrent() {
            account = getCurrentAccount();
            string plain = JsonConvert.SerializeObject(account);
            string s = CommUtils.encryptBase64(getSKey(), BYTE_KEY, plain);
            FileInfo file = new FileInfo(PATH);
            file.Directory.Create();
            File.WriteAllText(PATH, s);
            observers.ForEach(a => { a(); });
        }

        public void addObserver(Action a) {
            observers.Add(a);
        }

        public void removeObserver(Action a) {
            observers.Remove(a);
        }

        private Account getCurrentAccount() {
            if (account == null) {
                account = load();
            }
            return account;
        }

        private Account load() {
            FileInfo fi = new FileInfo(PATH);
            if (!fi.Exists) return createNewOne();
            try {
                string s = File.ReadAllText(PATH);
                string plain = CommUtils.decryptBase64(getSKey(), BYTE_KEY, s);
                return JsonConvert.DeserializeObject<Account>(plain);
            } catch (Exception e) {
                Debug.Log(e);
                return createNewOne();
            }
        }

        private Account createNewOne() {
            account = new Account();
            saveCurrent();
            return account;
        }

        public static string getSKey() {
            string ss = CommUtils.getSha1("IOn3r123" + SystemInfo.deviceUniqueIdentifier);
            return ss.Substring(0, 8);
        }

        public static Account getAccount() {
            return instance.getCurrentAccount();
        }

        public static string getPid() {
            return getAccount().pid;
        }

        public static void save() {
            instance.saveCurrent();
        }





    }
}

using surfm.tool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.Events;

namespace com.surfm.account {
    public class AvatarChooser : MonoBehaviour {
        public DialogManager dialogManager;
        private Image img;
        public UnityEvent onChanged;

        void Awake() {
            img = GetComponentInChildren<Image>();
        }

        void Start() {
            reflesh();
        }

        public void onClick() {
            showChoose();
        }

        public void showChoose() {
            showChoose(dialogManager, (a) => {
                a.setupAvatar(img);
                onChanged.Invoke();
            });

        }

        public static void showChoose(DialogManager dialogManager, Action<Account> onChanged) {
            List<ChooseDialog.RowData> l = DefaultAvatarUtils.load().ConvertAll(a => {
                return new ChooseDialog.RowData() {
                    sprite = a.sprite,
                    text = a.sprite.name,
                    target = a
                };
            });
            dialogManager.get<ChooseDialog>().
                setData(l).
                setCallback((b, d) => {
                    Account a = AccountLoader.getAccount();
                    a.setAvatar((DefaultAvatarUtils.Bundle)d.target);
                    onChanged(a);
                }).
                show(true);
        }

        private void onSelected(bool b, ChooseDialog.RowData d) {
            Account a = AccountLoader.getAccount();
            a.setAvatar((DefaultAvatarUtils.Bundle)d.target);
            a.setupAvatar(img);
            onChanged.Invoke();
        }

        internal void reflesh() {
            Account a = AccountLoader.getAccount();
            a.setupAvatar(img);
        }
    }
}

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

        public void onClick() {
            showChoose();
        }

        public void showChoose() {
            List<ChooseDialog.RowData> l = DefaultAvatarUtils.load().ConvertAll(a => {
                return new ChooseDialog.RowData() {
                    sprite = a.sprite,
                    text = a.sprite.name,
                    target = a
                };
            });
            dialogManager.get<ChooseDialog>().
                setData(l).
                setCallback(onSelected).
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

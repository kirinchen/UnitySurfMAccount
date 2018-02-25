using UnityEngine;
using System.Collections;
using System;
using surfm.tool;
using surfm.tool.i18n;
using UnityStomp;

namespace com.surfm.account {
    public class Signuper : PageHandler, AccountService.LoginHandler {
        public static readonly string KEY_UNNOWN_FAIL = "unknownFail";
        public static readonly string KEY_USER_EXIST = "userExist";
        public static readonly string KEY_BOT_ERROR = "codeError";
        public EmailInputer email;
        public RePassworder rePassworder;
        public Passworder password;
        public PhoneInputer phoner;
        public DBotCoder botcode;
        //private URestApi rest;
        private Toast toast { get { return Toast.getInstance(); } }
        private AccountManager am { get { return AccountManager.getInstance(); } }



        internal override void show() {
            base.show();
            botcode.reloadBotCode();
            am.backButton.gameObject.SetActive(true);
            am.backButton.onClick.RemoveAllListeners();
            am.backButton.onClick.AddListener(am.switchLoginPage);
        }

        public void onClick() {
            if (AccountConstant.PHONE_VAILD) {
                vaildPhone();
            } else {
                submit();
            }
        }

        private void vaildPhone() {
            if (validate()) {
                string rn = UidUtils.getRandomNumber(6);
                string msg = string.Format(I18n.get("phone code={0}"), rn);
                SMSService.getInstance().sendToRemote(phoner.getValue() + "", msg, () => {
                    DialogManager.instance.get<InputDialog>().show(I18n.get("Input Phone Code"), t => {
                        if (string.Equals(rn, t)) {
                            submit();
                        } else {
                            Toast.getInstance().show(I18n.get("The Phone Vaild Code Fail"));
                        }
                    });
                }, e => {
                    Toast.getInstance().show(e.ToString());
                });
            }
        }

        public void submit() {
            if (validate()) {
                Loading.getInstance().show(true);
                post2Remote();
            }
        }

        private bool validate() {
            return email.validate() & password.validate() & rePassworder.validate() & botcode.validate() & phoner.validate();
        }

        private void post2Remote() {
            UserSignupFormDto dto = new UserSignupFormDto();
            dto.email = email.getValue();
            dto.password = password.getValue();
            dto.botCodeKey = botcode.getKey();
            dto.botCodeValue = botcode.getValue();
            dto.phone = phoner.getValue() + "";

            //rest.postJson("/api/v1/public/signup", dto, onOk, onError);
            AccountService.getInstance().signup(dto, this);
        }


        private void handleBotcodeError(SurfMErrorDto dto) {
            botcode.alert(I18n.get(KEY_BOT_ERROR));
        }

        private void handleEmailExistError(SurfMErrorDto dto) {
            email.alert(I18n.get(KEY_USER_EXIST));
        }

        public void onOk(LoginResultDto dto) {
            Loading.getInstance().show(false);
            AccountManager.getInstance().switchHelloPage();
        }

        public void onException(SurfMErrorDto dto) {
            Debug.Log(dto);
            Loading.getInstance().show(false);
            if (dto.getSurfMError() == SurfMErrorDto.SurfMError.NONE) {
                toast.show(I18n.get(KEY_UNNOWN_FAIL));
            } else if (dto.getSurfMError() == SurfMErrorDto.SurfMError.BotcodeException) {
                handleBotcodeError(dto);
            } else if (dto.getSurfMError() == SurfMErrorDto.SurfMError.UserExistException) {
                handleEmailExistError(dto);
            }
        }

        public void onError(Exception e) {
            Loading.getInstance().show(false);
            Debug.LogException(e);
            toast.show(I18n.get(KEY_UNNOWN_FAIL));
        }
    }
}

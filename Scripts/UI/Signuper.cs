using UnityEngine;
using System.Collections;
using System;
using surfm.tool;
using surfm.tool.i18n;

namespace com.surfm.account {
    public class Signuper : PageHandler, AccountService.LoginHandler {
        public static readonly string KEY_UNNOWN_FAIL = "unknownFail";
        public static readonly string KEY_USER_EXIST = "userExist";
        public static readonly string KEY_BOT_ERROR = "codeError";
        public EmailInputer email;
        public RePassworder rePassworder;
        public Passworder password;
        public DBotCoder botcode;
        private URestApi rest;
        private Toast toast;
        private AccountManager am;

        new void Awake() {
            base.Awake();
            am = FindObjectOfType<AccountManager>();
            rest = FindObjectOfType<URestApi>();
            toast = FindObjectOfType<Toast>();
        }


        internal override void show() {
            base.show();
            botcode.reloadBotCode();
        }

        public void submit() {
            if (validate()) {
                Loading.getInstance().show(true);
                post2Remote();
            }
        }

        private bool validate() {
            return email.validate() & password.validate() & rePassworder.validate() & botcode.validate();
        }

        private void post2Remote() {
            AccountService.UserSignupFormDto dto = new AccountService.UserSignupFormDto();
            dto.email = email.getValue();
            dto.password = password.getValue();
            dto.botCodeKey = botcode.getKey();
            dto.botCodeValue = botcode.getValue();
            /* @TODO
            #if UNITY_FACEBOOK
                    dto.bindType = LoginType.FB;
                    dto.bindUid = FBManager.getInstance().getUserId();
            #elif UNITY_STANDALONE
                    if (SteamManager.Initialized) {
                        uint u = SteamUser.GetSteamID().GetAccountID().m_AccountID;
                        dto.bindType = LoginType.STEAM;
                        dto.bindUid = u.ToString();
                    }
            #endif      */

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
            /*TODO 登入完後*/
        }

        public void onException(SurfMErrorDto dto) {
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
            toast.show(I18n.get(KEY_UNNOWN_FAIL));
        }
    }
}

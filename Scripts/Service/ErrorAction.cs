﻿using System;
using UnityEngine;

namespace com.surfm.account {
    public class ErrorAction : URestApi.OnErrorB, AccountService.LoginHandler {
        private Action<SurfMErrorDto> sessionFailAction = (e) => { };
        private Action<SurfMErrorDto> serverErrorAction = (e) => { };
        private Action retry;

        public ErrorAction(URestApi.OnErrorBundle o) : base(o) {
        }

        public ErrorAction setSessionFailAction(Action<SurfMErrorDto> a) {
            sessionFailAction = a;
            return this;
        }

        public ErrorAction setServerErrorAction(Action<SurfMErrorDto> a) {
            serverErrorAction = a;
            return this;
        }

        public ErrorAction setRetryAction(Action r) {
            retry = r;
            sessionFailAction = (e) => {
                AccountService.getInstance().relogin(this);
            };
            return this;
        }

        protected override bool onCustom(URestApi.ErrorBundle eb) {
            try {
                SurfMErrorDto d = SurfMErrorDto.parse(eb.resp.DataAsText);
                if ((d.getSurfMError() == SurfMErrorDto.SurfMError.ApiKeyFailException || d.getSurfMError() == SurfMErrorDto.SurfMError.NoSuchElementException)) {
                    sessionFailAction(d);
                    return true;
                } else {
                    serverErrorAction(d);
                    return true;
                }
            } catch (Exception ex) {
                eb.e = ex;
            }
            return false;
        }

        public void onOk(LoginResultDto dto) {
            retry();
        }

        public void onException(SurfMErrorDto dto) {
            Debug.Log("onException=" + dto);
            serverErrorAction(dto);
        }

        public void onError(Exception e) {
            throw e;
        }
    }
}
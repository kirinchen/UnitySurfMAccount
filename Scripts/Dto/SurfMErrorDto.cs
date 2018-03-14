using UnityEngine;
using System.Collections;
using Newtonsoft.Json;

namespace com.surfm.account {
    public class SurfMErrorDto {
        public string timestamp;
        public int status;
        public string error;
        public string exception;
        public string message;
        public string path;

        public enum SurfMError {
            NONE, BotcodeException, UserExistException, AlreadyBindedException,
            PlayerDataNotExistException, ApiKeyFailException, NoSuchElementException, GiftGotedException, PhoneExistException
        }

        public bool isSuccess() {
            return status >= 200 && status < 300;
        }

        public SurfMError getSurfMError() {
            switch (exception) {
                case "net.surfm.account.exception.UserExistException":
                    return SurfMError.UserExistException;
                case "net.surfm.account.exception.BotcodeException":
                    return SurfMError.BotcodeException;
                case "net.surfm.account.exception.AlreadyBindedException":
                    return SurfMError.AlreadyBindedException;
                case "net.surfm.account.exception.PlayerDataNotExistException":
                    return SurfMError.PlayerDataNotExistException;
                case "net.surfm.account.exception.ApiKeyFailException":
                    return SurfMError.ApiKeyFailException;
                case "java.util.NoSuchElementException":
                    return SurfMError.NoSuchElementException;
                case "net.surfm.account.exception.GiftGotedException":
                    return SurfMError.GiftGotedException;
                case "net.surfm.account.exception.PhoneExistException":
                    return SurfMError.PhoneExistException;
            }
            return SurfMError.NONE;
        }

        public static SurfMErrorDto parse(string s) {
            return JsonConvert.DeserializeObject<SurfMErrorDto>(s);
        }

        public override string ToString() {
            return JsonConvert.SerializeObject(this);
        }

    }
}

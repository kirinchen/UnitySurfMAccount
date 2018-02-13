using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static com.surfm.account.AccountService;
//using static URestApi;

namespace com.surfm.account {
    public class ItemService : MonoBehaviour {

        public delegate void ReLoginAction(AccountService.LoginHandler ac);

        private URestApi rest;
        private ReLoginAction reLoginAction;

        internal void init(URestApi api, ReLoginAction r) {
            this.rest = api;
            reLoginAction = r;
        }

        public void getCoin(Action<ItemOutDto> oa, Action<SurfMErrorDto> sea, URestApi.OnErrorBundle ob) {
            ErrorAction ea = new ErrorAction(ob).setRetryAction(() => {
                getCoin(oa, sea, ob);
            }).setServerErrorAction(sea);
            rest.get("/v1/item/coin/", (msg) => {
                ItemOutDto dto = JsonConvert.DeserializeObject<ItemOutDto>(msg);
                oa(dto);
            }, ea.onError);
        }

    }
}

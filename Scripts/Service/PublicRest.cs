using com.surfm.rest;
using surfm.tool;

using System;
using static com.surfm.rest.URestApi;

namespace com.surfm.account {
    public class PublicRest  {

        private static PublicRest instance;

        private PublicRest() { }

         URestApi restApi = BeansRepo.bean<URestApi>(typeof(URestApi), "AccountServer");


        public void signUp(UserSignupFormDto dto, Action<Result> cb) {
            string urlTemp = "/api/v1/public/signup";
            restApi.postJson(urlTemp, dto, cb);
        }


        public PublicRest getInstance() {
            if (instance == null) {
                instance = new PublicRest();
            }
            return instance;
        }


    }
}

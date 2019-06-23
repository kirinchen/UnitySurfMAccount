using com.surfm.rest;
using surfm.tool;

using System;
using static com.surfm.rest.URestApi;

namespace com.surfm.account {
    public class PublicRest  {

        private static PublicRest instance;
        private URestApi restApi = BeansRepo.bean<URestApi>( "AccountServer");

        private PublicRest() {

        }

        public void signUp(UserSignupFormDto dto, Action<Result> cb) {
            string urlTemp = "v1/public/signup";
            restApi.postJson(urlTemp, dto, cb);
        }

        public void loginForGeneral(UserLoginFormDto dto,Action<TypeResult<LoginResultDto>> b) {
            string urlTemp = "v1/public/login?type=REST_API";
            restApi.postJson<LoginResultDto>(urlTemp, dto,b);
        }


        public static PublicRest getInstance() {
            if (instance == null) {
                instance = new PublicRest();
            }
            return instance;
        }

        public void clean() {
            instance = null;
        }
    }
}

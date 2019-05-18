using UnityEngine;
using System.Collections;
using static com.surfm.rest.URestApi;

namespace com.surfm.account {
    public class RestResult<R> {

        public Result result { get; private set; }

        internal RestResult(Result r) {
            result = r;
        }

        public R getBody() {
            return result.getBody<R>();
        }




    }
}

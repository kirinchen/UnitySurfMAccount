using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.surfm.account {
    public abstract class ServiceHandle : MonoBehaviour {

        public enum Mode {
            Base, Steam, FB
        }

        public string loginUrl = "/api/v1/public/login?type=REST_API";
        public Mode mode = Mode.Base;

        internal abstract void saveUserAndPass(LoginResultDto loginR, object d);

        internal abstract object getUserLoginFormDto();

        internal abstract object setupLoginDto(object d);

        internal abstract bool isLoaded();
    }
}

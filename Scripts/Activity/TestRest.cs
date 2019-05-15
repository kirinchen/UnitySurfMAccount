using UnityEngine;
using System.Collections;
using com.surfm.rest;

public class TestRest : MonoBehaviour {

    private URestApi rest;

    void Start () {
        //rest = FindObjectOfType<URestApi>();
        rest.authorization = "JSESSIONID=44D43A26339EFCD632CC0ADEFCE3FC67";
    }

    public void onClick() {
        //rest.postJson("/api/v1/account/test",null,(ok)=> {
        //    Debug.Log("ok="+ok);
        //},(m, s, r,e) => {
        //    Debug.Log("error="+m+" t="+r.Message);
        //});
    }
	

}

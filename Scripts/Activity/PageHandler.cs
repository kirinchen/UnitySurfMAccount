using GUIAnimator;
using UnityEngine;
using System.Collections;
using System;
using GUIAnimator;

public class PageHandler : MonoBehaviour {

    private GAui gaui;

    public void Awake() {
        gaui = GetComponent<GAui>();
    }

    internal virtual void show() {
        gaui.PlayInAnims(eGUIMove.Self);
    }

    internal virtual void hide() {
        try {
            gaui.PlayOutAnims(eGUIMove.Self);
        } catch (Exception e) {
            Debug.Log(gameObject.name);
        }
    }
}

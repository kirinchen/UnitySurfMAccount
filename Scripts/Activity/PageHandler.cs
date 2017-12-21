using UnityEngine;
using System.Collections;
using System;

public class PageHandler : MonoBehaviour {

    private GAui gaui;

    public void Awake() {
        gaui = GetComponent<GAui>();
    }

    internal virtual void show() {
        gaui.MoveIn(GUIAnimSystem.eGUIMove.Self);
    }

    internal virtual void hide() {
        try {
            gaui.MoveOut(GUIAnimSystem.eGUIMove.Self);
        } catch (Exception e) {
            Debug.Log(gameObject.name);
        }
    }
}

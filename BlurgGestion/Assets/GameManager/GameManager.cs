using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager I;
    public BuildingManager bM;
    public RessourceManager rM;
    public InputManager iM;

    private void Awake () {
        if (I == null) { I = this;}
    }

    void Update () {
        iM.Frame ();
        bM.Frame ();
        rM.Frame ();
    }
}

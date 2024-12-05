using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager I;
    public BuildingManager bM;
    public RessourceManager rM;
    public InputManager iM;
    public UIManager uiM;

    private void Awake () {
        if (I == null) { I = this;}
    }

    public void Start () {
        // Init
        bM.Init ();
        uiM.Init ();

        bM.SetupScene ();
        Camera.main.transform.position = new Vector3 (-60f, -60f);
    }
    void Update () {
        iM.Frame ();
        Building[] _builtBuildings = bM.Frame ();
        rM.Frame (_builtBuildings);
        uiM.Frame ();
    }
}

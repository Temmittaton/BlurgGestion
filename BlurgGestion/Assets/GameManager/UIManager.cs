using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
    public GameObject canvas;

    public void Init () {
        canvas = GameObject.Find ("Canvas");
    }
}

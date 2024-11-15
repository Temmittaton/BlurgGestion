using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public GameObject canvas;

    public void Init () {
        canvas = GameObject.Find ("Canvas");
    }

    public void Frame () {
        // Population (constant)
        float num = GameManager.I.rM.peopleAmount;
        canvas.transform.GetChild (0).GetChild (0).GetComponent<Text> ().text = num.ToString ();

        // Stone (ressource)
        num = GameManager.I.rM.stoneAmount;
        canvas.transform.GetChild (0).GetChild (1).GetComponent<Text> ().text = num.ToString ();

        // Blurg (prodution)
        num = GameManager.I.rM.blurgGen;
        canvas.transform.GetChild (0).GetChild (2).GetComponent<Text> ().text = num.ToString () + " /s";

        // Food (production)
        num = GameManager.I.rM.foodGen;
        canvas.transform.GetChild (0).GetChild (3).GetComponent<Text> ().text = num.ToString () + " /s";

        // Electricity (production)
        num = GameManager.I.rM.electricityGen;
        canvas.transform.GetChild (0).GetChild (4).GetComponent<Text> ().text = num.ToString () + " /s";
    }
}

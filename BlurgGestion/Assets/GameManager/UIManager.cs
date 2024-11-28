using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public GameObject canvas;
    public Panel openPanel = Panel.None;
    public BuildItem[] buildItems;
    public GameObject[] buildItemObjects;
    public GameObject buildItemPrefab, menuAnchor;

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

    public void BuildButtonPressed () {
        // Disable build button
        canvas.transform.GetChild (1).gameObject.SetActive (false);

        // Open build menu
        canvas.transform.GetChild (2).gameObject.SetActive (true);

        // Reposition anchor
        menuAnchor.transform.position = new Vector3 (96, 16, 0);

        // Summon buttons
        buildItemObjects = new GameObject[buildItems.Length];
        for (int i = 0; i < buildItems.Length; i++) {
            buildItemObjects[i] = Instantiate (buildItemPrefab, canvas.transform.GetChild (2));
            buildItemObjects[i].transform.position = menuAnchor.transform.position + Vector3.right * i * 256;
            buildItemObjects[i].GetComponent<BuildMenuItemScript> ().Init (buildItems[i]);
        }
    }
    public void BuiltMenuExistButtonPressed () {
        // Disable build button
        canvas.transform.GetChild (1).gameObject.SetActive (true);

        // Open build menu
        canvas.transform.GetChild (2).gameObject.SetActive (false);

        // Destroy buttons
        for (int i = 0; i < buildItems.Length; i++) {
            Destroy (buildItemObjects[i]);
        }
    }
}

public enum Panel {
    None,
    Build,
}
[System.Serializable]
public struct BuildItem {
    public int ID;
    public int[] costs;
    public Ressource[] ressources;
}
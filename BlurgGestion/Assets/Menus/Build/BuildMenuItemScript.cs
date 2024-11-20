using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildMenuItemScript : MonoBehaviour {
    private BuildItem item;

    public void Init (BuildItem _item) {
        item = _item;
        string name = GameManager.I.bM.buildings[item.ID].name;
        transform.GetChild (2).GetComponent<Text> ().text = name;
    }
}
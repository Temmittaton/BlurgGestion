using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingScript : MonoBehaviour {
    public Building building;
    public int level;
    public Vector2 pos;

    public void Init () {
        string _path = "Buildings/" + building.name + "/" + level.ToString () + "/sprite";
        gameObject.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> (_path);
    }
}

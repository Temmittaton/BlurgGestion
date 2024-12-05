using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BuildingManager : MonoBehaviour {
    private int GRID_SIZE = 1;
    private int TERRAIN_WIDTH = 16;
    [SerializeField] private GameObject template;
    [SerializeField] private GameObject selectIndicator;
    private int selectedBuilding = -1;
    private Vector2 currentMove = Vector2.zero;
    public GameObject[] builtBuildings;
    public Building[] buildings;
    public GameObject ghostBuilding;

    private bool CheckPosition (Vector3 pos, Vector2 size) {
        for (int i = 0; i < builtBuildings.Length; i++) {
            if (builtBuildings[i] == null) { continue; }

            Vector3 _pos = builtBuildings[i].transform.position;

            Vector2 _size = builtBuildings[i].GetComponent<BuildingScript> ().building.size;
            bool collision = (pos.x + size.x > _pos.x) && (_pos.x + _size.x > pos.x) && (pos.y + size.y > _pos.y) && (_pos.y + _size.y > pos.y);
            if (collision) {
                return false;
            }
        }

        return true;
    }
    public void SetupScene () {
        // TODO : load save and summon + place all buildings
    }
    public void AddBuilding (Building _building) {
        for (int i = 0; i < buildings.Length; i++) {
            if (builtBuildings[i] != null) {continue;}

            GameObject _o = Instantiate (template);
            _o.GetComponent<BuildingScript> ().building = _building;

            Vector2 _pos = Vector2.negativeInfinity;
            // Get position
            for (int j = 0; j < TERRAIN_WIDTH * 2; j++) {
                for (int k = 0; k < TERRAIN_WIDTH * 2; k++) {
                    Vector2 pPos = j * (2 * (j % 2) - 1) * Vector2.right + k * (2 * (k % 2) - 1) * Vector2.up;
                    Debug.Log (pPos);
                    if (CheckPosition (pPos.x * new Vector2 (-1, -1) + pPos.y * new Vector2 (1, -1), _building.size)) {
                        _pos = pPos;
                        break;
                    }
                }
            }
            if (_pos == Vector2.negativeInfinity) { return; }

            _o.transform.position = _pos.x * new Vector2 (-1, -1) + _pos.y * new Vector2 (1, -1);
            _o.GetComponent<BuildingScript> ().pos = _pos;
            _o.GetComponent<BuildingScript> ().level = 1;
            _o.GetComponent<BuildingScript> ().Init ();
            _o.transform.localScale = Vector3.one * 8;
            builtBuildings[i] = _o;

            GameManager.I.iM.CenterCameraOn (_o.transform.position);

            break;
        }
    }
    public void MoveBuilding (Vector2 move) {
        if (currentMove == Vector2.zero) {
            // If just started moving
            ghostBuilding = Instantiate (template);
            ghostBuilding.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, .5f);
            ghostBuilding.transform.position = builtBuildings [selectedBuilding].transform.position;
        }

        currentMove += move;
        ghostBuilding.transform.position = builtBuildings[selectedBuilding].transform.position + new Vector3 ((int)currentMove.x, (int)currentMove.y, 0) * GRID_SIZE;

        // Check current position
        bool isPosValid = CheckPosition (ghostBuilding.transform.position, builtBuildings[selectedBuilding].GetComponent<BuildingScript> ().building.size);
        if (isPosValid) {
            selectIndicator.GetComponent<SpriteRenderer> ().color = Color.green;
        }
        else {
            selectIndicator.GetComponent<SpriteRenderer> ().color = Color.red;
        }
    }
    public void CancelBuildingMovement () {
        ghostBuilding.SetActive (false);
        currentMove = Vector2.zero;
    }
    public int SelectBuilding (Vector2 worldPos) {
        Vector2 touchPos = worldPos.x * new Vector2 (-1, -1) + worldPos.y * new Vector2 (1, -1);

        for (int i = 0; i < builtBuildings.Length; i++) {
            if (builtBuildings[i] == null) {continue;}

            Vector2 _pos = builtBuildings[i].GetComponent<BuildingScript> ().pos;
            Vector2 _size = builtBuildings[i].GetComponent<BuildingScript> ().building.size;
            bool hits = (touchPos.x >= _pos.x) && (touchPos.y >= _pos.y);
            hits = hits && (touchPos.x <= _pos.x + _size.x) && (touchPos.y <= _pos.y + _size.y);
            if (hits) {
                // summon selectedindicator
                selectIndicator.SetActive (true);
                selectIndicator.transform.position = builtBuildings [i].transform.position;
                selectIndicator.transform.localScale = builtBuildings[i].GetComponent<BuildingScript> ().building.size;

                selectedBuilding = i;
                return i;
            }
        }

        selectIndicator.SetActive (false);
        selectedBuilding = -1;
        return -1;
    }
    public void Init () {
        builtBuildings = new GameObject[TERRAIN_WIDTH * TERRAIN_WIDTH];
    }
    public Building[] Frame () {
        Building[] _builtBuildings = new Building[builtBuildings.Length];

        for (int i = 0; i < builtBuildings.Length; i++) {
            if (builtBuildings[i] == null) { continue; }
            _builtBuildings [i] = builtBuildings [i].GetComponent<BuildingScript> ().building;
        }

        return _builtBuildings;
    }
}

public enum BuildingType {
    StatRaiser,
    Stock,
    Production,
    Defense,
}
public enum Ressource {
    Population,
    Food,
    Electricity,
    Blurg,
    Stone,
}
[System.Serializable]
public class Building {
    public string name;
    public Vector2 size;
    public BuildingType type;
    public Ressource ressource;
    public int stat; // Capacity if stock, gen/sec if production, stat if statraiser, dmg if defense
}
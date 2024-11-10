using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BuildingManager : MonoBehaviour {
    private int GRID_SIZE = 1;
    private int TERRAIN_WIDTH = 64;
    [SerializeField] private GameObject template;
    public GameObject[] builtBuildings;
    public Building[] buildings;

    public void AddBuilding (Building _building, int2 _pos) {
        for (int i = 0; i < buildings.Length; i++) {
            if (builtBuildings[i] != null) {continue;}

            GameObject _o = Instantiate (template);
            _o.GetComponent<BuildingScript> ().building = _building;
            _o.transform.position = _pos.x * new Vector2 (-1, -1) + _pos.y * new Vector2 (1, -1);
            _o.GetComponent<BuildingScript> ().pos = _pos.x * new Vector2 (-1, -1) + _pos.y * new Vector2 (1, -1);
            _o.GetComponent<BuildingScript> ().Init ();
        }
    }
    public void MoveBuilding (Vector2 move) {
        // TODO
    }
    public int SelectBuilding (Vector2 worldPos) {
        Vector2 touchPos = worldPos.x * new Vector2 (-1, -1) + worldPos.y * new Vector2 (1, -1);

        for (int i = 0; i < buildings.Length; i++) {
            if (buildings[i] == null) {continue;}

            Vector2 _pos = builtBuildings[i].GetComponent<BuildingScript> ().pos;
            Vector2 _size = builtBuildings[i].GetComponent<BuildingScript> ().building.size;
            bool hits = (touchPos.x >= _pos.x) && (touchPos.y >= _pos.y);
            hits = hits && (touchPos.x <= _pos.x + _size.x) && (touchPos.y <= _pos.y + _size.y);
            if (hits) {
                return i;
            }
        }

        return -1;
    }
    public void Init () {
        builtBuildings = new GameObject[TERRAIN_WIDTH * TERRAIN_WIDTH];
    }
    public void Frame () {

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
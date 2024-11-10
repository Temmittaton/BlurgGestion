using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RessourceManager : MonoBehaviour {
    [Header ("Ressources amount :")]
    public int stoneAmount;
    public int foodAmount;
    public int peopleAmount;
    public int blurgAmount;
    [Header ("Ressources max amount :")]
    public int stoneMaxAmount;
    public int foodMaxAmount;
    public int blurgMaxAmount;
    [Header ("Ressources generation :")]
    public float stoneGen;
    public float foodGen;
    public float electricityGen;
    public float blurgGen;

    public void ActualiseMaxAmounts (GameObject[] buildings) {
        stoneMaxAmount = 0;
        foodMaxAmount = 0;
        blurgMaxAmount = 0;

        for (int i = 0; i < buildings.Length; ++i) {
            if (buildings[i] == null) { continue; }

            if (buildings[i].GetComponent<BuildingScript> ().building.type != BuildingType.Stock) {
                continue;
            }
            else {
                switch (buildings[i].GetComponent<BuildingScript> ().building.ressource) {
                    case (Ressource.Blurg):
                        blurgAmount += buildings[i].GetComponent<BuildingScript> ().building.stat;
                        break;
                    case (Ressource.Food):
                        foodAmount += buildings[i].GetComponent<BuildingScript> ().building.stat;
                        break;
                    case (Ressource.Stone):
                        stoneAmount += buildings[i].GetComponent<BuildingScript> ().building.stat;
                        break;
                }
            }
        }
    }
    public void ActualiseGen (GameObject[] buildings) {
        stoneGen = 0;
        foodGen = 0;
        electricityGen = 0;
        blurgGen = 0;

        for (int i = 0; i < buildings.Length; ++i) {
            if (buildings[i] == null) { continue; }

            if (buildings[i].GetComponent<BuildingScript> ().building.type != BuildingType.Production) {
                continue;
            }
            else {
                switch (buildings[i].GetComponent<BuildingScript> ().building.ressource) {
                    case (Ressource.Stone):
                        stoneGen += buildings[i].GetComponent<BuildingScript> ().building.stat;
                        break;
                    case (Ressource.Food):
                        foodGen += buildings[i].GetComponent<BuildingScript> ().building.stat;
                        break;
                    case (Ressource.Electricity):
                        electricityGen += buildings[i].GetComponent<BuildingScript> ().building.stat;
                        break;
                    //also blurg when ennemies are implemented
                }
            }
        }
    }
    public void Frame () {

    }
}

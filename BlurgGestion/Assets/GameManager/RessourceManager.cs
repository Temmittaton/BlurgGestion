using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RessourceManager : MonoBehaviour {
    [Header ("Ressources amount :")]
    public float stoneAmount;
    public float foodAmount;
    public float peopleAmount;
    public float blurgAmount;
    [Header ("Ressources max amount :")]
    public int stoneMaxAmount;
    public int foodMaxAmount;
    public int blurgMaxAmount;
    [Header ("Ressources generation :")]
    public float stoneGen;
    public float foodGen;
    public float electricityGen;
    public float blurgGen;

    public void ActualiseMaxAmountsAndStats (Building[] buildings) {
        stoneMaxAmount = 0;
        foodMaxAmount = 0;
        blurgMaxAmount = 0;
        peopleAmount = 0;

        for (int i = 0; i < buildings.Length; ++i) {
            if (buildings[i] == null) { continue; }

            if (buildings[i].type != BuildingType.Stock && buildings[i].type != BuildingType.StatRaiser) {
                continue;
            }
            else {
                switch (buildings[i].ressource) {
                    case (Ressource.Blurg):
                        blurgAmount += buildings[i].stat;
                        break;
                    case (Ressource.Food):
                        foodAmount += buildings[i].stat;
                        break;
                    case (Ressource.Stone):
                        stoneAmount += buildings[i].stat;
                        break;
                    case Ressource.Population:
                        peopleAmount += buildings[i].stat;
                        break;
                }
            }
        }
    }
    public void ActualiseGen (Building[] buildings) {
        stoneGen = 0;
        foodGen = 0;
        electricityGen = 0;
        blurgGen = 0;

        for (int i = 0; i < buildings.Length; ++i) {
            if (buildings[i] == null) { continue; }

            if (buildings[i].type != BuildingType.Production) {
                continue;
            }
            else {
                switch (buildings[i].ressource) {
                    case (Ressource.Stone):
                        stoneGen += buildings[i].stat;
                        break;
                    case (Ressource.Food):
                        foodGen += buildings[i].stat;
                        break;
                    case (Ressource.Electricity):
                        electricityGen += buildings[i].stat;
                        break;
                    //also blurg when ennemies are implemented
                }
            }
        }
    }
    private void ActualiseRessources () {
        stoneAmount = Mathf.Min (stoneAmount + stoneGen * Time.deltaTime, stoneMaxAmount);
        foodAmount = Mathf.Min (foodAmount + foodGen * Time.deltaTime, foodMaxAmount);
    }

    public void Frame (Building[] builtBuildings) {
        ActualiseGen (builtBuildings);
        ActualiseMaxAmountsAndStats (builtBuildings);
        ActualiseRessources ();
    }
}

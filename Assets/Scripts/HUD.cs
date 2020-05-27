using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        getStats = getPlayer.GetComponent<PlayerStats>();
    }

    public Slider getSliderHealth, getSliderFood, getSliderWater;
    public Text getText;
    public GameObject getPlayer;
    private PlayerStats getStats;

    private string textInfo = "";

    // Update is called once per frame
    void Update()
    {
        getSliderHealth.value = getStats.health;
        getSliderFood.value = getStats.food;
        getSliderWater.value = getStats.water;

        TextInformation();
    }

    public void TextInformation()
    {
        textInfo = "";  // Resets and prevents strings below adding up on string
        /*
        textInfo += "Ammo: " + getStats.ammo + "\n";
        textInfo += "\n";
        textInfo += "Foodcans: " + getStats.foodCount + "\n";
        textInfo += "Waterbottles: " + getStats.waterCount + "\n";
        textInfo += "Medkits: " + getStats.medKitCount + "\n";
        */
        textInfo += getStats.ammo + "\n";
        textInfo += getStats.foodCount + "\n";
        textInfo += getStats.waterCount + "\n";
        textInfo += getStats.medKitCount + "\n";

        getText.text = textInfo;
    }
}

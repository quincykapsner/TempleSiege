using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarsManager : MonoBehaviour
{
    private Statue statue;

    public Slider healthBar;
    public Slider chargeBar;

    // Start is called before the first frame update
    void Start()
    {
        statue = FindObjectOfType<Statue>();
        healthBar.maxValue = statue.maxHealth;
        chargeBar.maxValue = 100;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = statue.health;
        chargeBar.value = statue.charge;
    }
}

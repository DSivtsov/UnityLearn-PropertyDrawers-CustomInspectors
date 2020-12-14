using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class TestPlayerStatsClass : MonoBehaviour
{
    [Unit("m/s²")]
    public float EarthGravity = 9.80665f;



    //Make the private field of our PlayerStats struct visible in the Inspector
    //by applying [SerializeField] attribute to it
    //[SerializeField]
    //private PlayerStats[] FirstPlayerStats = new PlayerStats[]
    //{
    //    new PlayerStats { namePlayer = "Alfa", movementSpeed = 1, hitPoints = 1, hasHealthPotion = true },
    //    new PlayerStats { namePlayer = "Beta", movementSpeed = 10, hitPoints = 5, hasHealthPotion = false },
    //    new PlayerStats { namePlayer = "Gamma", movementSpeed = 100, hitPoints = 15, hasHealthPotion = true }
    //};

}

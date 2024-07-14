using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightManager : MonoBehaviour
{
    public static FightManager instance;

    private int maxStamina = 5;

    private int playerStamina;
    private int enemyStamina;

    public Slider slider;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Setup();
    }

    public void Setup()
    {
        playerStamina = enemyStamina = maxStamina;
    }

    public void Resolve()
    {
        Debug.Log(slider.value);
    }
}

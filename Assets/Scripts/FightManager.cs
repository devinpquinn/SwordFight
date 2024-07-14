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

    public void Constrain()
    {
        if(slider.value > playerStamina)
        {
            slider.value = playerStamina;
        }
    }

    public void Release()
    {
        if(slider.value == -1)
        {
            //play a sound effect
        }
        else
        {
            Resolve((int)slider.value);
        }
    }

    public void Resolve(int spent)
    {
        Debug.Log(slider.value);
    }
}

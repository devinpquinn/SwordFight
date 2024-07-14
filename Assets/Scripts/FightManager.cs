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

    public Image playerStaminaBar;
    public Image enemyStaminaBar;

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

    public void Resolve(int playerSpent)
    {
        int enemySpent = EnemyAction();

        if(playerSpent < 0 && enemySpent < 0)
        {
            //disengage
            playerStamina = enemyStamina = maxStamina;
        }
        else
        {
            if (playerSpent > 0)
            {
                playerStamina -= playerSpent;
            }
            else if (playerSpent < 0)
            {
                if(playerStamina < maxStamina)
                {
                    playerStamina++;
                }
            }
            if (enemySpent > 0)
            {
                enemyStamina -= enemySpent;
            }
            else if (enemySpent < 0)
            {
                if (enemyStamina < maxStamina)
                {
                    enemyStamina++;
                }
            }
        }

        UpdateBars(playerStamina, enemyStamina);
    }

    public void UpdateBars(int playerAmt, int enemyAmt)
    {
        playerStaminaBar.fillAmount = (float) playerAmt / maxStamina;
        enemyStaminaBar.fillAmount = (float) enemyAmt / maxStamina;
    }

    public int EnemyAction()
    {
        return Random.Range(-2, enemyStamina);
    }
}

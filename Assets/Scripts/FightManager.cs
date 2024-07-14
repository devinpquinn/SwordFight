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

    private int position;
    public Image[] trackMarkers = new Image[5];

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
        position = 3;
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
            Debug.Log("DISENGAGE!");
            playerStamina = enemyStamina = maxStamina;
            position = 3;
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

            if(playerSpent == enemySpent)
            {
                //clash
                Debug.Log("CLASH!");

                if(Random.Range(0, 2) == 1)
                {
                    //player wins clash
                    playerSpent++;
                }
                else
                {
                    //enemy wins clash
                    enemySpent++;
                }
            }
            else
            {
                if(playerSpent > enemySpent)
                {
                    //player pushes back enemy
                    position++;
                }
                else
                {
                    //enemy pushes back player
                    position--;
                }

                //check for wound
                if(position > 5)
                {
                    Debug.Log("ENEMY HIT!");
                    playerStamina = enemyStamina = maxStamina;
                    position = 3;
                }
                else if(position < 1)
                {
                    Debug.Log("PLAYER HIT!");
                    playerStamina = enemyStamina = maxStamina;
                    position = 3;
                }
            }
        }

        UpdateBars(playerStamina, enemyStamina);

        UpdateTrack(position);

        slider.value = -1;
    }

    public void UpdateBars(int playerAmt, int enemyAmt)
    {
        playerStaminaBar.fillAmount = (float) playerAmt / maxStamina;
        enemyStaminaBar.fillAmount = (float) enemyAmt / maxStamina;
    }

    public void UpdateTrack(int pos)
    {
        for(int i = 0; i < trackMarkers.Length; i++)
        {
            trackMarkers[i].color = new Color(0.2f, 0.2f, 0.2f);
            if(pos - 1 == i)
            {
                trackMarkers[i].color = new Color(0.6f, 0.6f, 0.6f);
            }
        }
    }

    public int EnemyAction()
    {
        return Random.Range(-2, enemyStamina);
    }
}

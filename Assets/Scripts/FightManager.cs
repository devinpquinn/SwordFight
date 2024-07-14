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

    public TMPro.TextMeshProUGUI playerStaminaLabel;
    public TMPro.TextMeshProUGUI enemyStaminaLabel;

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

            UpdateLabels("<color=yellow>Disengage!</color>", "<color=yellow>Disengage!</color>");
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

            if(playerSpent != enemySpent)
            {
                if (playerSpent > enemySpent)
                {
                    //player pushes back enemy
                    position++;
                }
                else
                {
                    //enemy pushes back player
                    position--;
                } 
            }
            else
            {
                //clash
                Debug.Log("CLASH!");

                if (Random.Range(0, 2) == 1)
                {
                    //player wins clash
                    position++;
                    UpdateLabels("<color=green>CLASH " + playerSpent + " - Win!</color>", "<color=red>CLASH " + enemySpent + " - Lose!</color>");
                }
                else
                {
                    //enemy wins clash
                    position--;
                    UpdateLabels("<color=red>CLASH " + playerSpent + " - Lose!</color>", "<color=green>CLASH " + enemySpent + " - Win!</color>");
                }
            }

            //check for wound
            if (position > 5)
            {
                Debug.Log("ENEMY HIT!");
                playerStamina = enemyStamina = maxStamina;
                position = 3;

                UpdateLabels("<color=green>" + playerSpent.ToString() + "</color>", "<color=red>" + (enemySpent < 0 ? "Defend" : enemySpent) + " - HIT!!</color>");
            }
            else if (position < 1)
            {
                Debug.Log("PLAYER HIT!");
                playerStamina = enemyStamina = maxStamina;
                position = 3;

                UpdateLabels("<color=red>" + (playerSpent < 0 ? "Defend" : playerSpent) + " - HIT!!</color>", "<color=green>" + enemySpent.ToString() + "</color>");
            }
            else if(playerSpent > enemySpent)
            {
                UpdateLabels("<color=green>" + playerSpent + " - Win!</color>", "<color=red>" + (enemySpent < 0 ? "Defend" : enemySpent) + " - Lose!</color>");
            }
            else if (enemySpent > playerSpent)
            {
                UpdateLabels("<color=red>" + (playerSpent < 0 ? "Defend" : playerSpent) + " - Lose!</color>", "<color=green>" + enemySpent + " - Win!</color>");
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

    public void UpdateLabels(string playerLabel, string enemyLabel)
    {
        playerStaminaLabel.text = playerLabel;
        enemyStaminaLabel.text = enemyLabel;

        playerStaminaLabel.gameObject.SetActive(false);
        playerStaminaLabel.gameObject.SetActive(true);

        enemyStaminaLabel.gameObject.SetActive(false);
        enemyStaminaLabel.gameObject.SetActive(true);
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

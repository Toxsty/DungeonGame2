using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerStats : MonoBehaviour
{
    public Text HealthText;
    public Text ArmorText;

    private float MaxHealth = 100;
    private float Health = 100;
    private float Armor = 0;
    private float DamageReduction = 0;

    private int PlayerLevel = 1;
    private int PlayerXp = 0;
    private int nextLevelXp = 100;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Health < MaxHealth)
        {
            Health += 0.5f*Time.deltaTime;
            if (Health > MaxHealth)
                Health = MaxHealth;
            SetHealthText();
        }
        if (Input.GetKeyUp(KeyCode.L))
        {
            LevelUp();
        }
        if (Input.GetKeyUp(KeyCode.G))
        {
            PlayerDamage(10);
        }
    }

    public void resetHealth()
    {
        Health = MaxHealth;
        SetHealthText();
    } 

    public void SetPlayerHealth(float pHealth)
    {
        Health = pHealth;
        if (Health <= 0)
        {
            Health = 0;
        }
        SetHealthText();
    }

    public void AddPlayerHealth(float pHealth)
    {
        Health = Health + pHealth;
        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }
        SetHealthText();
    }

    public void SetPlayerArmor(float pArmor)
    {
        Armor = pArmor;
        DamageReduction = 5 / (-(float)Math.Sqrt(Armor + 25)) + 1;
    }

    public void PlayerDamage(float pDamage)
    {
        Health = Health - pDamage * (1 - DamageReduction);
        if (Health <= 0)
        {
            Health = 0;
        }
        SetHealthText();
    }

    public void LevelUp()
    {
        PlayerLevel++;
        MaxHealth += 10;
        Health += 10;
        SetPlayerArmor(Armor + 1);
        
    }

    public int getPlayerLevel()
    {
        return PlayerLevel;
    }

    public void AddPlayerXp(int pXp)
    {
        PlayerXp += pXp;
        if (PlayerXp >= nextLevelXp)
        {
            LevelUp();
        }
        PlayerXp = PlayerXp - nextLevelXp;
        nextLevelXp = 100 * PlayerLevel * PlayerLevel;
    }

    public void SetHealthText()
    {
        HealthText.text = (int)Health + "/" + (int)MaxHealth;
        ArmorText.text = (int)Armor + "";
    }
}

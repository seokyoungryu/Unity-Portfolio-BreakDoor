
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    private StatusManager statusManager;

    [SerializeField] private Weapon[] weapons;

    private void Awake()
    {
        statusManager = FindObjectOfType<StatusManager>();
    }
    private void Start()
    {
        StatusUpdate();
    }

    private void Update()
    {
        //StatusUpdate();
    }

    public void StatusUpdate()
    {
        SetAtkAllWeapons();
        SetAtkSpeedAllWeapons();
        SetWeaponRange();
        SetFarSpeed();
    }

    public void SetAtkAllWeapons()
    {
        weapons[GameManager.SWORD].typeATK = statusManager.GetSword_Damage();
        // weapons[GameManager.SWORD].elseTypeATK = (statusManager.GetSword_Damage() * 0.1f > 1) ? statusManager.GetSword_Damage() * 0.1f : 1;
        weapons[GameManager.SWORD].elseTypeATK = 1;

        weapons[GameManager.GUN].typeATK = statusManager.GetGun_Damage();
        // weapons[GameManager.GUN].elseTypeATK = (statusManager.GetGun_Damage() * 0.1f > 1) ? statusManager.GetGun_Damage() * 0.1f : 1;
        weapons[GameManager.GUN].elseTypeATK = 1;

        weapons[GameManager.GAUNTLET].typeATK = statusManager.GetGauntlet_Damage();
        //weapons[GameManager.GAUNTLET].elseTypeATK = (statusManager.GetGauntlet_Damage() * 0.1f > 1) ? statusManager.GetGauntlet_Damage() * 0.1f : 1;
        weapons[GameManager.GAUNTLET].elseTypeATK = 1;

    }

    public void SetAtkSpeedAllWeapons()
    {
        weapons[GameManager.SWORD].atkDelay = statusManager.GetSword_Rate();
        weapons[GameManager.GUN].atkDelay = statusManager.GetGun_Rate();
        weapons[GameManager.GAUNTLET].atkDelay = statusManager.GetGauntlet_Rate();
    }

    public void SetWeaponRange()
    {
        weapons[GameManager.SWORD].range = statusManager.GetSword_Range();
        weapons[GameManager.GUN].range = statusManager.GetGun_Range();
        weapons[GameManager.GAUNTLET].range = statusManager.GetGauntlet_Range();


    }

    public void SetFarSpeed()
    {
        weapons[GameManager.SWORD].farSpeed = statusManager.GetSword_FarSpeed();
        weapons[GameManager.GUN].farSpeed = statusManager.GetGun_FarSpeed();
        weapons[GameManager.GAUNTLET].farSpeed = statusManager.GetGauntlet_FarSpeed();
    }


    public bool IsCritical()
    {
        float chance = Random.Range(10f, 100f);
        if (chance > statusManager.GetCritical_Chance())
        {
            return false;

        }
        else if (chance <= statusManager.GetCritical_Chance())
        {
            return true;

        }

        return false;
    }

    public float CriticalDmg(float dmg, bool chance)
    {
        if (chance)
        {
            float criticalDmg = (dmg * (1 + (statusManager.GetCritical_Damage() * 0.01f)));
            return criticalDmg;

        }
        else if (!chance)
        {
            return dmg;
        }

        return dmg;
    }

   
}

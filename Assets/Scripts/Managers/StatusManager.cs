using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager : MonoBehaviour
{

    [Header("Damage")]
    [SerializeField] private int sword_Damage;
    [SerializeField] private int gun_Damage;
    [SerializeField] private int gauntlet_Damage;

    [Header("Attack Speed")]
    [SerializeField] private float sword_AtkSpeed;
    [SerializeField] private float gun_AtkSpeed;
    [SerializeField] private float gauntlet_AtkSpeed;

    [Header("Weapon Range")]
    [SerializeField] private float sword_Range;
    [SerializeField] private float gun_Range;
    [SerializeField] private float gauntlet_Range;

    [Header("Critical")]
    [SerializeField] private float critical_Damage;
    [SerializeField] private float critical_Chance;

    [Header("Far Speed")]
    [SerializeField] private float sword_FarSpeed;
    [SerializeField] private float gun_FarSpeed;
    [SerializeField] private float gauntlet_FarSpeed;

    // save data 
    private int[] damage;
     private float[] rate;
     private float[] range;
    private float[] critical;




    private void Start()
    {
        damage = new int[3];
        rate = new float[3];
        range = new float[3];
        critical = new float[2];

    }
    /////////////////////////////////////////////////

    //Set Damage Value
    public void AddSword_Damage(int num)
    {
        sword_Damage += num;
    }

    public void AddGun_Damage(int num)
    {
        gun_Damage += num;
    }

    public void AddGauntlet_Damage(int num)
    {
        gauntlet_Damage += num;
    }

    //Set Rate Value
    public void AddSword_ATKSpeed(float num)
    {
        sword_AtkSpeed -= num;
    }
    public void AddGun_ATKSpeed(float num)
    {
        gun_AtkSpeed -= num;
    }
    public void AddGauntlet_ATKSpeed(float num)
    {
        gauntlet_AtkSpeed -= num;
    }

    //Set Range Value
    public void AddSword_Range(int num)
    {
        sword_Range += num;
    }
    public void AddGun_Range(int num)
    {
        gun_Range += num;
    }
    public void AddGauntlet_Range(int num)
    {
        gauntlet_Range += num;
    }

    public void AddCritical_Damage(float num)
    {
        critical_Damage += num;
    }
    public void AddCritical_Chance(float num)
    {
        critical_Chance += num;
    }

    ////////////////////////////////////////////

    //Get Damage Value
    public int GetSword_Damage()
    {
        return sword_Damage;
    }
    public int GetGun_Damage()
    {
        return gun_Damage;
    }
    public int GetGauntlet_Damage()
    {
        return gauntlet_Damage;
    }

    //Get Rate Value
    public float GetSword_Rate()
    {
        return sword_AtkSpeed;
    }
    public float GetGun_Rate()
    {
        return gun_AtkSpeed;
    }
    public float GetGauntlet_Rate()
    {
        return gauntlet_AtkSpeed;
    }

    //Get Range Value
    public float GetSword_Range()
    {
        return sword_Range;
    }
    public float GetGun_Range()
    {
        return gun_Range;
    }
    public float GetGauntlet_Range()
    {
        return gauntlet_Range;
    }

    //Get Critical Value
    public float GetCritical_Damage()
    {
        return critical_Damage;
    }
    public float GetCritical_Chance()
    {
        return critical_Chance;
    }

    //Get Far Speed Value
    public float GetSword_FarSpeed()
    {
        return sword_FarSpeed;
    }
    public float GetGun_FarSpeed()
    {
        return gun_FarSpeed;
    }
    public float GetGauntlet_FarSpeed()
    {
        return gauntlet_FarSpeed;
    }
    ////////////////////////////////////////////////////////

    //Set Damage Value
    public void SetSword_Damage(int num)
    {
        sword_Damage = num;
    }
    public void SetGun_Damage(int num)
    {
        gun_Damage = num;
    }
    public void SetGauntlet_Damage(int num)
    {
        gauntlet_Damage = num;
    }

    //Set Rate Value
    public void SetSword_Rate(float num)
    {
        sword_AtkSpeed = num;
    }
    public void SetGun_Rate(float num)
    {
        gun_AtkSpeed = num;
    }
    public void SetGauntlet_Rate(float num)
    {
        gauntlet_AtkSpeed = num;
    }

    //Set Range Value
    public void SetSword_Range(float num)
    {
        sword_Range = num;
    }
    public void SetGun_Range(float num)
    {
        gun_Range = num;
    }
    public void SetGauntlet_Range(float num)
    {
        gauntlet_Range = num;
    }

    //Set Critical Value
    public void SetCritical_Damage(float num)
    {
        critical_Damage = num;
    }
    public void SetCritical_Chance(float num)
    {
        critical_Chance = num;
    }

    ///////////////////////////
    public void SaveValue()
    {
        damage[0] = sword_Damage;
        damage[1] = gun_Damage;
        damage[2] = gauntlet_Damage;

        range[0] = sword_Range;
        range[1] = gun_Range;
        range[2] = gauntlet_Range;

        rate[0] = sword_AtkSpeed;
        rate[1] = gun_AtkSpeed;
        rate[2] = gauntlet_AtkSpeed;

        critical[0] = critical_Damage;
        critical[1] = critical_Chance;
    }

    public int[] GetDamage()
    {
        return damage;
    }

    public float[] GetRange()
    {
        return range;
    }

    public float[] GetRate()
    {
        return rate;
    }

    public float[] GetCritical()
    {
        return critical;
    }

    public void LoadDamageArr(int[] arr)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            damage[i] = arr[i];
        }
    }

    public void LoadRangeArr(float[] arr)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            range[i] = arr[i];
        }
    }

    public void LoadRateArr(float[] arr)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            rate[i] = arr[i];
        }
    }

    public void LoadCriticalArr(float[] arr)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            critical[i] = arr[i];
        }
    }

    public void InsertValue()
    {
        sword_Damage = damage[0];
        gun_Damage = damage[1];
        gauntlet_Damage = damage[2];

        sword_Range = range[0];
        gun_Range = range[1];
        gauntlet_Range = range[2];

        sword_AtkSpeed = rate[0];
        gun_AtkSpeed = rate[1];
        gauntlet_AtkSpeed = rate[2];

        critical_Damage = critical[0];
        critical_Chance = critical[1];
    }


}

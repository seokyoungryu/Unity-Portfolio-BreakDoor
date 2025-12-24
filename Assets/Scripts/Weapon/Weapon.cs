using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string name;
    public float range;
    public DoorType weaponType;
    public float typeATK;
    public float elseTypeATK;
    public float atkDelay;
    public float farSpeed;

    public int weaponChangeAnimNum;
    public int comboLimit;

    public string attack_Paticle_Name;
    public string attack_Sound_Name;
}

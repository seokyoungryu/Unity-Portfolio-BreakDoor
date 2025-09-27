using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboAttack : MonoBehaviour
{

    [SerializeField] private Animator playerAnim;

    private bool comboPossible;
    public int comboStep;



    public void Attack(Weapon weapon)
    {
        if (comboStep == 0)
        {
            playerAnim.SetTrigger("IsAttack");
            if (weapon.weaponType == DoorType.SWORD_DOOR || weapon.weaponType == DoorType.GAUNTLET_DOOR)
            {
                comboStep = 1;
                playerAnim.SetInteger("ComboCount", comboStep);
                comboPossible = true;
                return;
            }
            else
                ComboReset();

        }
        if(comboPossible && comboStep == 1)
        {
            playerAnim.SetTrigger("IsAttack");
            if (weapon.weaponType == DoorType.SWORD_DOOR)
            {
                comboStep = 2;
                playerAnim.SetInteger("ComboCount", comboStep);
                return;
            }
            else
                ComboReset();

        }
        if(comboPossible && comboStep == 2)
        {
            playerAnim.SetTrigger("IsAttack");
            ComboReset();
            return;
        }

    }

    public void ComboReset()
    {
        comboStep = 0;
        playerAnim.SetInteger("ComboCount", comboStep);
        comboPossible = false;
    }

}

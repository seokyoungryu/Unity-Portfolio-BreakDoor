using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static Weapon currentWeapon;
    public static bool isWeaponChanging = false;
    public readonly string SWORD = "Sword", GUN = "Gun", GAUNTLET = "Gauntlet";

    private Dictionary<string, Weapon> weaponDIction = new Dictionary<string, Weapon>();
    public float weaponChangeDelay;

    [Header("Component")]
    [SerializeField] private Weapon[] weapons;
    [SerializeField] private WeaponController weaponContreller;
    [SerializeField] private Animator playerAnim;
    [SerializeField] private ComboAttack combo;

    private void Start()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weaponDIction.Add(weapons[i].name, weapons[i]);
        }
    }
   
    private void WeaponChangeProcess(Weapon _weapon)
    {
        if (currentWeapon != null)
            currentWeapon.gameObject.SetActive(false);

        weaponContreller.currentControllerWeapon = _weapon;
        currentWeapon = _weapon;
   
        currentWeapon.gameObject.SetActive(true);
    }

    private IEnumerator WeaponChangeCoroutine(string weaponName)
    {
        isWeaponChanging = true;
        playerAnim.SetTrigger("IsChange");
        playerAnim.SetInteger("IsWeaponOut", weaponDIction[weaponName].weaponChangeAnimNum);
        combo.ComboReset();
        WeaponChangeProcess(weaponDIction[weaponName]);
        yield return new WaitForSeconds(weaponChangeDelay);
        
        isWeaponChanging = false;

    }

    public void WeaponChange(string weaponName)
    {
        if (weaponDIction.ContainsKey(weaponName))
        {
            if (!isWeaponChanging && currentWeapon.name != weaponName)
                StartCoroutine(WeaponChangeCoroutine(weaponName));
        }
        else
        {
            Debug.Log("weapon Dictionary에 무기가 존재하지 않습니다.");

        }
    }

  
    /////////////////////////
    ///

    public void MobileSwordChange()
    {
        if(GameManager.Instance.isGameStart)
        {
            WeaponChange(SWORD);
        }
       
    }
    public void MobileGunChange()
    {
        if (GameManager.Instance.isGameStart)
        {
            WeaponChange(GUN);
        }
       
    }
    public void MobileGauntletChange()
    {
        if (GameManager.Instance.isGameStart)
        {
            WeaponChange(GAUNTLET);
        }
       
    }
}   

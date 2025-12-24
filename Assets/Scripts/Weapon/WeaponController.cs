using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Weapon currentControllerWeapon;
    [SerializeField] private Transform rayPos;
    [SerializeField] private LayerMask doorLayer;

    [Header("Component")]
    [SerializeField] private Animator anim;
    [SerializeField] private Status status;
    [SerializeField] private ComboAttack combo;
    [SerializeField] private HitFeel hitFeel;
    [SerializeField] private ImgDoorHP doorHp_Img;
    [SerializeField] private DoorTimeLimit timer;

    private RaycastHit hitInfoAttack;
    private RaycastHit hitInfoNearDoor;

    private float atk;
    public bool isAttack = false;
    
    private float atkParticleRotation;
    private Vector3 particle_rotat;
    private Vector3 particle_pos;
    private void Awake()
    {
        WeaponManager.currentWeapon = currentControllerWeapon;
    
       // Debug.Log(ObjectPooling.Instance.UseOBP("Ground").name);
       
    }

    private void Update()
    {
        RayCastDraw();
        CheckNearLine();
        CheckBossDoor();
        if (GameManager.Instance.isGameStart)
        {
            if (!isAttack && !WeaponManager.isWeaponChanging && !GameManager.Instance.isBackstep && !GameManager.Instance.isGameOver)
            {
                Attack();
            }
        }


    }

    private IEnumerator AttackCoroutine(Weapon weapon)
    {
        isAttack = true;
        SoundManager.Instance.PlayEffectSound(weapon.attack_Sound_Name, 1f);
        combo.Attack(weapon);
        CheckDoorType();
        yield return new WaitForSeconds(weapon.atkDelay);

        isAttack = false;
    }

    private void CheckDoorType()
    { 

        if(hitInfoAttack.transform.CompareTag("Door"))
        {
            Door door = hitInfoAttack.transform.GetComponent<Door>();

            timer.CheckTimeType(door);
            SoundManager.Instance.CheckBossSound();
            GameManager.Instance.isTimeActive = true;
            CheckATKType(door, WeaponManager.currentWeapon, door.GetDoorType());
            bool isCri = status.IsCritical();
            door.Damage(status.CriticalDmg(atk,isCri), isCri);
            doorHp_Img.SetDoorHP_Img(door.GetMaxHP(), door.GetCurrentHP());
            doorHp_Img.DoorType_Name_Text(door.GetTextDoorName());
           

        }
    }

   
    public void CheckATKType(Door door, Weapon currentWeapon, DoorType DOORTYPE)
    {
        if (door.GetDoorType() == DoorType.BOSS_DOOR)
        {
            atk = currentWeapon.typeATK;
        }
        else if (door.GetDoorType() == DOORTYPE && currentWeapon.weaponType == DOORTYPE)
        {
            atk = currentWeapon.typeATK;
        }
        else
        {
            atk = currentWeapon.elseTypeATK;
        }
    }

    private void Attack()
    {
        if (IsRayAttackTrue())
        {
            GameManager.Instance.isNearDoor = true;
            StartCoroutine(AttackCoroutine(currentControllerWeapon));
            StartCoroutine(Attack_Particle());
            doorHp_Img.DoorHP_SetActive(GameManager.Instance.isNearDoor);
            doorHp_Img.DoorHPText_Hit_Anim();
        }
        else
        {
            GameManager.Instance.isTimeActive = false;
            GameManager.Instance.isNearDoor = false;
            doorHp_Img.DoorHP_SetActive(GameManager.Instance.isNearDoor);
        }
    }

    IEnumerator Attack_Particle()
    {
        string particleName = currentControllerWeapon.attack_Paticle_Name;
        GameObject atk_Particle = ObjectPooling.Instance.UseOBP(currentControllerWeapon.attack_Paticle_Name);
        CalculateParticleRotation(currentControllerWeapon.weaponType,atk_Particle);
        hitFeel.TimeStop();     
        yield return new WaitForSeconds(1f);

        Particle_False(particleName, atk_Particle);
    }

    private void Particle_False(string name, GameObject go)
    {
        ObjectPooling.Instance.RemoveOBP(name, go);
    }

    private void CalculateParticleRotation(DoorType weaponType,GameObject particle)
    {
        if(weaponType == DoorType.SWORD_DOOR)
        {
            if (combo.comboStep == 0)
                atkParticleRotation = Random.Range(-22f, -45f);
            else if (combo.comboStep == 1)
                atkParticleRotation = Random.Range(27f, 35f);
            else if (combo.comboStep == 2)
                atkParticleRotation = 0f;


            particle_rotat = new Vector3(0, 0, atkParticleRotation);
            particle_pos = hitInfoAttack.point + Vector3.up * Random.Range(9f,16f);
        }
        else if(weaponType == DoorType.GUN_DOOR)
        {
            if(combo.comboStep == 0)
            {
                atkParticleRotation = -3f;
                particle_rotat = new Vector3(atkParticleRotation, 0, 0);
                particle_pos = hitInfoAttack.point + (Vector3.up * 5);
            }
        }
        else if(weaponType == DoorType.GAUNTLET_DOOR)
        {
            if(combo.comboStep == 0)
            {
                atkParticleRotation = 0f;
                particle_rotat = new Vector3(atkParticleRotation, -4, 0);
                particle_pos = hitInfoAttack.point + (Vector3.up * 3) + (Vector3.right * 2f);
            }
            else if(combo.comboStep == 1)
            {
                atkParticleRotation = 0f;
                particle_rotat = new Vector3(atkParticleRotation, 10, 0);
                particle_pos = hitInfoAttack.point + (Vector3.up * 3) + (Vector3.right * 0f);
            }
        }

        particle.transform.position = particle_pos;
        particle.transform.rotation = Quaternion.Euler(particle_rotat);
    }

    private void CheckNearLine()
    {
        IsRayNearDoor();
        if(IsRayAttackTrue())
        {
            GameManager.Instance.isNearLine = true;
            CheckNearLine(hitInfoNearDoor.transform.gameObject);
        
        }
        else
        {
            GameManager.Instance.isNearLine = false;
            CheckNearLine(hitInfoNearDoor.transform.gameObject);
        }
       
    }

    private void CheckBossDoor()
    {
        if(hitInfoNearDoor.transform != null)
        {
            Door bossDoor = hitInfoNearDoor.transform.GetComponent<Door>();
            if(bossDoor.GetDoorType() == DoorType.BOSS_DOOR)
            {
                GameManager.Instance.isBossDoor = true;
                doorHp_Img.RoundTextBossAnim(GameManager.Instance.isBossDoor);
                Debug.Log("보스 온 ");

            }
            else
            {
                GameManager.Instance.isBossDoor = false;
                doorHp_Img.RoundTextBossAnim(GameManager.Instance.isBossDoor);
                //Debug.Log("보스 오프 ");

            }
        }
    }

    private void CheckNearLine(GameObject hitIn)
    {
        Door door = hitIn.GetComponent<Door>();
        door.CheckLine(GameManager.Instance.isNearLine);
    }
   
    private void Attack_ForMobile()
    {
        if (IsRayAttackTrue())
        {
            StartCoroutine(AttackCoroutine(currentControllerWeapon));

        }
    }
    public void Btn_Attack()
    {
        if (GameManager.Instance.isGameStart)
        {
            if (!isAttack && !WeaponManager.isWeaponChanging)
                Attack_ForMobile();

        }
    }


    private bool IsRayAttackTrue()
    {
        if(Physics.Raycast(rayPos.position, rayPos.forward, out hitInfoAttack, currentControllerWeapon.range, doorLayer))
        {
            return true;
        }
        return false;
    }

    private bool IsRayNearDoor()
    {
        if (Physics.Raycast(rayPos.position,rayPos.forward, out hitInfoNearDoor, 1000,doorLayer))
        {
            return true;
        }
        return false;
    }


    private void RayCastDraw()
    {
        Debug.DrawRay(currentControllerWeapon.gameObject.transform.position, Vector3.forward * currentControllerWeapon.range, Color.red);
        
    }

   
}

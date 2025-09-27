using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour
{
    [Header("Ground")]
    [SerializeField] private Transform startGroundPos;
    [SerializeField] private string[] groundsName;
    [SerializeField] private int createSize;
    [SerializeField] private Vector3 lastGroundPos;

    [Header("Door")]
    [SerializeField] private int createCount;
    [SerializeField] private Transform startDoorPos;
    [SerializeField] private float doorDistanceMin;
    [SerializeField] private float doorDistanceMax;
    [SerializeField] private string[] doorType;  // 마지막 배열 보스
    [SerializeField] private int[] bossRoom;

    private Vector3 lastDoorPos;
    private float distanceSpace;
    private float doorSizeZ;
    [SerializeField] private int bossAddNum;
    private float groundSizeZ;

    [Header("Count")]
    [SerializeField] private int doorCount = 0;
    [SerializeField] private int bossCount = 0;


    private void Start()
    {
        PosInit();
        InitBoosRoom(10);

        if (GameManager.Instance.isTitle)
        {
            for (int i = 0; i < 8; i++)
            {
                CreateNextGround();
            }
        }
        else if(!GameManager.Instance.isTitle)
        {
            for (int i = 0; i < 8; i++)
            {
                CreateNextGround();
            }
            for (int i = 0; i < createCount; i++)
            {
                CreateDoor(DoorTypeCount());
            }
        }
        
    }


    private void InitBoosRoom( int count, int size = 50)
    {
        bossRoom = new int[size];
       
        for (int i = 0; i < size; i++)
        {
            bossRoom[i] = count + bossAddNum;
            bossAddNum = bossRoom[i];
        }
    }

    public int DoorTypeCount() 
    {      
        if (doorCount == (bossRoom[bossCount])) 
        {
            int bossNum = doorType.Length - 1;
            return bossNum;

        }
        else
        {
            int num = Random.Range(0, doorType.Length - 1);
            return num;
        }
    }

    public void PosInit()
    {
        if(GameManager.Instance.isTitle)
        {
            groundSizeZ = startGroundPos.localScale.z;
            lastGroundPos = startGroundPos.position;

        }
        else if(!GameManager.Instance.isTitle)
        {
            groundSizeZ = startGroundPos.localScale.z;
            lastGroundPos = startGroundPos.position;
            doorSizeZ = startDoorPos.localScale.z;
            lastDoorPos = startDoorPos.position;
        }
    }
 
    public void CreateNextGround() //땅 
    {
        int random = Random.Range(0, groundsName.Length);
        GameObject ground = ObjectPooling.Instance.UseOBP(groundsName[random]);
        ground.transform.position = lastGroundPos + new Vector3(0, 0, groundSizeZ);
        lastGroundPos = ground.transform.position;  

    }

    public void CreateDoor(int num) // 문
    {
      
        GameObject door = ObjectPooling.Instance.UseOBP(doorType[num]);
        distanceSpace = Random.Range(doorDistanceMin, doorDistanceMax);
        SetDoorHP(door.GetComponent<Door>());
        door.transform.position = lastDoorPos + new Vector3(0, 0, doorSizeZ + distanceSpace);
        lastDoorPos = door.transform.position;

        if (door.GetComponent<Door>().GetDoorType() == DoorType.BOSS_DOOR)
        {
            bossCount++;
            doorCount++;
        }
        else
            doorCount++;


    }


    private void SetDoorHP(Door door)
    {
        for (int i = 0; i < bossRoom.Length; i++)
        {       
            if (doorCount < bossRoom[i])
            {
                if (!(door.GetDoorType() == DoorType.BOSS_DOOR))
                {
                    door.SetMaxHp((int)(door.GetMaxHP() * (1 + (bossRoom[i] * 0.11))));
                    door.SetCurrentHP(door.GetMaxHP());
                    return;
                }
                else if(door.GetDoorType() == DoorType.BOSS_DOOR)
                {
                    door.SetMaxHp((int)(door.GetMaxHP() * (1 + (bossRoom[i] * 0.12f))));
                    door.SetCurrentHP(door.GetMaxHP());
                    return;
                }
            }
        }
    }


    public int GetBossCount()
    {
        return bossCount;
    }

}

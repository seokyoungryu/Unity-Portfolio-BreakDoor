using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    [SerializeField] private MapGeneration mapGeneration;

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Ground")
        {
            Ground ground = other.GetComponent<Ground>();
            ObjectPooling.Instance.RemoveOBP(ground.name, other.gameObject);
            mapGeneration.CreateNextGround();
        }

        if(other.gameObject.layer.Equals("Door"))
        {
            Door door = other.GetComponent<Door>();
            ObjectPooling.Instance.RemoveOBP(door.GetDoorName(), other.gameObject);
            int random = Random.Range(0, 3);
            mapGeneration.CreateDoor(random);
        }



        if (other.gameObject.tag == "OpenDoor")
            Destroy(other.gameObject);


    }
}

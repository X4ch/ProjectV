using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Teleporter : MonoBehaviour
{

    [SerializeField] GameObject exitPortal;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CarController car = collision.GetComponent<CarController>();
        if (car != null && car.canTeleport)
        {
            collision.transform.position = exitPortal.transform.position;
            car.setTeleportationTime(0);
            car.canTeleport = false;
        }
    }
}

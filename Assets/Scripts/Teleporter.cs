using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] GameObject carPrefab;
    [SerializeField] GameObject exitPortal;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject carObject = collision.gameObject;
        CarController car = collision.GetComponent<CarController>();
        if (car != null && car.canTeleport)
        {
            Destroy(carObject);
            GameObject carInstance = Instantiate(carPrefab, exitPortal.transform.position, exitPortal.transform.rotation);
            carInstance.GetComponent<CarController>().setTeleportationTime(0);
            carInstance.GetComponent<CarController>().canTeleport = false;
        }
    }
}

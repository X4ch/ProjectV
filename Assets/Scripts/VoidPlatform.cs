using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidPlatform : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        CarController car = collision.gameObject.GetComponent<CarController>();
        if (car != null)
        {
            car.IsOnPlatform = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        CarController car = collision.gameObject.GetComponent<CarController>();
        if (car != null)
        {
            car.IsOnPlatform = false;
        }
    }
}

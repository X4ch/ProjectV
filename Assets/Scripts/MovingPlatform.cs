using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private CarController car;
    private void OnTriggerStay2D(Collider2D collision)
    {
        car = collision.gameObject.GetComponent<CarController>();
        if (car != null)
        {
            car.IsOnMovingPlatform = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        car = collision.gameObject.GetComponent<CarController>();
        if (car != null)
        {
            car.IsOnMovingPlatform = false;
        }
    }

    public void Moving(float x, float y)
    {
        transform.position += new Vector3(x, y, 0);
        if (car == null) return;
        if (car.IsOnMovingPlatform)
        {
            car.transform.position += new Vector3(x, y, 0);

        }
    }
}

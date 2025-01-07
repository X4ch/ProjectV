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
        Vector3 movement = new Vector3(x, y, 0);

        // Check for collisions with VoidPlatform in the direction of movement
        RaycastHit2D hit = Physics2D.BoxCast(
            transform.position,
            GetComponent<Collider2D>().bounds.size,
            0f,
            movement.normalized,
            movement.magnitude,
            LayerMask.GetMask("VoidPlatform")
        );

        if (hit.collider != null)
        {
            return;
        }

        // Apply movement to the platform
        transform.position += movement;

        // Move the car if it's on the platform
        if (car != null && car.IsOnMovingPlatform)
        {
            car.transform.position += movement;
        }
    }
}

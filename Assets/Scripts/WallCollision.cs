using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollision : MonoBehaviour
{
    [SerializeField] ScreenShake screenShake;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision with wall detected");
        if (collision.gameObject.GetComponent<CarController>())
        {            
            float speed = collision.gameObject.GetComponent<CarController>().getVelocity();
            screenShake.TriggerShake(speed);
        }
    }
}
    
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WallCollision : MonoBehaviour
{
    [SerializeField] ScreenShake screenShake;
    [SerializeField] EdgeCollider2D edgeCollider;
    [SerializeField] GameObject car;
    [SerializeField] GameObject audioManager;

    private void Update()
    {
        if (car == null)
        {
            car = GameObject.FindGameObjectWithTag("Player");
        }

        if (edgeCollider.IsTouching(car.GetComponent<BoxCollider2D>()))
        {
            float speed = car.GetComponent<CarController>().getVelocity();
            if (Mathf.Abs(speed) > 10) 
            { 
                audioManager.GetComponent<AudioManager>().PlayWallHit();
            }
            screenShake.TriggerShake(speed);
            Rumble.Instance.RumblePulse(0.1f, 0.4f, 0.5f);
        }
    }
}
    
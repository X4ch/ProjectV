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

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager");
    }

    private void Update()
    {
        if (car == null)
        {
            car = GameObject.FindGameObjectWithTag("Player");
        }

        if (edgeCollider.IsTouching(car.GetComponent<BoxCollider2D>()))
        {
            float speed = car.GetComponent<CarController>().getVelocity();
            if (speed > 10) 
            { 
                audioManager.GetComponent<AudioManager>().PlayWallHit();
            }
            screenShake.TriggerShake(speed);
            Rumble.Instance.RumblePulse(10, 40, 10);
        }
    }
}
    
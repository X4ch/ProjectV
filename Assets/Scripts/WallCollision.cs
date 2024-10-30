using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WallCollision : MonoBehaviour
{
    [SerializeField] ScreenShake screenShake;
    [SerializeField] EdgeCollider2D edgeCollider;
    [SerializeField] GameObject car;


    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("Collision with wall detected");
    //    if (collision.gameObject.GetComponent<CarController>())
    //    {            
    //        float speed = collision.gameObject.GetComponent<CarController>().getVelocity();
    //        screenShake.TriggerShake(speed);
    //    }
    //}
    private void Update()
    {

        if (car == null)
        {
            car = GameObject.FindGameObjectWithTag("Player");
        }

        if (edgeCollider.IsTouching(car.GetComponent<BoxCollider2D>()))
        {
            float speed = car.GetComponent<CarController>().getVelocity();
            screenShake.TriggerShake(speed);
        }
    }
}
    
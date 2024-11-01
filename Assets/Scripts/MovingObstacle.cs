using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [SerializeField] GameObject obstacle;
    [SerializeField] GameObject target;
    [SerializeField] float movingSpeed;
    [SerializeField] BoxCollider2D boxCollider;

    [SerializeField] ScreenShake screenShake;
 
    private bool isReturning = false;

    private GameObject car;
    private GameObject audioManager;

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

        if (obstacle.transform.position == target.transform.position)
        {
            isReturning = true;
        }
        else if (obstacle.transform.position == this.transform.position)
        {
            isReturning = false;
        }

        if (!isReturning)
        {
            Vector3 newPosition = Vector3.MoveTowards(obstacle.transform.position, target.transform.position, movingSpeed*Time.deltaTime);
            obstacle.transform.position = newPosition;
        }
        else
        {
            Vector3 newPosition = Vector3.MoveTowards(obstacle.transform.position, this.transform.position, movingSpeed*Time.deltaTime);
            obstacle.transform.position = newPosition;
        }

        

        if (boxCollider.IsTouching(car.GetComponent<BoxCollider2D>()))
        {
            float speed = car.GetComponent<CarController>().getVelocity();
            if (speed > 10) { audioManager.GetComponent<AudioManager>().PlayWallHit(); }
            screenShake.TriggerShake(speed);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [SerializeField] GameObject obstacle;
    [SerializeField] GameObject target;
    [SerializeField] float movingSpeed;

    private bool isReturning = false;
    private float speed;

    private void Update()
    {
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
            Vector3 newPosition = Vector3.MoveTowards(obstacle.transform.position, target.transform.position, movingSpeed);
            obstacle.transform.position = newPosition;
        }
        else
        {
            Vector3 newPosition = Vector3.MoveTowards(obstacle.transform.position, this.transform.position, movingSpeed);
            obstacle.transform.position = newPosition;
        }
        
    }
}

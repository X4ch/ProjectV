using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class BreakableBox : MonoBehaviour
{

    [SerializeField] float breakSpeed = 10f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CarController car = collision.GetComponent<CarController>();

        if (car != null && math.abs(car.getVelocity()) >= breakSpeed)
        {
            Debug.Log("Box breaked");
            Destroy(this.GameObject());
        }
    }
}

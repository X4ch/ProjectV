using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidOut : MonoBehaviour
{
    [SerializeField] private EdgeCollider2D edgeCollider;
    [SerializeField] private GameObject car;

    private void Update()
    {

        if (car == null)
        {
            car = GameObject.FindGameObjectWithTag("Player");
        }

        if (edgeCollider.IsTouching(car.GetComponent<BoxCollider2D>()))
        {
            Debug.Log("Fall in void");
        }
    }
}

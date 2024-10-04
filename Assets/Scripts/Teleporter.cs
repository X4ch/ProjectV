using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Teleporter : MonoBehaviour
{

    [SerializeField] GameObject exitPortal; 

    void Start()
    {
        Color parentColour = GetComponentsInParent<Renderer>()[1].material.color;
        GetComponent<Renderer>().material.color = parentColour;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Tp collided");

        if (collision.GetComponent<CarController>() != null)
        {
            collision.transform.position = exitPortal.transform.position;
        }
    }

}

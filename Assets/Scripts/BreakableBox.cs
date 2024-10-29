using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class BreakableBox : MonoBehaviour
{

    [SerializeField] float breakSpeed = 10f;
    [SerializeField] AudioSource audioPlayer;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CarController car = collision.GetComponent<CarController>();

        if (car != null && math.abs(car.getVelocity()) >= breakSpeed)
        {
            audioPlayer.Play();
            Debug.Log("Box breaked");
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            BoxCollider2D[] colliderList = gameObject.GetComponents<BoxCollider2D>();
            foreach (var collider in colliderList)
            {
                collider.enabled = false;
            }
        }
    }
}

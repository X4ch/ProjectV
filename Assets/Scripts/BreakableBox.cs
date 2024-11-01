using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class BreakableBox : MonoBehaviour
{

    [SerializeField] float breakSpeed = 10f;
    private GameObject audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CarController car = collision.GetComponent<CarController>();

        if (car != null && math.abs(car.getVelocity()) >= breakSpeed)
        {
            audioManager.GetComponent<AudioManager>().PlayBrokenCrate();
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            Renderer[] childRenderers = gameObject.GetComponentsInChildren<Renderer>();
            foreach (var renderer in childRenderers)
            {
                renderer.enabled = false;
            }
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            BoxCollider2D[] colliderList = gameObject.GetComponents<BoxCollider2D>();
            foreach (var collider in colliderList)
            {
                collider.enabled = false;
            }
        }
    }
}

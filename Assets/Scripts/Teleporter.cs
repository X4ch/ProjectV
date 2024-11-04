using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] GameObject carPrefab;
    [SerializeField] GameObject exitPortal;

    [SerializeField] GameObject audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject carObject = collision.gameObject;
        CarController car = collision.GetComponent<CarController>();

        if (car != null)
        {

            audioManager.GetComponent<AudioManager>().PlayTeleport();

            carObject.transform.position = exitPortal.transform.position;

            Rigidbody2D carRigidbody2D = carObject.GetComponent<Rigidbody2D>();
            Vector2 currentVelocity = carRigidbody2D.velocity;

            Vector2 portalForwardDirection = exitPortal.transform.up;

            float currentSpeed = currentVelocity.magnitude;
            Vector2 newVelocity = portalForwardDirection * currentSpeed;

            carRigidbody2D.velocity = newVelocity;

            car.setRotation(exitPortal.transform.rotation.eulerAngles.z);
        }
    }

}

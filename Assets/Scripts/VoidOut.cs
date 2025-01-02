using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidOut : MonoBehaviour
{
    [SerializeField] private GameObject trackManager;
    private GameObject audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        CarController car = collision.gameObject.GetComponent<CarController>();
        if (car != null)
        {
            if (car.IsOnPlatform || car.IsOnMovingPlatform)
            {
                return;
            }

            Debug.Log("Fall in void");
            audioManager.GetComponent<AudioManager>().PlayVoidOut();
            trackManager.GetComponent<TrackManager>().OnRespawn();
        }
    }
}

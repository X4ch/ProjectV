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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<CarController>() != null)
        {
            Debug.Log("Fall in void");
            audioManager.GetComponent<AudioManager>().PlayVoidOut();
            trackManager.GetComponent<TrackManager>().OnRespawn();
        }
    }
}

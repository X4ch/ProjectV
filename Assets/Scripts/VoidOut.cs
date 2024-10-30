using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidOut : MonoBehaviour
{
    [SerializeField] private GameObject trackManager;
    [SerializeField] private AudioSource voidOutSound;   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Fall in void");
        voidOutSound.Play();
        trackManager.GetComponent<TrackManager>().OnRespawn();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidOut : MonoBehaviour
{
    [SerializeField] private GameObject trackManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Fall in void");
        trackManager.GetComponent<TrackManager>().OnRespawn();
    }
}

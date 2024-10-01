using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField]
    private GameObject trackManager;

    private void OnTriggerEnter2D()
    {
        trackManager.GetComponent<TrackManager>().crossCheckpoint(this);
    }
}

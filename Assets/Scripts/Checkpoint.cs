using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField]
    private GameObject trackManager;
    private BoxCollider2D checkpointCollider;

    private void Awake()
    {
        checkpointCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D()
    {
        trackManager.GetComponent<TrackManager>().crossCheckpoint(checkpointCollider);
    }
}

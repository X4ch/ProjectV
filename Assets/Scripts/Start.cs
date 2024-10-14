using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start : MonoBehaviour
{
    [SerializeField]
    private GameObject trackManager;

    private void OnTriggerEnter2D()
    {
        trackManager.GetComponent<TrackManager>().CrossEndline();
    }
}

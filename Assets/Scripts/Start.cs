using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start : MonoBehaviour
{
    [SerializeField]
    private GameObject trackManager;
    private BoxCollider2D startCollider;

    private void Awake()
    {
        startCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D()
    {
        trackManager.GetComponent<TrackManager>().crossStartLine();
    }
}

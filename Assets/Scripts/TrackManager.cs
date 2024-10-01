using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackManager : MonoBehaviour
{

    [SerializeField] bool isMultiLap;
    [SerializeField] int numberOfLap;
    private int lapCounter;

    [SerializeField] GameObject startLine;
    private BoxCollider2D startLineCollider;

    [SerializeField] GameObject carPrefab;
    private GameObject car;

    private bool isTrackFinished;
    private float trackTimer;

    [SerializeField] private int numberOfCheckpointCrossed;
    private int numberOfCheckpoints;
    [SerializeField] List<GameObject> checkpoints;

    public void crossCheckpoint(BoxCollider2D collider)
    {
        Debug.Log("Checkpoint crossed");

        numberOfCheckpointCrossed++;
        collider.enabled = false;
    }

    public void crossStartLine()
    {
        Debug.Log("Lap finished");

        lapCounter++;
        numberOfCheckpointCrossed = 0;
        startLineCollider.enabled = false;

        if (lapCounter == numberOfLap)
        {
            EndTrack();
        }
        else
        {
            reanableCheckpoints();
        }
    }

    private void reanableCheckpoints()
    {
        foreach (var checkpoint in checkpoints)
        {
            checkpoint.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
    private void EndTrack()
    {
        // TO DO : implement logic when a track is done

        Debug.Log("Track finished");
        car.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        numberOfCheckpoints = checkpoints.Count;
        car = Instantiate(carPrefab, startLine.transform.position, Quaternion.identity);
        startLineCollider = startLine.GetComponent<BoxCollider2D>();
        startLineCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        trackTimer += Time.deltaTime;

        if (numberOfCheckpointCrossed == numberOfCheckpoints) 
        {
            startLineCollider.enabled = true;
        }
    }
}

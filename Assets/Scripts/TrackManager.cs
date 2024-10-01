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

    [SerializeField] GameObject endLine;
    private BoxCollider2D endLineCollider;

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

    public void crossEndLine()
    {
        if (isMultiLap)
        {
            Debug.Log("Lap finished");

            lapCounter++;
            numberOfCheckpointCrossed = 0;
            endLineCollider.enabled = false;

            if (lapCounter == numberOfLap)
            {
                EndTrack();
            }
            else
            {
                reanableCheckpoints();
            }
        }

        else
        {
            Debug.Log("Track ended");

            EndTrack();
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
        Debug.Log("Track finished");
        car.SetActive(false);

        // TO DO : implement logic when a track is done
    }

    // Start is called before the first frame update
    void Start()
    {
        numberOfCheckpoints = checkpoints.Count;
        car = Instantiate(carPrefab, startLine.transform.position, Quaternion.identity);
        startLineCollider = startLine.GetComponent<BoxCollider2D>();
        startLineCollider.enabled = false;

        endLineCollider = endLine.GetComponent<BoxCollider2D>();
        endLineCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        trackTimer += Time.deltaTime;

        if (numberOfCheckpointCrossed == numberOfCheckpoints) 
        {
            endLineCollider.enabled = true;
        }
    }
}

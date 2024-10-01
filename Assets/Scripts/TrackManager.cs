using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackManager : MonoBehaviour
{

    [SerializeField] bool isMultiLap;
    [SerializeField] int numberOfLap;
    private int lapCounter;

    [SerializeField] GameObject startLine;

    [SerializeField] GameObject endLine;
    private BoxCollider2D endLineCollider;

    [SerializeField] GameObject carPrefab;
    private GameObject car;

    private bool isTrackFinished;
    private float trackTimer;

    [SerializeField] private int numberOfCheckpointCrossed;
    private int numberOfCheckpoints;
    [SerializeField] List<GameObject> checkpoints;
    private Vector2 respawnPoint;

    public void crossCheckpoint(Checkpoint checkpoint)
    {
        Debug.Log("Checkpoint crossed");

        numberOfCheckpointCrossed++;
        checkpoint.GetComponent<BoxCollider2D>().enabled = false;

        respawnPoint = checkpoint.transform.position;
    }

    public void crossEndLine()
    {
        if (isMultiLap)
        {
            Debug.Log("Lap finished");

            lapCounter++;
            numberOfCheckpointCrossed = 0;
            endLineCollider.enabled = false;

            respawnPoint = endLine.transform.position;

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

    public void onRespawn()
    {
        Destroy(car);
        car = Instantiate(carPrefab, respawnPoint, Quaternion.identity);
    }

 

    // Start is called before the first frame update
    void Start()
    {
        numberOfCheckpoints = checkpoints.Count;
        car = Instantiate(carPrefab, startLine.transform.position, Quaternion.identity);

        // TO DO : change car rotation

        endLineCollider = endLine.GetComponent<BoxCollider2D>();
        endLineCollider.enabled = false;

        respawnPoint = startLine.transform.position;
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

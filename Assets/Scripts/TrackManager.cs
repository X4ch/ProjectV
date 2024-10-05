using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

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

    private GameObject currentCheckPoint;

    private Vector3 carPositionAtCheckpoint;
    private Quaternion carAngleAtCheckpoint;
    private float carVelocityAtCheckpoint;
    private float carRotationAtCheckpoint;
    private float carAccelerationInput;

    public void crossCheckpoint(Checkpoint checkpoint)
    {
        Debug.Log("Checkpoint crossed");

        numberOfCheckpointCrossed++;
        checkpoint.GetComponent<BoxCollider2D>().enabled = false;
        currentCheckPoint = checkpoints[numberOfCheckpointCrossed - 1];

        setCarStatus();
    }

    private void setCarStatus()
    {
        CarController controller = car.GetComponent<CarController>();

        carPositionAtCheckpoint = car.transform.position;
        carAngleAtCheckpoint = car.transform.rotation;
        carVelocityAtCheckpoint = controller.getVelocity();
        carRotationAtCheckpoint = controller.getRotation();
        carAccelerationInput = controller.getAccelerationInput();
    }

    public void crossEndLine()
    {
        if (isMultiLap)
        {
            Debug.Log("Lap finished");

            lapCounter++;
            numberOfCheckpointCrossed = 0;
            endLineCollider.enabled = false;

            currentCheckPoint = endLine;

            setCarStatus();

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

    public void onLaunchedRespawn()
    {
        Destroy(car);
        car = Instantiate(carPrefab, carPositionAtCheckpoint, carAngleAtCheckpoint);
        CarController controller = car.GetComponent<CarController>();
        controller.setVelocity(carVelocityAtCheckpoint);
        controller.setRotation(carRotationAtCheckpoint);
        controller.setAccelerationInput(carAccelerationInput);
    }

    public void onRespawn()
    {
        Destroy(car);
        car = Instantiate(carPrefab, currentCheckPoint.transform.position, currentCheckPoint.transform.rotation);
        //car.GetComponent<Rigidbody2D>().MoveRotation(currentCheckPoint.transform.rotation);
    }

    // Start is called before the first frame update
    void Start()
    {
        numberOfCheckpoints = checkpoints.Count;
        car = Instantiate(carPrefab, startLine.transform.position, startLine.transform.rotation);
        setCarStatus();

        endLineCollider = endLine.GetComponent<BoxCollider2D>();
        endLineCollider.enabled = false;

        currentCheckPoint = startLine;
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

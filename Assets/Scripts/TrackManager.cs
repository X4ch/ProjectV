using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class TrackManager : MonoBehaviour
{

    [SerializeField] bool isMultilap; // probably useless
    [SerializeField] public int numberOfLapsTotal;
    public int numberOfLapsCrossed;

    [SerializeField] GameObject startline;
    [SerializeField] GameObject endline;
    private BoxCollider2D endlineCollider;

    [SerializeField] GameObject carPrefab;
    private GameObject car;

    public bool isTrackStarted = false; // private and Setters and Getters would be better...
    //private bool isTrackFinished = false;
    private float trackTimerStart;
    public float trackTimer; // private and Setters and Getters would be better...
    private List<float> lapTimers;

    [SerializeField] List<GameObject> checkpoints;
    private int numberOfCheckpointsTotal;
    private int numberOfCheckpointsCrossed;

    private GameObject currentCheckpoint;

    private Vector3 carPositionAtCheckpoint;
    private Quaternion carAngleAtCheckpoint;
    private float carVelocityAtCheckpoint;
    private float carRotationAtCheckpoint;
    private float carAccelerationInput;

    public void CrossCheckpoint(Checkpoint checkpoint)
    {
        Debug.Log("Checkpoint crossed");

        numberOfCheckpointsCrossed++;
        checkpoint.GetComponent<BoxCollider2D>().enabled = false;
        currentCheckpoint = checkpoints[numberOfCheckpointsCrossed - 1];

        SetCarStatus();
    }

    private void SetCarStatus()
    {
        CarController controller = car.GetComponent<CarController>();

        carPositionAtCheckpoint = car.transform.position;
        carAngleAtCheckpoint = car.transform.rotation;
        carVelocityAtCheckpoint = controller.getVelocity();
        carRotationAtCheckpoint = controller.getRotation();
        carAccelerationInput = controller.getAccelerationInput();
    }

    private void AddLapTimer()
    {
        
        lapTimers.Add(trackTimer);

        int minutes = Mathf.FloorToInt(trackTimer / 60F);
        int seconds = Mathf.FloorToInt(trackTimer - minutes * 60);
        int milliseconds = Mathf.FloorToInt((trackTimer - minutes * 60 - seconds) * 1000);
        Debug.Log(string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds));
    }

    public void CrossEndline()
    {
        Debug.Log("Lap finished");

        numberOfLapsCrossed++;
        numberOfCheckpointsCrossed = 0;
        endlineCollider.enabled = false;

        currentCheckpoint = endline;

        SetCarStatus();

        AddLapTimer();

        if (numberOfLapsCrossed == numberOfLapsTotal) EndTrack();
        else RenableCheckpoints();
        
    }

    private void RenableCheckpoints()
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

        // TODO : add the menu with the timers, and the possibility to restart the track or go back to the main menu
    }

    public void OnLaunchedRespawn()
    {
        Destroy(car);
        car = Instantiate(carPrefab, carPositionAtCheckpoint, carAngleAtCheckpoint);
        CarController controller = car.GetComponent<CarController>();
        controller.setVelocity(carVelocityAtCheckpoint);
        controller.setRotation(carRotationAtCheckpoint);
        controller.setAccelerationInput(carAccelerationInput);
    }

    public void OnRespawn()
    {
        Destroy(car);
        car = Instantiate(carPrefab, currentCheckpoint.transform.position, currentCheckpoint.transform.rotation);
        //car.GetComponent<Rigidbody2D>().MoveRotation(currentCheckpoint.transform.rotation);
    }
    public float GetCarVelocity()
    {
        return car.GetComponent<CarController>().getVelocity();
    }


// Start is called before the first frame update
    private void Start()
    {
        numberOfCheckpointsTotal = checkpoints.Count;
        car = Instantiate(carPrefab, startline.transform.position, startline.transform.rotation);
        SetCarStatus();

        endlineCollider = endline.GetComponent<BoxCollider2D>();
        endlineCollider.enabled = false;

        currentCheckpoint = startline;
        StartCoroutine(Countdown(5));
    }

    IEnumerator Countdown(int seconds)
    {

        for (int i = seconds; i > 4; i--)
        {
            Debug.Log(i);
            yield return new WaitForSeconds(1);
        }

        Debug.Log("3");
        UIManager.Instance.DisplayCountdown("3");
        yield return new WaitForSeconds(1);

        Debug.Log("2");
        UIManager.Instance.DisplayCountdown("2");
        yield return new WaitForSeconds(1);

        Debug.Log("1");
        UIManager.Instance.DisplayCountdown("1");
        yield return new WaitForSeconds(1);

        Debug.Log("0");
        UIManager.Instance.DisplayCountdown("Go !");
        StartGame();

        yield return new WaitForSeconds(1);
        UIManager.Instance.HideStartUI();
        UIManager.Instance.ShowRaceUI();
    }

    void StartGame()
    {
        lapTimers = new List<float>();
        trackTimerStart = Time.time;
        isTrackStarted = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isTrackStarted)
        {
            return;
        }
        trackTimer = Time.time - trackTimerStart;

        if (numberOfCheckpointsCrossed == numberOfCheckpointsTotal)
        {
            endlineCollider.enabled = true;
        }
    }

}
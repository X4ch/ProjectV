using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TrackManager : MonoBehaviour
{
    [SerializeField] public int numberOfLapsTotal;
    public int numberOfLapsCrossed;

    [SerializeField] GameObject startline;
    [SerializeField] GameObject endline;
    private BoxCollider2D endlineCollider;

    [SerializeField] GameObject car1Prefab;
    [SerializeField] GameObject car2Prefab;
    [SerializeField] GameObject car3Prefab;
    private GameObject carPrefab;
    private GameObject car;

    public bool isTrackRunning = false; // private and Setters and Getters would be better...
    private float trackTimerStart;
    public float trackTimer; // private and Setters and Getters would be better...
    private List<float> lapTimers;

    [SerializeField] List<GameObject> checkpoints;
    private int numberOfCheckpointsTotal;
    private int numberOfCheckpointsCrossed;

    private GameObject currentCheckpoint;

    [SerializeField] private GameObject audioManager;

    public void CrossCheckpoint(Checkpoint checkpoint)
    {
        numberOfCheckpointsCrossed++;
        checkpoint.GetComponent<BoxCollider2D>().enabled = false;
        currentCheckpoint = checkpoints[numberOfCheckpointsCrossed - 1];
    }

    private void AddLapTimer()
    {
        float lapTime = trackTimer - lapTimers.Sum();
        lapTimers.Add(lapTime);
    }

    public void CrossEndline()
    {
        numberOfLapsCrossed++;
        numberOfCheckpointsCrossed = 0;
        endlineCollider.enabled = false;

        currentCheckpoint = endline;

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
        car.SetActive(false);
        isTrackRunning = false;

        audioManager.GetComponent<AudioManager>().StopDrift();
        audioManager.GetComponent<AudioManager>().StopEngineTest();

        UIManager.Instance.HideRaceUI();
        UIManager.Instance.ShowEndUI(lapTimers);
    }

    public void OnRespawn()
    {
        if (!isTrackRunning) return;
        Destroy(car);
        car = Instantiate(carPrefab, currentCheckpoint.transform.position, currentCheckpoint.transform.rotation);
    }

    public float GetCarVelocity()
    {
        return car.GetComponent<CarController>().getVelocity();
    }

    public void OnHorn()
    {
        if (!isTrackRunning) return;
        audioManager.GetComponent<AudioManager>().PlayHorn();
    }

    private void Start()
    {
        numberOfCheckpointsTotal = checkpoints.Count;
        if (PlayerPrefs.HasKey("Car"))
        {
            switch (PlayerPrefs.GetInt("Car"))
            {
                case 1:
                    carPrefab = car1Prefab;
                    break;
                case 2:
                    carPrefab = car2Prefab;
                    break;
                case 3:
                    carPrefab = car3Prefab;
                    break;
                default:
                    carPrefab = car1Prefab;
                    break;
            }
        }
        else
        {
            carPrefab = car1Prefab;
        }
        car = Instantiate(carPrefab, startline.transform.position, startline.transform.rotation);

        endlineCollider = endline.GetComponent<BoxCollider2D>();
        endlineCollider.enabled = false;

        currentCheckpoint = startline;
        StartCoroutine(Countdown(5));
    }

    IEnumerator Countdown(int seconds)
    {
        for (int i = seconds; i > 4; i--)
        {
            yield return new WaitForSeconds(1);
        }

        UIManager.Instance.DisplayCountdown("3");
        UIManager.Instance.Light3();
        yield return new WaitForSeconds(1);

        UIManager.Instance.DisplayCountdown("2");
        UIManager.Instance.Light2();
        yield return new WaitForSeconds(1);

        UIManager.Instance.DisplayCountdown("1");
        UIManager.Instance.Light1();
        yield return new WaitForSeconds(1);

        UIManager.Instance.DisplayCountdown("Go !");
        UIManager.Instance.LightAll();
        StartGame();

        yield return new WaitForSeconds(1);
        UIManager.Instance.HideStartUI();
        UIManager.Instance.ShowRaceUI();
    }

    void StartGame()
    {
        lapTimers = new List<float>();
        trackTimerStart = Time.time;
        isTrackRunning = true;
    }

    void Update()
    {
        if (!isTrackRunning) return;

        trackTimer = Time.time - trackTimerStart;

        if (numberOfCheckpointsCrossed == numberOfCheckpointsTotal)
        {
            endlineCollider.enabled = true;
        }
    }

}
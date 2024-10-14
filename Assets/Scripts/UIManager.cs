using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    //[SerializeField] private GameObject pauseMenu;
    //[SerializeField] private GameObject gameMenu;

    [SerializeField] private TrackManager trackManager;

    [SerializeField] private GameObject startUI;
    [SerializeField] private TMP_Text countdownText;
    [SerializeField] private GameObject redCircle;
    [SerializeField] private GameObject yellowCircle;
    [SerializeField] private GameObject greenCircle;

    [SerializeField] private GameObject raceUI;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text speedText;
    [SerializeField] private TMP_Text lapText;

    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        HideRaceUI();
        timerText.text = "00:00:000";
        speedText.text = "0 km/h";
        lapText.text = "0/0";
        countdownText.text = "Ready ?";
    }

    // Update is called once per frame
    void Update()
    {
        if (trackManager.isTrackStarted)
        {
            timerText.text = DisplayTime(trackManager.trackTimer);
            speedText.text = string.Format("{0:00} km/h", Mathf.RoundToInt(Mathf.Abs(trackManager.GetCarVelocity() * 3.6f)));
            lapText.text = string.Format("{0}/{1}", trackManager.numberOfLapsCrossed + 1, trackManager.numberOfLapsTotal);
        }
    }

    public void DisplayCountdown(string currentCountdown)
    {
        countdownText.text = currentCountdown;
    }

    public void HideStartUI()
    {
        startUI.gameObject.SetActive(false); // Hide countdown after it's done
        ShowRaceUI(); // Show the race UI after countdown ends
    }

    private void ShowRaceUI()
    {
        raceUI.SetActive(true);
    }

    private void HideRaceUI()
    {
        raceUI.SetActive(false);
    }

    private static string DisplayTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60F);
        int seconds = Mathf.FloorToInt(time - minutes * 60);
        int milliseconds = Mathf.FloorToInt((time - minutes * 60 - seconds) * 1000);

        string niceTime = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
        return niceTime;
    }

}

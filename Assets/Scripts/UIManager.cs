using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Track Manager")]
    [SerializeField] private TrackManager trackManager;

    [Header("Audio Manager")]
    [SerializeField] public GameObject audioManager;

    [Header("Starting UI")]
    [SerializeField] private GameObject startUI;
    [SerializeField] private TMP_Text countdownText;
    [SerializeField] private GameObject circle_3;
    [SerializeField] private GameObject circle_2;
    [SerializeField] private GameObject circle_1;

    private Color redOn = new Color32(0xFF, 0x00, 0x00, 0xFF); // Bright red
    private Color greenOn = new Color32(0x00, 0xFF, 0x00, 0xFF); // Bright green

    [Header("Racing UI")]
    [SerializeField] private GameObject raceUI;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text speedText;
    [SerializeField] private TMP_Text lapText;

    [Header("Ending UI")]
    [SerializeField] private GameObject endUI;
    [SerializeField] private GameObject endUIFirstButton;
    [SerializeField] private TMP_Text finalTimeText;
    [SerializeField] private List<TMP_Text> lapTimerTexts;

    [Header("Pause UI")]
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject pauseUIFirstButton;
    [SerializeField] private GameObject pauseTimeText;

    [Header("IOT")]
    [SerializeField] private SerialHandler serialHandler;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        startUI.SetActive(true);
        raceUI.SetActive(false);
        endUI.SetActive(false);

        timerText.text = "00:00:000";
        speedText.text = "0 km/h";
        lapText.text = "0/0";

        countdownText.text = "Ready ?";
    }

    // Update is called once per frame
    void Update()
    {
        if (trackManager.isTrackRunning)
        {
            timerText.text = DisplayTime(trackManager.trackTimer);
            speedText.text = string.Format("{0:00} km/h", Mathf.RoundToInt(Mathf.Abs(trackManager.GetCarVelocity() * 3.6f)));
            lapText.text = string.Format("{0}/{1}", trackManager.numberOfLapsCrossed + 1, trackManager.numberOfLapsTotal);
            if (serialHandler == null) return;
            //serialHandler.SetSpeed(Mathf.RoundToInt(Mathf.Abs(trackManager.GetCarVelocity() * 3.6f)));
        }
    }

    public void DisplayCountdown(string currentCountdown)
    {
        countdownText.text = currentCountdown;
    }
    public void Light3()
    {
        audioManager.GetComponent<AudioManager>().PlayAudio3();
        circle_3.GetComponent<UnityEngine.UI.Image>().color = redOn;
        //IOT :
        if (serialHandler == null) return;
        //serialHandler.SetRedLed(true);
        serialHandler.SendLEDCommand(1);
    }
    public void Light2()
    {
        audioManager.GetComponent<AudioManager>().PlayAudio2();
        circle_2.GetComponent<UnityEngine.UI.Image>().color = redOn;
        //IOT :
        if (serialHandler == null) return;
        //serialHandler.SetRedLed(false);
        //serialHandler.SetOrangeLed(true);
        serialHandler.SendLEDCommand(0);
        serialHandler.SendLEDCommand(3);
    }
    public void Light1()
    {
        audioManager.GetComponent<AudioManager>().PlayAudio1();
        circle_1.GetComponent<UnityEngine.UI.Image>().color = redOn;
        //IOT :
        if (serialHandler == null) return;
        //serialHandler.SetOrangeLed(false);
        //serialHandler.SetGreenLed(true);
        serialHandler.SendLEDCommand(2);
        serialHandler.SendLEDCommand(5);
    }

    public void LightAll()
    {
        audioManager.GetComponent<AudioManager>().PlayAudioGo();
        circle_3.GetComponent<UnityEngine.UI.Image>().color = greenOn;
        circle_2.GetComponent<UnityEngine.UI.Image>().color = greenOn;
        circle_1.GetComponent<UnityEngine.UI.Image>().color = greenOn;

        //IOT :
        if (serialHandler == null) return;
        //serialHandler.SetOrangeLed(true);
        //serialHandler.SetRedLed(true);
        serialHandler.SendLEDCommand(1);
        serialHandler.SendLEDCommand(3);
        serialHandler.SendLEDCommand(5);
        
        
    }

    public void UnlightAll()
    {
        //IOT :
        if (serialHandler == null) return;
        //serialHandler.SetGreenLed(false);
        //serialHandler.SetOrangeLed(false);
        //serialHandler.SetRedLed(false);
        serialHandler.SendLEDCommand(0);
        serialHandler.SendLEDCommand(2);
        serialHandler.SendLEDCommand(4);
    }

    public void HideStartUI()
    {
        startUI.gameObject.SetActive(false); // Hide countdown after it's done
    }

    public void ShowRaceUI()
    {
        raceUI.SetActive(true);
    }

    public void HideRaceUI()
    {
        raceUI.SetActive(false);
    }

    public void ShowEndUI(List<float> lapTimers)
    {
        endUI.SetActive(true);

        // IOT :
        for (int i = 0; i <2; i++)
        {
            LightAll();
            UnlightAll();
        }

        finalTimeText.text = string.Format("Time : " + DisplayTime(lapTimers.Sum()));
        for (int i = 0; i < lapTimers.Count; i++)
        {
            lapTimerTexts[i].gameObject.SetActive(true);
            lapTimerTexts[i].text = string.Format("Lap {0} : " + DisplayTime(lapTimers[i]), i + 1);
        }
        for (int i = lapTimers.Count; i < lapTimerTexts.Count; i++)
        {
            lapTimerTexts[i].gameObject.SetActive(false);
        }

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(endUIFirstButton);
    }
    public void HideEndUI()
    {
        endUI.SetActive(false);
    }
    public void ShowPauseUI()
    {
        pauseUI.SetActive(true);
        pauseTimeText.GetComponent<TMP_Text>().text = "Time : " + DisplayTime(trackManager.trackTimer);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(pauseUIFirstButton);
    }

    public void HidePauseUI()
    {
        pauseUI.SetActive(false);
    }

    public void PauseGame()
    {
        audioManager.GetComponent<AudioManager>().StopEngineTest();
        audioManager.GetComponent<AudioManager>().StopDrift();
        ShowPauseUI();
        HideRaceUI();
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        HidePauseUI();
        ShowRaceUI();
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        audioManager.GetComponent<AudioManager>().PlayMenuClick();
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMenu()
    {
        audioManager.GetComponent<AudioManager>().PlayMenuClick();
        Time.timeScale = 1;
        SceneManager.LoadScene("MenuScene");
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

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class UIManager : MonoBehaviour
{

    //[SerializeField] private GameObject pauseMenu;
    //[SerializeField] private GameObject gameMenu;

    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text speedText;
    [SerializeField] private TMP_Text lapText;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        //gameMenu.SetActive(true);
        timer = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        timerText.text = DisplayTime();
    }

    public string DisplayTime()
    {
        float time = Time.time - timer;
        int minutes = Mathf.FloorToInt(time / 60F);
        int seconds = Mathf.FloorToInt(time - minutes * 60);
        int milliseconds = Mathf.FloorToInt((time - minutes * 60 - seconds) * 1000);

        string niceTime = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
        return niceTime;
    }

}

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    [Header("Menus")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject levelSelectionMenu;
    [SerializeField] private GameObject carSelectionMenu;
    [SerializeField] private GameObject validationMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject audioManager;

    [Header("First buttons")]
    [SerializeField] private GameObject mainMenuFirstButton;
    [SerializeField] private GameObject levelSelectionMenuFirstButton;
    [SerializeField] private GameObject carSelectionMenuFirstButton;
    [SerializeField] private GameObject validationMenuFirstButton;
    [SerializeField] private GameObject optionsMenuFirstButton;

    [Header("Level and Car preview")]
    [SerializeField] private Image levelSelectedSection;
    [SerializeField] private Image carSelectedSection;
    [SerializeField] private Sprite level1Image;
    [SerializeField] private Sprite level2Image;
    [SerializeField] private Sprite level3Image;
    [SerializeField] private Sprite level4Image;
    [SerializeField] private Sprite level5Image;
    [SerializeField] private Sprite car1Image;
    [SerializeField] private Sprite car2Image;
    [SerializeField] private Sprite car3Image;


    [Header("Controller Guide")]
    [SerializeField] private TMP_Text controllerGuideText;
    [SerializeField] private GameObject controllerGuide;
    [SerializeField] private GameObject keyboardGuide;

    [Header("Options")]
    [SerializeField] private Slider volumeSlider;

    private string levelToLoad;
    private int carToLoad = 0;
    private bool isControllerGuide = true;

    public void LoadMainMenu(GameObject currentMenu)
    {
        audioManager.GetComponent<AudioManager>().PlayMenuClick();
        currentMenu.SetActive(false);
        mainMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(mainMenuFirstButton);
    }

    public void LoadLevelSelectionMenu(GameObject currentMenu)     
    {
        audioManager.GetComponent<AudioManager>().PlayMenuClick();
        currentMenu.SetActive(false);
        levelSelectionMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(levelSelectionMenuFirstButton);
    }

    public void LoadCarSelectionMenu(GameObject currentMenu)
    {
        audioManager.GetComponent<AudioManager>().PlayMenuClick();
        currentMenu.SetActive(false);
        carSelectionMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(carSelectionMenuFirstButton);
    }

    public void LoadValidationMenu(GameObject currentMenu)
    {
        audioManager.GetComponent<AudioManager>().PlayMenuClick();
        if (levelToLoad != null)
        {
            switch (levelToLoad)
            {
                case "FirstLevel":
                    levelSelectedSection.sprite = level1Image;
                    break;
                case "SecondLevel":
                    levelSelectedSection.sprite = level2Image;
                    break;
                case "ThirdLevel":
                    levelSelectedSection.sprite = level3Image;
                    break;
                case "FourthLevel":
                    levelSelectedSection.sprite = level4Image;
                    break;
                case "FifthLevel":
                    levelSelectedSection.sprite = level5Image;
                    break;
            }
        }

        StartCoroutine(SetCarImageWithDelay());

        currentMenu.SetActive(false);
        validationMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(validationMenuFirstButton);
    }

    private IEnumerator SetCarImageWithDelay()
    {
        // Optional: adjust the delay time as needed
        yield return new WaitForSeconds(0.01f);

        if (carToLoad != 0)
        {
            switch (carToLoad)
            {
                case 1:
                    carSelectedSection.sprite = car1Image;
                    break;
                case 2:
                    carSelectedSection.sprite = car2Image;
                    break;
                case 3:
                    carSelectedSection.sprite = car3Image;
                    break;
            }
        }
    }

    public void LoadOptionsMenu(GameObject currentMenu)
    {
        volumeSlider.onValueChanged.AddListener(delegate { VolumeChange(); });
        volumeSlider.value = PlayerPrefs.GetFloat("Volume");
        audioManager.GetComponent<AudioManager>().PlayMenuClick();
        currentMenu.SetActive(false);
        optionsMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionsMenuFirstButton);
    }

    public void LoadLevelScene()
    {
        audioManager.GetComponent<AudioManager>().PlayMenuClick();
        SceneManager.LoadScene(levelToLoad);
    }

    public void SetLevelToLoad(string levelName)
    {
        levelToLoad = levelName; 
        PlayerPrefs.SetString("Level", levelName);
        PlayerPrefs.Save();
    }

    public void SetCar(int carNumber)
    {
        carToLoad = carNumber;
        PlayerPrefs.SetInt("Car", carNumber);
        PlayerPrefs.Save();
    }

    public void SwitchControllerGuide()
    {
        audioManager.GetComponent<AudioManager>().PlayMenuClick();
        if (isControllerGuide)
        {
            controllerGuide.SetActive(false);
            keyboardGuide.SetActive(true);
            controllerGuideText.text = "Keyboard";
        }
        else
        {
            keyboardGuide.SetActive(false);
            controllerGuide.SetActive(true);
            controllerGuideText.text = "Controller";
        }
        isControllerGuide = !isControllerGuide;
    }

    public void QuitGame()
    {
        audioManager.GetComponent<AudioManager>().PlayMenuClick();
        Debug.Log("Successfully quit");
        levelToLoad = null;
        Application.Quit();
    }
    private void VolumeChange()
    {
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
        PlayerPrefs.Save();    
    }
}

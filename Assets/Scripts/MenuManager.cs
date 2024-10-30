using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

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

    [Header("Controller Guide")]
    [SerializeField] private TMP_Text controllerGuideText;
    [SerializeField] private GameObject controllerGuide;
    [SerializeField] private GameObject keyboardGuide;

    [Header("Slider")]
    [SerializeField] private Slider volumeSlider;

    private string levelToLoad;
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
        currentMenu.SetActive(false);
        validationMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(validationMenuFirstButton);
    }

    public void LoadOptionsMenu(GameObject currentMenu)
    {
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
        if (levelName == "") levelToLoad = null;
        else levelToLoad = levelName;
    }

    public void SetCar(int carNumber)
    {
        //TODO
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

    private void Start()
    {
        volumeSlider.onValueChanged.AddListener(delegate { VolumeChange(); });
        volumeSlider.value = PlayerPrefs.GetFloat("Volume");
    }

    private void VolumeChange()
    {
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
        PlayerPrefs.Save();
        Debug.Log("Volume changed");
    }
}

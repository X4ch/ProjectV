using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{

    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject levelSelectionMenu;
    [SerializeField] private GameObject carSelectionMenu;
    [SerializeField] private GameObject optionsMenu;

    [SerializeField] private GameObject mainMenuFirstButton;
    [SerializeField] private GameObject levelSelectionMenuFirstButton;
    [SerializeField] private GameObject carSelectionMenuFirstButton;
    [SerializeField] private GameObject optionsMenuFirstButton;

    private string levelToLoad;

    public void LoadMainMenu(GameObject currentMenu)
    {
        currentMenu.SetActive(false);
        mainMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(mainMenuFirstButton);
    }

    public void LoadLevelSelectionMenu(GameObject currentMenu)     
    {
        currentMenu.SetActive(false);
        levelSelectionMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(levelSelectionMenuFirstButton);
    }

    public void LoadCarSelectionMenu(GameObject currentMenu)
    {
        currentMenu.SetActive(false);
        carSelectionMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(carSelectionMenuFirstButton);
    }

    public void LoadOptionsMenu(GameObject currentMenu)
    {
        currentMenu.SetActive(false);
        optionsMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionsMenuFirstButton);
    }


    //Load the level you want : Level1, Level2, Level3, Level4 or Level5 
    public void LoadLevelScene()
    {
        //SceneManager.LoadScene(levelToLoad);
        SceneManager.LoadScene(levelToLoad);
    }

    //Set the name of level you want to load, if no name has been set, make levelToLoad null
    public void SetLevelToLoad(string levelName)
    {
        if (levelName == "") levelToLoad = null;
        else levelToLoad = levelName;
    }

    public void QuitGame()
    {
        Debug.Log("Successfully quit");
        levelToLoad = null;
        Application.Quit();
    }

}

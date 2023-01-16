using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Exposed

    [SerializeField] Canvas _mainCanvas;
    [SerializeField] GameObject _winScreen;
    [SerializeField] GameObject _gameMenu;

    #endregion

    #region Unity Lyfecycle

    private void Awake()
    {
        HideMenus();
        Debug.Log("JE REPASSE LA");
    }

    void Start()
    {
        HideMenus();
        Debug.Log("JE PASSE DANS LE START");

    }

    void Update()
    {
        EnableAndDisableMenu();
        CheckIfVictory();
    }


    #endregion

    #region Methods

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        HideMenus();
    }

    public void NextLevel(string levelName)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
        HideMenus();
        //_nextLevelNumber++;
        //levelName = "Level" + _nextLevelNumber.ToString();
        //SceneManager.LoadScene(levelName);
    }

    public void ShowGameMenu()
    {
        _isMenuActive = true;
        _mainCanvas.enabled = true;
        _winScreen.SetActive(false);
        _gameMenu.SetActive(true);
    }

    public void ShowWinScreen()
    {
        _isMenuActive = true;
        _mainCanvas.enabled = true;
        _gameMenu.SetActive(false);
        _winScreen.SetActive(true);
    }

    public void HideMenus()
    {
        _isMenuActive = false;
        _mainCanvas.enabled = false;
        _winScreen.SetActive(false);
    }



    public void EnableAndDisableMenu()
    {

        if (Input.GetAxis("Cancel") == 1)
        {
            if (!_cancelPressed)
            {
                if (_isMenuActive == true)
                {
                    HideMenus();
                }
                else
                {
                    ShowGameMenu();
                }
                _cancelPressed = true;
            }
        }
        else
        {
            _cancelPressed = false;
        }
    }

    public void CheckIfVictory()
    {
        int _musicBoxesNumber = 0;
        int _musicBoxesValidated = 0;

        foreach (GameObject musicBoxes in GameObject.FindGameObjectsWithTag("MusicBox"))
        {
            _musicBoxesNumber++;
            if (musicBoxes.GetComponent<AudioSource>().volume == 1)
            {
                _musicBoxesValidated++;
            }
        }
        if (_musicBoxesNumber == _musicBoxesValidated)
        {
            ShowWinScreen();
        }

    }

    


    public void QuitGame()
    {
        Application.Quit();
    }

    #endregion

    #region Private & Protected

    private bool _isMenuActive;
    private bool _cancelPressed;
    //int _nextLevelNumber = 1;


    #endregion
}

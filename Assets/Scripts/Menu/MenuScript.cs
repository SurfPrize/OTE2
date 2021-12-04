using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject menuDetection;
    private Controlos _controls;

    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Escape))
    //    {
    //        if (GameIsPaused)
    //            Resume();
    //        else
    //            Pause();
    //    }
    //}
    private void Awake()
    {
        _controls = new Controlos();
        _controls.UI.Pause.Enable();
        _controls.UI.Pause.performed += Pause_performed;
    }
    private void OnDisable()
    {
        _controls.UI.Pause.performed -= Pause_performed;
        _controls.UI.Pause.Disable();
    }
    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {


        if (GameIsPaused)
        {
            Debug.Log("resume");
            Resume();

        }
        else
        {
            Debug.Log("Pause");
            Pause();

        }
    }

    public void Resume()
    {
        menuDetection.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    public void Pause()
    {
        menuDetection.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void LoadMenu()
    {
        Debug.Log("Loading...");
        SceneManager.LoadScene("Menu");
    }
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
    public void StartGame()
    {
        Debug.Log("Game Started...");
        SceneManager.LoadScene("Butterfly testing scene");
    }

}

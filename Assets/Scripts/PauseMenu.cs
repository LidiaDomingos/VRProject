using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject wristUI;
    public GameObject ray;

    public bool activeWristUI = true;
    // Start is called before the first frame  update
    void Start()
    {
        DisplayWristUi();
    }

    public void PauseButtonPressed(InputAction.CallbackContext context)
    {
        if(context.performed){
            DisplayWristUi();
        }
    }

    public void DisplayWristUi()
    {
        if(activeWristUI){
            wristUI.SetActive(false);
            ray.SetActive(false);
            activeWristUI = false;
            Time.timeScale = 1;
        }else{
            wristUI.SetActive(true);
            ray.SetActive(true);
            activeWristUI = true;
            Time.timeScale = 0;
        }
        wristUI.SetActive(activeWristUI);
    }

    public void RestartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame(){
        Application.Quit();
    }
}

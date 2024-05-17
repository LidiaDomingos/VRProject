using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject wristUI;

    public bool activeWristUI = true;
    //public XRRayInteractor rayInteractor; 
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
            activeWristUI = false;
            Time.timeScale = 1;
        }else{
            wristUI.SetActive(true);
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

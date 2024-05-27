using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class PauseMenu : MonoBehaviour
{
    public GameObject wristUI;
    public XRRayInteractor leftRayInteractor;
    public XRRayInteractor rightRayInteractor;

    public SceneTransitionManager sceneScript;

    public bool activeWristUI = false;

    // Start is called before the first frame  update
    void Start()
    {
        //DisplayWristUi();
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
        leftRayInteractor.gameObject.SetActive(activeWristUI);
        rightRayInteractor.gameObject.SetActive(activeWristUI);
        wristUI.SetActive(activeWristUI);
    }

    public void RestartGame(){
        Time.timeScale = 1;
        sceneScript.GoToSceneAsync(1);
    }

    public void ExitGame(){
        Application.Quit();
    }
}

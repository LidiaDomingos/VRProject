using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerLogic player;
    public SceneTransitionManager sceneScript;
    private bool endOfGame = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if (player.health <= 0 & !endOfGame & player.isPlayerDead){
        //     sceneScript.GoToSceneAsync(0);
        //     endOfGame = true;
        // }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerLogic : MonoBehaviour
{
    public float health = 100f;
    public bool isPlayerDead = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0){
            isPlayerDead = true;
        }
        
    }
}

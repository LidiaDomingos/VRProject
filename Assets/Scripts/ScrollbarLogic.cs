using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScrollbarLogic : MonoBehaviour
{
    public GameObject player;

    public Scrollbar scrollbar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {  
        // Reduz o valor do tamanho da scrollbar em 0.2
        scrollbar.size = player.GetComponent<PlayerLogic>().health / 100f;
        // Garante que o valor do tamanho da scrollbar permaneça entre 0 e 1
        scrollbar.size = Mathf.Clamp01(scrollbar.size);
    }
}

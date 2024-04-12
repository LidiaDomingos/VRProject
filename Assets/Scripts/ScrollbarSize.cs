using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollbarSizeController : MonoBehaviour
{
    public Scrollbar scrollbar;
    public float newSize = 0.5f; // Novo tamanho da scrollbar (entre 0 e 1)

    void Start()
    {
        if (scrollbar == null)
        {
            Debug.LogError("Scrollbar não atribuída ao ScrollbarSizeController!");
            return;
        }

        // Define o novo tamanho da scrollbar
        scrollbar.size = newSize;
    }
}


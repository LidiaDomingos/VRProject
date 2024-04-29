using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordSound : MonoBehaviour
{
    public AudioClip hitSound;
    private AudioSource audioSource;

    void Start()
    {
        // Adicione um componente AudioSource ao objeto se não existir
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        // Verifica se a colisão é com um inimigo
        if (other.CompareTag("Enemy"))
        {
            // Verifica se há um AudioClip atribuído
            if (hitSound != null)
            {
                // Reproduz o som
                audioSource.PlayOneShot(hitSound);
            }
            else
            {
                Debug.LogWarning("Nenhum som atribuído à espada.");
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptBullet : MonoBehaviour
{
     public GameObject particleSystemObject; // Objeto do sistema de partículas
    public float bulletDuration = 0.5f; // Duração do disparo

    void Update()
    {
        // Verifica se o botão de disparo foi pressionado
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot(); // Chama a função de disparo
        }
    }

    void Shoot()
    {
        // Ativa o sistema de partículas
        particleSystemObject.SetActive(true);

        // Desativa o sistema de partículas após um curto período de tempo
        Invoke("DeactivateParticleSystem", bulletDuration);
    }

    void DeactivateParticleSystem()
    {
        // Desativa o sistema de partículas
        particleSystemObject.SetActive(false);
    }

}

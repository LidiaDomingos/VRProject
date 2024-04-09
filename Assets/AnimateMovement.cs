using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateMovement : MonoBehaviour
{
    public Animator animator; // Referência ao Animator

    private Rigidbody rb;

    void Start()
    {
        // Obter referência ao Rigidbody do objeto jogador
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Verificar se o jogador está se movendo
        bool isMoving = rb.velocity.magnitude > 0;

        // Ativar/desativar a flag no Animator para entrar/sair da animação de andar
        animator.SetBool("isWalking", isMoving);
    }
}

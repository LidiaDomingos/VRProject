using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player; 
    Rigidbody rb;
    public float moveSpeed = 3f; 
    public float detectionRange = 10f; 
    public float detectionAttackRange = 3f; 
    public float health = 100f; 
    public GameObject enemy;
    public Animator animator;
    private bool isDead;
    public Renderer[] renderers;
    public Material flashMaterial;
    public float flashDuration = 0.1f;
    private Material[][] originalMaterials;
    private float cooldownTimer = 0f;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody>();
        isDead = false;
        originalMaterials = new Material[renderers.Length][];
        for (int i = 0; i < renderers.Length; i++)
        {
            originalMaterials[i] = new Material[renderers[i].materials.Length];
            for (int j = 0; j < renderers[i].materials.Length; j++)
            {
                originalMaterials[i][j] = renderers[i].materials[j];
            }
        }
    }

    private void FixedUpdate()
    {
        if (health <= 0 && !isDead){
            animator.SetTrigger("Death");
            isDead = true;
            Destroy(enemy, 3f);
        }
        else if (!isDead){
            if (Vector3.Distance(transform.position, player.position) <= detectionRange)
            {
                animator.SetBool("Walk", true);
                transform.LookAt(player);
                if (rb.position.x < player.position.x){
                    Vector3 target = new Vector3(player.position.x - 1, player.position.y, player.position.z - 1);
                    Vector3 newPos = Vector3.MoveTowards(rb.position, target, moveSpeed * Time.fixedDeltaTime);
                    rb.MovePosition(newPos);
                }
                else {
                    Vector3 target = new Vector3(player.position.x + 1, player.position.y, player.position.z + 1);
                    Vector3 newPos = Vector3.MoveTowards(rb.position, target, moveSpeed * Time.fixedDeltaTime);
                    rb.MovePosition(newPos);
                }
            }
            if (Vector3.Distance(transform.position, player.position) <= detectionAttackRange){
                cooldownTimer -= Time.deltaTime;
                if (cooldownTimer <= 0)
                {
                    cooldownTimer = 5;
                    animator.SetTrigger("Attack");
                }
            }

            
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("weapon"))
        {   
            animator.SetTrigger("Hit");
            health = health - 25;
            StartCoroutine(FlashRed());
        }
    }

    IEnumerator FlashRed()
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            Material[] flashMaterials = new Material[originalMaterials[i].Length];

            for (int j = 0; j < flashMaterials.Length; j++)
            {
                flashMaterials[j] = flashMaterial;
            }

            renderers[i].materials = flashMaterials;
        }

        yield return new WaitForSeconds(flashDuration);

        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].materials = originalMaterials[i];
        }
    }


}

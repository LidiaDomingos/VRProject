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
    public Animator animator;
    private bool isDead;
    public Renderer[] renderers;
    public Material flashMaterial;
    public float flashDuration = 0.1f;
    private Material[][] originalMaterials;
    private float cooldownTimer = 0f;

    public AudioClip death_audio;
    public AudioClip damage_audio;
    public AudioClip punch_audio;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
        if (health <= 0f && !isDead){
            animator.SetTrigger("Death");
            audioSource.PlayOneShot(death_audio);
            player.GetComponent<PlayerLogic>().score += 10;
            isDead = true;
            SpawnManager spawnManager = FindObjectOfType<SpawnManager>();
            if (spawnManager != null)
            {
                spawnManager.EnemyDefeated();
            }
            Destroy(gameObject, 1.5f);
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
                if (cooldownTimer <= 0f)
                {
                    if (!player.GetComponent<PlayerLogic>().isPlayerDead){
                        player.GetComponent<PlayerLogic>().health -= 10f;
                    }
                    cooldownTimer = 5f;
                    animator.SetTrigger("Attack");
                    audioSource.PlayOneShot(punch_audio);
                }
            }

            
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("weapon") && !isDead)
        {   
            animator.SetTrigger("Hit");
            health = health - 25f;
            audioSource.PlayOneShot(damage_audio);
            StartCoroutine(FlashRed());
            Destroy(collider.gameObject);
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

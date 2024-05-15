using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged : MonoBehaviour
{
    public Transform player; 
    Rigidbody rb;
    public float moveSpeed = 3f; 
    private float detectionRange = 30f; 
    private float detectionAttackRange = 8f; 
    public float health = 100f; 
    private bool isDead;

    //flash red when hit
    public Renderer[] renderers;
    public Material flashMaterial;
    public float flashDuration = 0.1f;
    private Material[][] originalMaterials;
    private float cooldownTimer = 0f;
    public Animator animator;

    //projectiles 
    public GameObject projectilePrefab;
    public Transform shootSource;
    public float projectileSpeed = 10f;

    public bool isAttacking = false;

    public AudioClip death_audio;
    public AudioClip damage_audio;
    public AudioClip shoot_audio;
    private AudioSource audioSource;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        audioSource = GetComponent<AudioSource>();
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
            audioSource.PlayOneShot(death_audio);
            animator.SetTrigger("Death");
            isDead = true;
            SpawnManager spawnManager = FindObjectOfType<SpawnManager>();
            if (spawnManager != null)
            {
                spawnManager.EnemyDefeated();
            }
            Destroy(gameObject, 1.5f);
        }
        else if (!isDead){
            transform.LookAt(player);
            if (Vector3.Distance(transform.position, player.position) <= detectionRange && Vector3.Distance(transform.position, player.position) > detectionAttackRange)
            {
                animator.SetBool("Idle", false);
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
            else {
                animator.SetBool("Idle", true);
            }
            if (Vector3.Distance(transform.position, player.position) <= detectionAttackRange){
                cooldownTimer -= Time.deltaTime;
                if (cooldownTimer <= 0f)
                {
                    if (!player.GetComponent<PlayerLogic>().isPlayerDead){
                        player.GetComponent<PlayerLogic>().health -= 5f;
                    }
                    cooldownTimer = 5f;
                    animator.SetTrigger("Attack");
                    Attack();
                }
            }

            
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("weapon") && !isDead)
        {   
            audioSource.PlayOneShot(damage_audio);
            animator.SetTrigger("Hit");
            health = health - 25f;
            Destroy(collider.gameObject);
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

    private void Attack(){
        GameObject projectileInstance = Instantiate(projectilePrefab, shootSource.position, Quaternion.identity);
        
        projectileInstance.SetActive(true);
        StartCoroutine(MoveProjectile(projectileInstance, shootSource.forward));
        audioSource.PlayOneShot(shoot_audio);
    }

    private IEnumerator MoveProjectile(GameObject projectile, Vector3 direction)
    {
        float elapsedTime = 0f;

        while (elapsedTime < 3f && projectile != null) // Tempo de vida do projétil (ajuste conforme necessário)
        {
            float step = projectileSpeed * Time.deltaTime;
            projectile.transform.Translate(direction * step);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        Destroy(projectile);
    }

}
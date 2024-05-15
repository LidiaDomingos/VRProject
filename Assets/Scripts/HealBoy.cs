using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealBoy : MonoBehaviour
{
    public Transform player; 
    Rigidbody rb;
    public Animator animator;
    public float detectionRange = 3f; 
    public float health = 100f; 
    public float moveSpeed = 3f; 
    private bool isNear = false;
    private bool isDead = false;
    public AudioClip damage_audio;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        isNear = false;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!isDead){
            if (Vector3.Distance(transform.position, player.position) >= detectionRange){
                transform.LookAt(player);
                Vector3 target = new Vector3(player.position.x - 1, player.position.y, player.position.z - 1);
                Vector3 newPos = Vector3.MoveTowards(rb.position, target, moveSpeed * Time.fixedDeltaTime);
                rb.MovePosition(newPos);
            }
            else {
                if( isNear == false){
                    isNear = true;
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                    animator.SetTrigger("Heal");
                    Heal();
                }
            }
        }


    }

    private void Heal(){
        if (!player.GetComponent<PlayerLogic>().isPlayerDead){
            player.GetComponent<PlayerLogic>().health += 10f;
            isDead = true;
            SpawnManager spawnManager = FindObjectOfType<SpawnManager>();
            if (spawnManager != null)
            {
                spawnManager.EnemyDefeated();
            }
            Destroy(gameObject, 1.5f);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("weapon") & !isDead)
        {   
            health = health - 100f;
            isDead = true;
            audioSource.PlayOneShot(damage_audio);
            animator.SetTrigger("Death");
            SpawnManager spawnManager = FindObjectOfType<SpawnManager>();
            if (spawnManager != null)
            {
                spawnManager.EnemyDefeated();
            }
            Destroy(collider.gameObject, 1.5f);
            Destroy(gameObject, 1.5f);
        }
    }

}

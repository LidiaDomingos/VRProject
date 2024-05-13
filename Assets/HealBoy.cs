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
    public GameObject enemy;
    // public AudioClip damage_audio;
    // private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody>();
        isNear = false;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, player.position) >= detectionRange){
            transform.LookAt(player);
            Vector3 target = new Vector3(player.position.x - 1, player.position.y, player.position.z - 1);
            Vector3 newPos = Vector3.MoveTowards(rb.position, target, moveSpeed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
        }
        else {
            animator.SetTrigger("Heal");
        }

    }



    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("weapon"))
        {   
            health = health - 100;
            //audioSource.PlayOneShot(death_audio);
            animator.SetTrigger("Death");
            SpawnManager spawnManager = FindObjectOfType<SpawnManager>();
            if (spawnManager != null)
            {
                spawnManager.EnemyDefeated();
            }
            Destroy(enemy, 3f);
        }
    }

}

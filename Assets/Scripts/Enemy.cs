using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player; 
    Rigidbody rb;
    public float moveSpeed = 3f; 
    public float detectionRange = 10f; 
    public float health = 100f; 
    public GameObject enemy;
    public Animator animator;
    private bool isDead;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody>();
        isDead = false;
    }

    private void Update()
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
            else
            {
                animator.SetBool("Walk", false);
            }
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log(collider);
        if (collider.CompareTag("weapon"))
        {   
            animator.SetTrigger("Hit");
            health = health - 25;
        }
    }

}

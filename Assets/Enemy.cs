using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player; 
    public float moveSpeed = 5f; 

    public float health = 100f;

    void Update()
    {
        if (player != null) 
        {
            Vector3 direction = (player.position - transform.position).normalized;

            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }

    void OnTriggerEnter3D(Collider collider)
    {
        if (collider.CompareTag("weapon"))
        {
            health = health - 10;
            Debug.Log("entrei aqui");
        }
    }

}

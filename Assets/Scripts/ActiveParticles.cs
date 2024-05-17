using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ActiveParticles : MonoBehaviour
{
    public GameObject projectilePrefab; // Prefab do projetil
    public GameObject GunTransform; // Prefab do projetil
    public Transform shootSource; // Ponto de origem do disparo
    private Transform initialPosition;
    public LayerMask layerMask;
    public float distance = 10;
    public float projectileSpeed = 10; // Velocidade do projetil
    public AudioClip shoot_audio;
    AudioSource audioSource;

    void Start()
    {
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.activated.AddListener(x => StartShoot());
        audioSource = GetComponent<AudioSource>();
        initialPosition = GunTransform.transform;
    }

    void FixedUpdate()
    {
        if (this.transform.position.y < 0.2f){
            this.transform.position = initialPosition.position;
            this.transform.rotation = initialPosition.rotation;
        }
    }

    public void StartShoot()
    {
        // Instancia o projetil no ponto de origem do disparo
        GameObject projectileInstance = Instantiate(projectilePrefab, shootSource.position, Quaternion.identity);

        projectileInstance.SetActive(true);
        audioSource.PlayOneShot(shoot_audio);
        StartCoroutine(MoveProjectile(projectileInstance, -shootSource.forward));
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
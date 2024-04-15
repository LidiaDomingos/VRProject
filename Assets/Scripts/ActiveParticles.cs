using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ActiveParticles : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform shootSource;
    public float distance = 10;
    public float projectileSpeed = 10;

    void Start()
    {
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.activated.AddListener(x => StartShoot());
    }

    public void StartShoot()
    {
        GameObject projectileInstance = Instantiate(projectilePrefab, shootSource.position, Quaternion.identity);
        
        projectileInstance.SetActive(true);
        StartCoroutine(MoveProjectile(projectileInstance, -shootSource.forward));
    }

    private IEnumerator MoveProjectile(GameObject projectile, Vector3 direction)
    {
        float elapsedTime = 0f;

        while (elapsedTime < 3f && projectile != null) 
        {
            float step = projectileSpeed * Time.deltaTime;
            projectile.transform.Translate(direction * step);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        Destroy(projectile);
    }

}

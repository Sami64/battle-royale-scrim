using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float explosionDuration = 0.25f;
    public float explosionWaitDuration = 5f;
    float explosionTimer;
    public float radius = 10f;
    bool exploded;

    public GameObject explosionModel;

    void Start()
    {
        explosionTimer = explosionWaitDuration;
        explosionModel.transform.localScale = Vector3.one * radius;
        explosionModel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        explosionTimer -= Time.deltaTime;
        if(explosionTimer <= 0f && !exploded)
        {
            exploded = true;
            Collider[] explodedObjects = Physics.OverlapSphere(transform.position, radius);
            foreach(Collider explodedObject in explodedObjects)
            {
                Debug.Log($"{explodedObject.name} was exploded!");
            }
            StartCoroutine(Explode());
        }
    }

    private IEnumerator Explode()
    {
        explosionModel.SetActive(true);
        yield return new WaitForSeconds(explosionDuration);
        Destroy(gameObject);
    }
}

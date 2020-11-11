using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    [SerializeField]
    GameObject arrowPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Shoot()
    {
        GameObject arrow = Instantiate(arrowPrefab);
        arrow.transform.position = transform.position + transform.forward;
        arrow.transform.forward = transform.forward;
    }
}

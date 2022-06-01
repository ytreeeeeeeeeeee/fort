using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float damage;

    [SerializeField] float fireRate = 1;
    [SerializeField] float force = 155;
    [SerializeField] float range = 15;
    [SerializeField] Camera _cam;

    float nextFire = 0;

    void Start()
    {
        
    }
    void Update()
    {
        if (_cam.GetComponent<Aim>().ex == false && Input.GetMouseButton(0) && Time.time > nextFire)
        {
            nextFire = Time.time + 1 / fireRate;
            Shoot();
        }
    }
    void Shoot()
    {
        RaycastHit hit;

        if (Physics.Raycast(_cam.transform.position, _cam.transform.forward, out hit, range))
        {
            if (hit.collider.tag == "Player" && hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * force);
            }
        }
    }
}

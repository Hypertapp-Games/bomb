using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float life = 3;

    void Awake()
    {
        Destroy(gameObject, life);
    }

    void OnCollisionEnter(Collision collision)
    {
            
        if (collision.gameObject.name != "RayCastFinish" )
        {
            Destroy(gameObject);
        }

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Ball(Clone)")
        {
            gameObject.GetComponent<SphereCollider>().isTrigger = false;
        }
    }
}

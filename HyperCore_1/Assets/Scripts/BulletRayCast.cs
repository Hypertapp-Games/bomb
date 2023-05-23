using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using Color = UnityEngine.Color;

public class BulletRayCast : MonoBehaviour
{
    public LayerMask _target;
    public LayerMask border;
    private Vector3 origin;
    private Vector3 direction;
    [SerializeField] public float lifetime = 4;
    [SerializeField] float bulletSpeed = 10;
    [SerializeField] float powershot = 500;
    [SerializeField] float distance;
    [SerializeField] float radius;
    [SerializeField] float delayTime = 0.1f;
    public GameObject effect;
    public GameObject BulletHitSound;

    
    void Awake()
    {
        radius = gameObject.GetComponent<SphereCollider>().radius/2;
    }
    void FixedUpdate()
    {
        distance = bulletSpeed * Time.deltaTime;
        
        origin = transform.position;
        direction = transform.forward;
        RaycastHit hit;
        if(Physics.SphereCast(origin,radius ,direction, out hit, distance, _target, QueryTriggerInteraction.UseGlobal))
        {
            CollideTarget(hit.transform.gameObject, hit.point); // phan nay de tac dung luc
        }
        transform.position += distance * transform.forward;
    }
    void CollideTarget(GameObject target , Vector3 hitPoint)
    {
        Instantiate(BulletHitSound);
        var direction = target.transform.position - hitPoint;
        target.GetComponent<Rigidbody>().AddForce(direction * powershot, ForceMode.Force);
        StartCoroutine(DestroyBullet(delayTime));
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(origin, origin+direction*distance);
        Gizmos.DrawWireSphere(origin+direction*distance, radius);
    }
    public IEnumerator DestroyBullet(float Delaytime)
    {
        yield return new WaitForSeconds(Delaytime);
        Instantiate(effect, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }
    public IEnumerator DestroyBullet2(float Delaytime)
    {
        yield return new WaitForSeconds(Delaytime);
        gameObject.SetActive(false);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Target : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]int damage;
    [SerializeField]GameManager gameManager;
    [SerializeField] private int minDamage = 30;
    [SerializeField] private int maxDamage = 100;
    [Header("CheckinList")] 
    public bool isInListPrioritize1 = false;
    public bool isInListPrioritize2 = false;
    public float limit1 = -4.19f;
    public float limit2 = -13.08f;
    public GameObject effect;
    public GameObject pointlight;
    bool Explored = false;
    Vector3 Anchor = new Vector3();
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        damage = Random.Range(minDamage, maxDamage);
        float scaleSize = (float)damage / 60; ;
        transform.localScale = new Vector3(1* scaleSize , 1* scaleSize , 1* scaleSize );
    }

    private void Update()
    {
        if (transform.position.z > limit1)
        {
            if (isInListPrioritize1 == false)
            {
                gameManager.listPrioritize1.Add(gameObject);
                isInListPrioritize1 = true;
            }
            
        }
        else if (transform.position.z < limit2)
        {
            if (isInListPrioritize2 == false)
            {
                gameManager.listPrioritize2.Add(gameObject);
                isInListPrioritize2 = true;
            }
            
        }
        else
        {
            if (isInListPrioritize1 == true)
            {
                gameManager.listPrioritize1.Remove(gameObject);
                isInListPrioritize1 = false;
            }
            else
            {
                gameManager.listPrioritize2.Remove(gameObject);
                isInListPrioritize2 = false;
            }
        }
        if(Explored == true)
        {
            transform.position = Anchor;
        }
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (Explored == false)
        {
            if (collision.gameObject.GetComponent<Tower>() != null)
            {
                collision.gameObject.GetComponent<Tower>().BloodDeducted(damage);
                Expl(collision.gameObject);

            }

            if (collision.gameObject.GetComponent<TowerInRayMode>() != null)
            {
                collision.gameObject.GetComponent<TowerInRayMode>().BloodDeducted(damage);
                Expl(collision.gameObject);
            }
        }
        
    }

    public void Expl(GameObject col)
    {
        Explored = true;
        Anchor = gameObject.transform.position;
        var midpoint = gameObject.transform.position;// + col.transform.position) / 2f;
        var eff  = Instantiate(effect, midpoint+new Vector3(0,0.5f,0), Quaternion.identity);
        //Instantiate(pointlight, midpoint + new Vector3(0, 1.5f, 0), Quaternion.identity);
        eff.transform.localScale = eff.transform.localScale * gameObject.transform.localScale.x;
        gameManager.RemoveBall(gameObject);
        col.GetComponent<Tower>().InstanceExplosionSound();
        StartCoroutine(Explore(0.2f));
    }
    public IEnumerator Explore(float time)
    {
        StartCoroutine(time.Tweeng( (p)=>gameObject.transform.localScale = p,
            gameObject.transform.localScale,
            gameObject.transform.localScale*1.3f));
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}

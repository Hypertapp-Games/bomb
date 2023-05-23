using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoShot : MonoBehaviour
{
    // Start is called before the first frame update
    public float BulletSpeed;
    private Tower _tower;
    
    GameManager _gameManager;

    public ObjectPool objpool;
    void Start()
    {
        _tower = gameObject.GetComponent<Tower>();
        _gameManager = _tower.gameManager;
    }

    public void StarAutoShot()
    {
        StartCoroutine(AutoShotByTime());
    }

    void InstanceBullet()
    {
        _tower.InstanceBullet();
    }
    public IEnumerator AutoShotByTime()
    {
        while(true)
        {
            var delaytime = BulletSpeed;
            yield return new WaitForSeconds(delaytime);
            InstanceBullet();
        }
    }
}

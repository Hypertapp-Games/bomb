using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BotAutoTarget : MonoBehaviour
{
    // Start is called before the first frame update
    public float minBulletSpeed;
    public float maxBulletSpeed;
    private Tower _botTower;
    
    GameManager _gameManager;

    public bool isRayMode = false;
    private GameObject targetGameObject;
    private Vector3 startPosition;
    private float elapsedTime;
    private float desiredDuration;

    public ObjectPool objpool;


    void Start()
    {
        _botTower = gameObject.GetComponent<Tower>();
        _gameManager = _botTower.gameManager;
        if (PlayerPrefs.GetInt("EditMode") == 0)
        {
            var a = (9 - PlayerPrefs.GetInt("LevelSpeed")) * 0.05f;
            minBulletSpeed = minBulletSpeed + a;
            maxBulletSpeed = maxBulletSpeed + a;
        }
    }
    void InstanceBullet()
    {
        _botTower.InstanceBullet();
        
    }
    public void AutoTargetBasicMode() // trong che do basic dan se ban sau khi chon duoc muc tieu
    {
        if(_gameManager.target.Count > 0)
        {
            var direction = GetRandomTarget().transform.position - transform.position;
            transform.forward = direction;
            InstanceBullet();
        }
    }
    public IEnumerator AutoTargetByTime()
    {
        while (true)
        {
            var delaytime = Random.Range(minBulletSpeed, maxBulletSpeed);
            yield return new WaitForSeconds(delaytime);
            AutoTargetBasicMode();
        }
    }

    public void StartAutoShot()
    {
        if (isRayMode == false)
        {
            
            StartCoroutine(AutoTargetByTime());
        }
        else
        {
            StartCoroutine(BotAutoTargetAndMoveInRayMode());
        }
        
    }

    private void Update()
    {
        if (isRayMode == true)
        {
            elapsedTime += Time.deltaTime;
            float percent = elapsedTime / desiredDuration;
            Vector3 targetPosition = new Vector3(0,transform.position.y,transform.position.z);
            try
            {
                targetPosition = new Vector3(targetGameObject.transform.position.x, transform.position.y, transform.position.z);
            }
            catch 
            {
            }
            
            transform.position = Vector3.Lerp(startPosition, targetPosition, Mathf.SmoothStep(0,1,percent) );
        }
    }

    //Doan nay la danh cho ray mode;
    public IEnumerator BotAutoTargetAndMoveInRayMode()
    {
        while (true)
        {
            var time = Random.Range(minBulletSpeed, maxBulletSpeed);
            yield return new WaitForSeconds(time); // delay giua cac lan banl
            if(_gameManager.target.Count > 0)
            {
                startPosition = transform.position;
                targetGameObject = GetRandomTarget();
                elapsedTime = 0;
                desiredDuration = (time - 0.05f);
                StartCoroutine(AutoShotByTime(time-0.05f)); // sau x time di chuyen se ban;

            }
        }
        
    }
    public IEnumerator AutoShotByTime(float Delaytime)
    {
        yield return new WaitForSeconds(Delaytime);
        InstanceBullet();
    }
    
    
    //Code lay muc tieu, dung chung cho ca 2 mode;
    public GameObject GetRandomTarget()
    {
        if (_gameManager.level == 1)
        {
            int randomNumber = Random.Range(0, 100);
            float sum = _gameManager.chane + _gameManager.chane1 + _gameManager.chane2;
            var rate = Math.Round((_gameManager.chane/sum) * 100);
            var rate1 = Math.Round((_gameManager.chane1/sum) * 100);
            var rate2 = Math.Round((_gameManager.chane2/sum) * 100);
            try
            {
                if (randomNumber < rate)
                {
                    return _gameManager.target[Random.Range(0, _gameManager.target.Count)];
                }
                else if (randomNumber <= rate + rate1)
                {
                    return _gameManager.listPrioritize1[Random.Range(0, _gameManager.listPrioritize1.Count)];
                }
                else
                {
                    return _gameManager.listPrioritize2[Random.Range(0, _gameManager.listPrioritize2.Count)];
                }
            }
            catch 
            {
                return _gameManager.target[Random.Range(0, _gameManager.target.Count)];
            }
            
        }
        else
        {
            return _gameManager.target[Random.Range(0, _gameManager.target.Count)];
        }
    }
}

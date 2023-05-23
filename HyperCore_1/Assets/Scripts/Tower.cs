using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Tower : MonoBehaviour
{
    [SerializeField] public GameManager gameManager;
    [SerializeField] public Transform bulletSpawnPoint;
    [SerializeField] public GameObject bulletPrefab;

    [SerializeField] public bool IsBot = false;
    [SerializeField] public bool IsRayMode = false;

    [SerializeField] private Slider HpBar;
    [SerializeField] private TextMeshProUGUI HpText;

    [SerializeField] int Hp = 0;
    [SerializeField] int currentHP = 200;
    [SerializeField] private int hpAfterDeducted = 0;
    public ObjectPool objpool;
    public ObjectPool SoundPool;
    public ObjectPool ExplosionSoundPool;

    public void InstanceBullet()
    {
        GameObject bullet = objpool.GetPooledObject();
        if (bullet != null)
        {
            bullet.transform.position = bulletSpawnPoint.position;
            bullet.SetActive(true);
            bullet.transform.forward = bulletSpawnPoint.forward;
            var _bulletrc = bullet.GetComponent<BulletRayCast>();
            StartCoroutine(_bulletrc.DestroyBullet2(_bulletrc.lifetime));
        }

        InstanceBulletSound();
    }

    public void InstanceBulletSound()
    {
        GameObject bulletSound = SoundPool.GetPooledObject();
        bulletSound.SetActive(true);
        StartCoroutine(bulletSound.GetComponent<HideObject>().Hide());
        bulletSound.GetComponent<AudioSource>().Play();
    }
    
    public void InstanceExplosionSound()
    {
        GameObject explosionSound = ExplosionSoundPool.GetPooledObject();
        explosionSound.SetActive(true);
        StartCoroutine(explosionSound.GetComponent<HideObject>().Hide());
        explosionSound.GetComponent<AudioSource>().Play();
    }
    public void StartHpSmooth()
    {
        StartCoroutine(1.5f.Tweeng(x => { Hp = (int)x; }, 0, currentHP));
    }

    public void TapShot(Vector3 targetpoint) // 
    {
        if(IsRayMode == false)
        {
            var direction = targetpoint - transform.position;
            transform.forward = direction;
            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.transform.forward = bulletSpawnPoint.forward;
        }
    }
    public void StartAutoShot() 
    {
        gameObject.GetComponent<AutoShot>().StarAutoShot();
    }
    public void BootStartShot()
    {
        gameObject.GetComponent<BotAutoTarget>().StartAutoShot();
    }

    private void Update()
    {
        if (Hp < 0)
        {
            Hp = 0;
        }
        HpText.text = Hp + "/" + 200;
        HpBar.GetComponent<Slider>().value = (float)Hp / 200;
    }

    public void BloodDeducted(int Damage)
    {
        hpAfterDeducted = currentHP - Damage;
        StartCoroutine(BloodDeductedSmooth(0.3f));
        currentHP = hpAfterDeducted;
    }
    
    public void EndGame()
    {
        if(Hp <= 0) {
           GameObject.Find("GameManager").GetComponent<UIManager>().EndGame(IsBot);
        }
        
    }

    public IEnumerator BloodDeductedSmooth(float time)
    {
        StartCoroutine(time.Tweeng(x => { Hp =(int)x; }, currentHP, hpAfterDeducted));
        yield return new WaitForSeconds(time);
        EndGame();


    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetLevel : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_InputField playerSpeed;
    public TMP_InputField botMin;
    public TMP_InputField botMax;

    public AutoShot player;
    public BotAutoTarget bot;

    private float ps;
    private float bmin;
    private float bmax;

    private void Start()
    {
        if (PlayerPrefs.GetFloat("PBS") != 0)
        {
            playerSpeed.text = PlayerPrefs.GetFloat("PBS").ToString();
            player.BulletSpeed = PlayerPrefs.GetFloat("PBS");
        }
        else
        {
            playerSpeed.text = player.BulletSpeed.ToString();
        }

        
        
        if (PlayerPrefs.GetFloat("BMinBS") != 0)
        {
            botMin.text = PlayerPrefs.GetFloat("BMinBS").ToString();
            bot.minBulletSpeed = PlayerPrefs.GetFloat("BMinBS");
        }
        else
        {
            botMin.text = bot.minBulletSpeed.ToString();
        }
        
        
        if (PlayerPrefs.GetFloat("BMaxBS") != 0)
        {
            botMax.text = PlayerPrefs.GetFloat("BMaxBS").ToString();
            bot.maxBulletSpeed = PlayerPrefs.GetFloat("BMaxBS");
        }
        else
        {
            botMax.text = bot.maxBulletSpeed.ToString();
        }
    }

    public void ChangPlayerSpeed()
    {
        float a = 0;
        float b = 0;
        try
        {
            a = float.Parse(playerSpeed.text);
        }
        catch
        {
            checkblabla(playerSpeed);
        }

        if (a < 0.06f)
        {
            a = 0.06f;
            checkblabla(playerSpeed);
        }
        player.BulletSpeed = a;
        PlayerPrefs.SetFloat("PBS", a);
        player.gameObject.SetActive(false);
        player.gameObject.SetActive(true);
        player.StarAutoShot();
       
        
    }
    public void ChangBotSpeed()
    {
        float a = 0;
        float b = 0;
        try
        {
            a = float.Parse(botMin.text);
        }
        catch
        {
            checkblabla(botMin);
        }

        if (a < 0.06f)
        {
            a = 0.06f;
            checkblabla(botMin);
        }

        try
        {
            b = float.Parse(botMax.text);
        }
        catch
        {
            checkblabla(botMax);
        }
        if (b < 0.06f)
        {
            b = 0.06f;
            checkblabla(botMax);
        }

        bot.minBulletSpeed = a;
        PlayerPrefs.SetFloat("BMinBS", a);
        bot.maxBulletSpeed = b;
        PlayerPrefs.SetFloat("BMaxBS", b);
        bot.gameObject.SetActive(false);
        bot.gameObject.SetActive(true);
        bot.StartAutoShot();

    }

    public void checkblabla(TMP_InputField tiF)
    {
        tiF.text = "0.6";
    }
}

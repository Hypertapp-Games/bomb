using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class LevelManager : MonoBehaviour
{
    public GameObject SettingPanel;
    public Toggle tick;
    private int EditMode;
    public AudioSource _audioSource;
    public bool IsTrue = false;
    public void Start()
    {
        EditMode = PlayerPrefs.GetInt("EditMode");
        SettingPanel.gameObject.SetActive(false);
        PlayerPrefs.DeleteKey("PMinBS");
        PlayerPrefs.DeleteKey("PMaxBS");
        PlayerPrefs.DeleteKey("BMinBS");
        PlayerPrefs.DeleteKey("BMaxBS");
        if (EditMode == 0)
        {
            tick.isOn = false;
        }
        else if( EditMode == 1)
        {
            tick.isOn = true;
        }
        //PlayerPrefs.SetInt("Level",0);
    }

    public TMP_Dropdown changeLevel;
    public TMP_Dropdown changeSpeed;

    public void Awake()
    {
        try
        {
            changeLevel.value =  PlayerPrefs.GetInt("Level");
        }
        catch 
        {
        }
        try
        {
            changeSpeed.value =  PlayerPrefs.GetInt("LevelSpeed");
        }
        catch 
        {
        }
        StartCoroutine(StartSound(0.01f));
        
    }

    public void LoadBasicModeScene()
    {
        PlayAudio();
        StartCoroutine(Hold1(0.1f));
        //SceneManager.LoadScene(1);
    }
    public void LoadRayModeScene()
    {
        PlayAudio();
        StartCoroutine(Hold2(0.1f));
        //SceneManager.LoadScene(2);
    }

    public void HandleInputData(int val)
    {
        if (val == 0)
        {
            //Debug.Log("easy");
            PlayerPrefs.SetInt("Level",val);
        }

        if (val == 1)
        {
            //Debug.Log("normal");
            PlayerPrefs.SetInt("Level",val);
        }

        PlayAudio();
    }

    public void quitbtn()
    {
        Application.Quit();
        PlayAudio();
    }

    public void OpenSetting()
    {
        SettingPanel.gameObject.SetActive(true);
        PlayAudio();
    }

    public void EditModeTick(bool toggle)
    {
        if (toggle == true)
        {
            PlayerPrefs.SetInt("EditMode",1);
        }
        else 
        {
            PlayerPrefs.SetInt("EditMode",0);
        }

        PlayAudio();
    }
    public void HandleSpeed(int val)
    {
        PlayerPrefs.SetInt("LevelSpeed",val);
        PlayAudio();
    }
    

    public void CloseSetting()
    {
        SettingPanel.gameObject.SetActive(false);
        PlayAudio();
    }

    public void PlayAudio()
    {
        if(IsTrue == true) { _audioSource.Play(); }
        
    }
    public IEnumerator StartSound(float time)
    {
        yield return new WaitForSeconds(time);
        IsTrue = true;
    }
    public IEnumerator Hold1(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(1);
    }
    public IEnumerator Hold2(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(2);
    }

}

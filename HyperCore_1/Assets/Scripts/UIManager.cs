using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    //[SerializeField] GameObject btnPanel;
    //[SerializeField] GameObject btnNewGame;
    [Header("Name Panel")] 
    [SerializeField] GameObject nameVSNamePanel;
    [SerializeField] GameObject lookingforplayersText;
    [SerializeField] GameObject enemyAvatarFrame;
    [SerializeField] TextMeshProUGUI enemyName;
    [SerializeField] Image enemyAvatarImage;
    
    [Header("HP Panel")] 
    [SerializeField] GameObject hpPanel;
    [SerializeField] Image hpPanelEnemyAvatar;
    [SerializeField] private TextMeshProUGUI hpPanelEnemyName;
    
    [Header("End game Panel")] 
    [SerializeField] GameObject endGamePanel;
    [SerializeField] TextMeshProUGUI winOrLose;

    [Header("Set bullet speed Panel")] 
    [SerializeField] GameObject setSpeedPanel;

    GenerateAIName generateAIName;
    GenerateAIAvatar generateAIAvatar;
    GameManager gameManager;
    
    [Header("Time to gen AI")] 
    [SerializeField] private float minTimeToGenerateAI = 1;
    [SerializeField] private float maxTimeToGenerateAI = 5;

    [Header("SFX")]
    public AudioSource audioSource;
    int a = 0;

    void Start()
    {
        nameVSNamePanel.gameObject.SetActive(true);
        NewGame();
        //btnNewGame.gameObject.SetActive(true);
        
        generateAIName = gameObject.GetComponent<GenerateAIName>();
        generateAIAvatar = gameObject.GetComponent<GenerateAIAvatar>();
        gameManager = gameObject.GetComponent<GameManager>();

        //StartCoroutine(0.2f.Tweeng((p) => lookingforplayersText.transform.localScale = p,
        //      lookingforplayersText.transform.localScale,
        //        new Vector3(2, 2, 2)));

    }
    public void NewGame()
    {
        //btnNewGame.gameObject.SetActive(false);
        //nameVSNamePanel.gameObject.SetActive(true);
        StartCoroutine(GenerateAI(Random.Range(minTimeToGenerateAI,maxTimeToGenerateAI)));
    }

    public IEnumerator GenerateAI(float time) // AI se duoc generate sau 1 khoang thoi gian de fake viec tim kiem nguoi choi
    {
        yield return new WaitForSeconds(time);
        
        lookingforplayersText.gameObject.SetActive(false);
        enemyAvatarFrame.gameObject.SetActive(true);
        enemyName.text = generateAIName.GetRandomName();
        enemyAvatarImage.sprite = generateAIAvatar.GetRandomSprite();
        
        hpPanel.gameObject.SetActive(true);
        hpPanelEnemyAvatar.sprite = enemyAvatarImage.sprite;
        hpPanelEnemyName.text = enemyName.text;
        if (PlayerPrefs.GetInt("EditMode") == 1)
        {
            setSpeedPanel.gameObject.SetActive(true);
        }
        
        gameManager.GenerateBall(6);
        
        StartCoroutine(CloseUInameVSNamePanel(1)); 
    }
    public IEnumerator CloseUInameVSNamePanel(float time) // Sau khi gen Ai se doi them 1 giay de dong UI va bat dau gane
    {
        yield return new WaitForSeconds(time);
       
        //btnPanel.gameObject.SetActive(false);
        StartCoroutine(WaitToAIReady(0.5f));
    }
    public IEnumerator WaitToAIReady(float time)
    {
        nameVSNamePanel.gameObject.GetComponent<AudioSource>().Play();
        StartCoroutine(1.2f.Tweeng( (p)=>nameVSNamePanel.gameObject.transform.position = p,
            nameVSNamePanel.gameObject.transform.position,
            nameVSNamePanel.gameObject.transform.position + new Vector3(1500, 0, 0)) );
        yield return new WaitForSeconds(time);
        //nameVSNamePanel.gameObject.SetActive(false);
        gameManager.StartGame();
    }
    public void EndGame(bool isBot)
    {
        if (a == 0)
        {
            gameManager.enemyTower.gameObject.SetActive(false);
            gameManager.enemyTower.gameObject.SetActive(true);

            gameManager.myTower.gameObject.SetActive(false);
            gameManager.myTower.gameObject.SetActive(true);

            StartCoroutine(PopUpEndGamePanel(1, isBot));
            a = 1;
        }
       
    }
    public IEnumerator PopUpEndGamePanel(float time , bool isBot)
    {
        yield return new WaitForSeconds(time);
        
        EndGamePanelPopUp(isBot);
    }
    public void EndGamePanelPopUp(bool isBot)
    {
        endGamePanel.gameObject.SetActive(true);
        if(isBot == true)
        {
            Debug.Log(isBot);
            winOrLose.text = "   You Win";
        }
        else
        {
            Debug.Log(isBot);
            winOrLose.text = "   You Lose";

        }
      
    }
    public void rePlayBasic()
    {
        //SceneManager.LoadScene(1);
        audioSource.Play();
        StartCoroutine(Hold1(0.1f));
    }
    public void rePlayRay()
    {
        // SceneManager.LoadScene(2);
        audioSource.Play();
        StartCoroutine(Hold2(0.1f));
    }

    public void quitbtn()
    {
        // SceneManager.LoadScene(0);
        audioSource.Play();
        StartCoroutine(Hold0(0.1f));
    }

    public IEnumerator Hold0(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(0);
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

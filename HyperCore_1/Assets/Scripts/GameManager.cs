using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject targetParent;
    [SerializeField] GameObject ball;
    [SerializeField] public GameObject enemyTower;
    [SerializeField] public GameObject myTower;
    //[Header("Ball Target List")] 
    [HideInInspector] public List<GameObject> target = new List<GameObject>();
    [HideInInspector] public List<GameObject> listPrioritize1 = new List<GameObject>();
    [HideInInspector] public List<GameObject> listPrioritize2 = new List<GameObject>();
    //[Header("List Chance")]
    public float chane = 10;
    public float chane1 = 0;
    public float chane2 = 0;

    
    public List<Transform> targetPosition;
    public int level = 0;
    void Start()
    {
        level = PlayerPrefs.GetInt("Level");
        Application.targetFrameRate = 60;
    }
    void GetListTargetBall()
    {
        for (int i = 0; i < targetParent.transform.childCount; i++)
        {
            target.Add(targetParent.transform.GetChild(i).gameObject);
        }
    }
    public void GenerateBall( int number)
    {
        for (int i = 0; i < number; i++)
        {
            var clone = Instantiate(ball, targetPosition[i].position, Quaternion.identity);
            clone.name = i.ToString();
            clone.transform.SetParent(targetParent.transform);
        }
        GetListTargetBall();
        
    }
    public void StartGame()
    {
        enemyTower.GetComponent<Tower>().BootStartShot();
        myTower.GetComponent<Tower>().StartAutoShot();
        enemyTower.GetComponent<Tower>().StartHpSmooth();
        myTower.GetComponent<Tower>().StartHpSmooth();
    }
    public void RemoveBall(GameObject ball)
    {
        target.Remove(ball);
        if (ball.GetComponent<Target>().isInListPrioritize1 == true)
        {
            listPrioritize1.Remove(ball);
        }
        if (ball.GetComponent<Target>().isInListPrioritize2 == true)
        {
            listPrioritize2.Remove(ball);
        }
    }
    public void PlayerTapShoot( Vector3 mousepositon)
    {
        myTower.GetComponent<Tower>().TapShot(mousepositon);
    }
    private void Update() // Test nhanh
    {
        if (listPrioritize1.Count > 0 )
        {
            chane1 = 60;
        }
        else
        {
            chane1 = 0;
        }
        
        if (listPrioritize2.Count > 0)
        {
            chane2 = 30;
        }
        else
        {
            chane2 = 0;
        }
        
        
    }

  
}

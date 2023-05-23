using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class RayCastDebug : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject turret;
    public GameObject tempTurret;
    public GameObject tempTower;
    public GameObject tower;
    Vector3 turretStartPosition;
    float getMousePositionxWhenClickOnTurret;
    public LayerMask Turret;
    public LayerMask RayCastFinish;

    public Vector3 aa;
    void Update()
    {
        Vector3 worldPosotion = Input.mousePosition;
        worldPosotion.z = 30f;
        Vector3 mousePose = Camera.main.ScreenToWorldPoint(worldPosotion);
        Debug.DrawRay(transform.position, mousePose - transform.position, Color.blue);
        Gizmos.color = Color.cyan;
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, RayCastFinish))
        {
            //Che do ban chon muc tieu
            //gameManager. PlayerShoot(hit.point);
            var temp = hit.point;
            aa = new Vector3(temp.x, -4.23f, temp.z);
        }
        if (Input.GetMouseButtonDown(0))
        {
            tempTower = tower;
            tempTurret = turret;
            // if (turret == null)
            // {
            //     if (Physics.Raycast(ray, out hit, 100, Turret))
            //     {
            //         //Che do ray 
            //         turret = hit.transform.gameObject;
            //         getMousePositionxWhenClickOnTurret = mousePose.x;
            //         turretStartPosition = turret.transform.position;
            //     }     
            // }
          
        }
        if(tempTurret  != null)
        {
            //var trasnx = turretStartPosition.x - getMousePositionxWhenClickOnTurret;
            tempTurret.transform.position = new Vector3(aa.x * 1.5f, tempTurret.transform.position.y, tempTurret.transform.position.z);
            if (tempTurret.transform.position.x > 4 )
            {
                tempTurret.transform.position = new Vector3(4, tempTurret.transform.position.y, tempTurret.transform.position.z);

            }
            if (tempTurret.transform.position.x <-4)
            {
                tempTurret.transform.position = new Vector3(-4, tempTurret.transform.position.y, tempTurret.transform.position.z);

            }
            
            if (Input.GetMouseButtonUp(0))
            {
                tempTurret = null;
            }
        }
        if(tempTower != null)
        {
            var direction = aa - tempTower.transform.position;
            if (aa.z < tempTower.transform.position.z)
            {
                direction = new Vector3(-direction.x, 0, -direction.z);
            }
            else
            {
                direction = new Vector3(direction.x, 0, direction.z);
            }
            tempTower.transform.forward = direction;
            
            if (Input.GetMouseButtonUp(0))
            {
                tempTower = null;
            }
        }
    }
}

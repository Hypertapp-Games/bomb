using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testspcasr : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject cH;
    public float radius;
    public float mD;
    public LayerMask lm;
    private Vector3 origin;
    private Vector3 direction;

    public float cHD;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        origin = transform.position;
        direction = transform.forward;
        RaycastHit hit;
        if (Physics.SphereCast(origin, radius, direction, out hit, mD, lm, QueryTriggerInteraction.UseGlobal))
        {
            cH = hit.transform.gameObject;
            cHD = hit.distance;
        }
        else
        {
            cHD = mD;
            cH = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(origin, origin+direction*cHD);
        Gizmos.DrawWireSphere(origin+direction*cHD, radius);
    }
}

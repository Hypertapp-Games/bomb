using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simpleswivel : MonoBehaviour
{
    // Start is called before the first frame update
    public float speedx = 0;
    public float speedz = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(speedx,0,speedz,Space.Self);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FitScene : MonoBehaviour
{
    // Start is called before the first frame update
    float _aspect;
    bool _ac = true;
    public float qhdview = 52.0f;
    public float wqhdplusview = 60.0f;

    void Start()
    {
        _aspect = (float)Screen.height / (float)Screen.width;
        var a = Anum();
        float num = (qhdview - a) / (2560.0f / 1440.0f);

        Camera.main.fieldOfView = (_aspect * num) + a;
    }

    // 1.8 , 52  ,,, 2, 57,   2.3 , 60;     1,34375  31/23   30.25/22.25  29.75
    private float Anum()
    {
        //28.72727f;
        float i = 0;
        while (_ac == true)
        {
            i += 0.00001f;
            var num = (wqhdplusview - i) / (qhdview - i);
            if(num >= 1.34375f )
            {
                return i;
                _ac = false;
            }
        }

        return 0;
    }
}

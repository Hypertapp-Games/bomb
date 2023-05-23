using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideObject : MonoBehaviour
{
    // Start is called before the first frame update
    public float activetime = 0.1f;
    void Awake()
    {
    }

    public IEnumerator Hide()
    {
        yield return new WaitForSeconds(activetime);
        gameObject.SetActive(false);
    }

}

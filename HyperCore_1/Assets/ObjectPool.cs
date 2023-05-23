using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public List<GameObject> pooledObjects = new List<GameObject>();
    public Queue<GameObject> wait = new Queue<GameObject>();
    public int amountToPool = 20;
    public GameObject bulletPrefab;

  
    // Start is called before the first frame update

    // Update is called once per frame
    void Start()
    {
        for(int i = 0; i < amountToPool; i++)
        {
            GameObject obj = Instantiate(bulletPrefab);
            obj.SetActive(false);
            obj.transform.parent = gameObject.transform;
            pooledObjects.Add(obj);
        }
    }
    public GameObject GetPooledObject()
    {
        for(int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                if (wait.Count() > 5)
                {
                    var temp = wait.Dequeue();
                    pooledObjects.Add(temp);
                }
                var atemp = pooledObjects[i];
                wait.Enqueue(atemp);
                pooledObjects.Remove(atemp);
                return atemp;
            }
        }
        return null;
    }
}

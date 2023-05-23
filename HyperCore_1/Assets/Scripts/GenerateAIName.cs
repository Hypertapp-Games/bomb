using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateAIName : MonoBehaviour
{
    
    [Serializable]
    private class NamesList
    {
        public List<string> names;
    }

    NamesList namesList;
    NamesList CurrentNamesList
    {
        get
        {
            if (namesList == null)
            {
                TextAsset textAsset = Resources.Load("Texts/NamesList") as TextAsset;
                namesList = JsonUtility.FromJson<NamesList>(textAsset.text);
            }
            return namesList;
        }
    }
    public string GetRandomName()
    {
        return CurrentNamesList.names[UnityEngine.Random.Range(0, CurrentNamesList.names.Count)];
    }
}

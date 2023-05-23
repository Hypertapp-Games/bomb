using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateAIAvatar : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    void Start()
    {
        sprites = Resources.LoadAll<Sprite>("Image");
    }
    public Sprite GetRandomSprite()
    {
        return sprites[UnityEngine.Random.Range(0, sprites.Length - 1)];
    }
}

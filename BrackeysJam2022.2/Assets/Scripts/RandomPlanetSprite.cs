using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPlanetSprite : UnityEngine.MonoBehaviour
{
    [SerializeField] private Sprite[] planetSprites;
    private SpriteRenderer sr;
    public SpriteRenderer minimapSR;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        int sprite = Random.Range(0, planetSprites.Length);
        sr.sprite = planetSprites[sprite];
        minimapSR.sprite = planetSprites[sprite];
    }
}

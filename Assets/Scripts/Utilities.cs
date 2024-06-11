using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities : MonoBehaviour
{
    public static Utilities Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null) { Instance = this; }
    }
    public GameObject EnemyPrefab;
    public SpriteMask HpMask;
    public SpriteMask StanceMask;
    public Transform TextDamage;
    public Transform TextDamageCrit;
    public SpriteRenderer[] PlayerHp;
    public float ElapsedTime;
    public Vector2 GenerateRandomCoordinate(float xMin, float xMax, float yMin, float yMax)
    {
        float xRand = Random.Range(xMin, xMax);
        float yRand = Random.Range(yMin, yMax);
        return new Vector2(xRand, yRand);
    }
    private void Update()
    {
        ElapsedTime += Time.deltaTime;
    }
}

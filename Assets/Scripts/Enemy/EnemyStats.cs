using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(fileName = "New Enemy Stats", menuName = "Enemy/EnemyStats")]
public class EnemyStats : ScriptableObject
{
    public int health;
    public int stance;
    public int maxHealth;
    public int maxStance;
}
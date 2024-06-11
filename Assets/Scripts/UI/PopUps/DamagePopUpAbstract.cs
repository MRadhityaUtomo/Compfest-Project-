using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class DamagePopUp : MonoBehaviour
{
    protected TextMeshPro DamageText;
    void Awake()
    {
        DamageText = GetComponent<TextMeshPro>();
    }
    public void SetUp(int damage)
    {
        DamageText.text = damage.ToString();
    }
}

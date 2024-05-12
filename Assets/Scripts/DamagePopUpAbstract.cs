using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class DamagePopUp : MonoBehaviour
{
    protected TextMeshPro DamageText;
    public IEnumerator DestroyObject(DamagePopUp damagePopUp)
    {
        yield return new WaitForSeconds(1);
        Destroy(damagePopUp.gameObject);
    }
    void Awake()
    {
        DamageText = GetComponent<TextMeshPro>();
    }
    public void SetUp(int damage)
    {
        DamageText.text = damage.ToString();
    }
}

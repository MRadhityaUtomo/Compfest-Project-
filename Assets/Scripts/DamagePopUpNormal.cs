using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DamagePopUpNormal : DamagePopUp
{
    public static DamagePopUpNormal Create(Vector3 position, int damage)
    {
        Transform damagePopUpTransform = Instantiate(Utilities.Instance.TextDamage, position, Quaternion.identity);
        DamagePopUpNormal damagePopUp = damagePopUpTransform.GetComponent<DamagePopUpNormal>();
        damagePopUp.SetUp(damage);
        return damagePopUp;
    }
}

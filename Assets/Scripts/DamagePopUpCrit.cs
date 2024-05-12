using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopUpCrit : DamagePopUp
{
    public static DamagePopUpCrit Create(Vector3 position, int damage)
    {
        Transform damagePopUpTransform = Instantiate(Utilities.Instance.TextDamageCrit, position, Quaternion.identity);
        DamagePopUpCrit damagePopUp = damagePopUpTransform.GetComponent<DamagePopUpCrit>();
        damagePopUp.SetUp(damage);
        return damagePopUp;
    }
}

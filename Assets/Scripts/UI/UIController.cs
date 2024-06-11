using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private SpriteMask _hpMask;
    [SerializeField] private SpriteMask _stanceMask;
    [SerializeField] private EnemyStats _enemyStats;
    void Update()
    {
        _hpMask.alphaCutoff = (float)(_enemyStats.maxHealth - _enemyStats.health) / _enemyStats.maxHealth;
        _stanceMask.alphaCutoff = (float)(_enemyStats.maxStance - _enemyStats.stance) / _enemyStats.maxStance;
    }
    public void ShowPopUp(int damage)
    {
        if (damage == 10)
        {
            DamagePopUpNormal damagePopUp = DamagePopUpNormal.Create(Utilities.Instance.GenerateRandomCoordinate(-2.5f, 2.5f, -0.5f, 2.5f), damage);
            StartCoroutine(DestroyPopUp(damagePopUp));
        }
        else
        {
            DamagePopUpCrit damagePopUp = DamagePopUpCrit.Create(Utilities.Instance.GenerateRandomCoordinate(-2.5f, 2.5f, -0.5f, 2.5f), damage);
            StartCoroutine(DestroyPopUp(damagePopUp));
        }
    }
    private IEnumerator DestroyPopUp(DamagePopUp damagePopUp)
    {
        yield return new WaitForSeconds(1);
        Destroy(damagePopUp.gameObject);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public static EnemyController Instance { get; private set; }
    private int _maxHp = 200;
    private int _hpMeter = 200;
    private int _maxStance = 100;
    private int _stanceMeter = 100;
    private bool _isStunned;
    private bool _canAttack = true;
    private Animator _animator;
    private Coroutine _attackCoroutine;
    private void Awake()
    {
        Instance = this;
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
        Utilities.Instance.HpMask.alphaCutoff = (float)(_maxHp - _hpMeter) / _maxHp;
        Utilities.Instance.StanceMask.alphaCutoff = (float)(_maxStance - _stanceMeter) / _maxStance;
        if (_stanceMeter <= 0 && !_isStunned)
        {
            StartCoroutine(StunEnemy());
        }
        if (_canAttack && !_isStunned)
        {
            _attackCoroutine = StartCoroutine(Attack());
        }
        if (_attackCoroutine != null && _isStunned)
        {
            StopCoroutine(_attackCoroutine);
        }
    }
    public void DecreaseHp(int damage)
    {
        _animator.SetTrigger("damaged");
        if (_isStunned)
        {
            damage = damage * 3/2;
        }
        if (damage == 10)
        {
            DamagePopUpNormal damagePopUp = DamagePopUpNormal.Create(Utilities.Instance.GenerateRandomCoordinate(-2.5f, 2.5f, -0.5f, 2.5f), damage);
            damagePopUp.StartCoroutine(damagePopUp.DestroyObject(damagePopUp));
        }
        else
        {
            DamagePopUpCrit damagePopUp = DamagePopUpCrit.Create(Utilities.Instance.GenerateRandomCoordinate(-2.5f, 2.5f, -0.5f, 2.5f), damage);
            damagePopUp.StartCoroutine(damagePopUp.DestroyObject(damagePopUp));
        }

        _hpMeter = Math.Max(0, _hpMeter - damage);
        _animator.SetInteger("hp", _hpMeter);
    }
    public void DecreaseStance(int damage)
    {
        _stanceMeter = Mathf.Max(0, _stanceMeter - damage);
    }
    private IEnumerator StunEnemy()
    {
        _animator.fireEvents = false;
        _animator.SetBool("isStunned", true);
        _isStunned = true;
        yield return new WaitForSeconds(5);
        _isStunned = false;
        _canAttack = true;
        _stanceMeter = _maxStance;
        _animator.SetBool("isStunned", false);
    }
    private IEnumerator Attack()
    {
        _canAttack = false;
        yield return new WaitForSeconds(1.5f);
        _animator.SetTrigger("attack");
        float attackCooldown = UnityEngine.Random.Range(1f, 3f);
        yield return new WaitForSeconds(attackCooldown);
        _canAttack = true;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(69);
    }
}

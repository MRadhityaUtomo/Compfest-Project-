using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{
    public static EnemyController Instance { get; private set; }
    public UnityEvent EnemyAttacks;
    public UnityEvent<int> EnemyAttacked;
    [SerializeField] private EnemyStats _enemyStats;
    private float _speedUp = 1f;
    private bool _isStunned;
    private bool _canAttack = true;
    private Animator _animator;
    private Coroutine _attackCoroutine;
    private void Awake()
    {
        Instance = this;
        _enemyStats.stance = _enemyStats.maxStance;
        _enemyStats.health = _enemyStats.maxHealth;
        _animator = GetComponent<Animator>();
    }
    private void OnDestroy()
    {
        Instance = null;
    }
    private void Update()
    {
        _animator.speed = _speedUp;
        _speedUp = Mathf.Min(Utilities.Instance.ElapsedTime * 0.01f, 2);
        _speedUp = Mathf.Max(_speedUp, 1);
        if (_enemyStats.health <= 0)
        {
            StopCoroutine(_attackCoroutine);
            StartCoroutine(DestroyEnemy());
        }
        if (_enemyStats.stance <= 0 && !_isStunned)
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
        EnemyAttacked.Invoke(damage);

        _enemyStats.health = Math.Max(0, _enemyStats.health - damage);
        _animator.SetInteger("hp", _enemyStats.health);
    }
    public void DecreaseStance(int damage)
    {
        _enemyStats.stance = Mathf.Max(0, _enemyStats.stance - damage);
    }
    private IEnumerator StunEnemy()
    {
        _animator.fireEvents = false;
        _animator.SetBool("isStunned", true);
        _isStunned = true;
        yield return new WaitForSeconds(5);
        _isStunned = false;
        _canAttack = true;
        _enemyStats.stance = _enemyStats.maxStance;
        _animator.SetBool("isStunned", false);
    }
    private IEnumerator Attack()
    {
        _canAttack = false;
        float attackCooldown = UnityEngine.Random.Range(1f, 3f);
        yield return new WaitForSeconds(attackCooldown);
        _animator.SetTrigger("attack");
        yield return new WaitForSeconds(1f/_speedUp);
        EnemyAttacks.Invoke();
        attackCooldown = UnityEngine.Random.Range(1f, 3f);
        yield return new WaitForSeconds(attackCooldown);
        _canAttack = true;

    }
    private IEnumerator DestroyEnemy()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}

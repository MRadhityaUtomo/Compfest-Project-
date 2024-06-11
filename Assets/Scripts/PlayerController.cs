using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }
    [SerializeField] private Animator _animator;
    private bool _canAttack = true;
    private bool _canParry = true;
    private int _hp = 3;
    public bool IsParrying;
    private Coroutine _parryCoroutine;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        UpdateHp();
        if (_canAttack && Input.GetMouseButtonDown(0))
        {
            StartCoroutine(HandleAttack());
        }

        if (_canParry && Input.GetMouseButtonDown(1))
        {
            Debug.Log("Parrying");
            _animator.SetTrigger("parry");
            StartCoroutine(HandleParry());
        }
    }

    private void UpdateHp()
    {
        for (int i = 0; i < _hp; i++)
        {
            Utilities.Instance.PlayerHp[i].color = Color.white;
        }
        for (int i = _hp; i < 3; i++)
        {
            Utilities.Instance.PlayerHp[i].color = Color.black;
        }
    }
    private IEnumerator HandleAttack()
    {
        _canAttack = false;
        _animator.SetTrigger("click");
        EnemyController.Instance.DecreaseHp(10);
        EnemyController.Instance.DecreaseStance(5);
        yield return CooldownAttack(0.5f);
    }

    private IEnumerator HandleParry()
    {
        _canParry = false;


        if (_parryCoroutine != null)
        {
            StopCoroutine(_parryCoroutine);
        }
        _parryCoroutine = StartCoroutine(Parry());
        yield return null;
    }

    private IEnumerator CooldownAttack(float time)
    {
        _canParry = false;
        yield return new WaitForSeconds(time);
        _canAttack = true;
        _canParry = true;
    }

    private IEnumerator Parry()
    {
        _canParry = false;
        _canAttack = false;
        yield return new WaitForSeconds(0.1f);
        IsParrying = true;
        yield return new WaitForSeconds(0.9f);
        IsParrying = false;
        yield return new WaitForSeconds(0.5f);
        _canAttack = true;
        _canParry = true;
    }
    public void HandleEnemyAttack()
    {
        _animator.gameObject.SetActive(true);
        Debug.Log(_animator.isActiveAndEnabled);
        _animator.SetTrigger("attacked");
        if (IsParrying)
        {
            Debug.Log("Parry Berhasil!");
            _canAttack = true;
            _canParry = true;
            IsParrying = false;
            EnemyController.Instance.DecreaseStance(20);
            StopCoroutine(_parryCoroutine);
        }
        else
        {
            Debug.Log("Noob");
            _hp = Math.Max(0, _hp - 1);
            _animator.ResetTrigger("attacked");
        }
    }
    private void HealPlayer()
    {
        _hp = Math.Min(3, _hp + 1);
    }
}

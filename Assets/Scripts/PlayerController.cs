using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }
    private Animator _swordAnim;
    private bool _canAttack = true;
    private bool _canParry = true;
    public bool IsParrying;
    private Coroutine _parryCoroutine;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        _swordAnim = GetComponent<Animator>();
    }

    private void Update()
    {
        _swordAnim.SetBool("canAttack", _canAttack && _canParry);
        if (_canAttack && Input.GetMouseButtonDown(0))
        {
            _swordAnim.SetTrigger("click");
            EnemyController.Instance.DecreaseHp(10);
            EnemyController.Instance.DecreaseStance(5);
            StartCoroutine(CooldownAttack(0.5f));
        }
        if (_canParry && Input.GetMouseButtonDown(1))
        {
            _swordAnim.SetTrigger("parry");
            if (_parryCoroutine != null)
            {
                StopCoroutine(_parryCoroutine);
            }
            _parryCoroutine = StartCoroutine(Parry());
        }
    }
    private IEnumerator CooldownAttack(float time)
    {
        _canParry = false;
        _canAttack = false;
        yield return new WaitForSeconds(time);
        _canAttack = true;
        _canParry = true;
    }
    private IEnumerator Parry()
    {
        _canParry = false;
        _canAttack = false;
        IsParrying = true;
        yield return new WaitForSeconds(0.75f);
        IsParrying = false;
        yield return new WaitForSeconds(0.75f);
        _canAttack = true;
        _canParry = true;
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (IsParrying) {
            Debug.Log("Parry Berhasil!");
            _canAttack = true;
            _canParry = true;
            EnemyController.Instance.DecreaseStance(20);
            StopCoroutine(_parryCoroutine);
        }
        else {
            Debug.Log("Noob");
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour
{
    private Vector2 moveDirection;
    private Rigidbody _rigidbody;
    private MoveSpeedStat _moveSpeedStat;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        enabled = false;
    }

    public void Setup(MoveSpeedStat moveSpeedStat)
    {
        this._moveSpeedStat = moveSpeedStat;
        enabled = true;
    }

    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction;
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = moveDirection * (float)_moveSpeedStat;
    }
}
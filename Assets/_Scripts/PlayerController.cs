using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(StatContainer))]
[RequireComponent(typeof(CharacterMovement))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private List<StatModifier> baseStats;


    private StatContainer statContainer;
    private CharacterMovement _characterMovement;
    private PlayerInputActions _playerInputActions;

    private Vector2 moveDirection;

    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        statContainer = GetComponent<StatContainer>();
        _characterMovement = GetComponent<CharacterMovement>();
    }

    public void Setup()
    {
        statContainer.Setup();
        statContainer.AddModifier(this,baseStats.ToArray());
        
        _characterMovement.Setup(statContainer.GetStat<MoveSpeedStat>());
    }


    private void Update()
    {
        _characterMovement.SetDirection(moveDirection);
    }

    private void OnMovementInputPerformed(InputAction.CallbackContext value)
    {
        moveDirection = value.ReadValue<Vector2>();
    }

    private void OnMovementInputCanceled(InputAction.CallbackContext value)
    {
        moveDirection = Vector2.zero;
    }


    private void OnEnable()
    {
        _playerInputActions.Enable();
        _playerInputActions.Player.Movement.performed += OnMovementInputPerformed;
        _playerInputActions.Player.Movement.canceled += OnMovementInputCanceled;
    }


    private void OnDisable()
    {
        _playerInputActions.Disable();
        _playerInputActions.Player.Movement.performed -= OnMovementInputPerformed;
        _playerInputActions.Player.Movement.canceled -= OnMovementInputCanceled;
    }
}
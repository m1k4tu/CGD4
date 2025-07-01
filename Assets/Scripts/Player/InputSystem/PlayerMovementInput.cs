using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerControls;

public class PlayerMovementInput : MonoBehaviour, PlayerControls.IPlayerMovementMapActions
{
    public PlayerControls PlayerControls {  get; private set; }
    public Vector2 MovementInput {  get; private set; }
    public Vector2 LookInput { get; private set; }
    public bool UI_Input { get; private set; }


    public void OnEnable()
    {
        PlayerControls = new PlayerControls();
        PlayerControls.Enable();

        PlayerControls.PlayerMovementMap.Enable();
        PlayerControls.PlayerMovementMap.SetCallbacks(this);
    }

    public void OnDisable()
    {
        PlayerControls.PlayerMovementMap.Disable();
        PlayerControls.PlayerMovementMap.RemoveCallbacks(this);
    }
    public void OnMovement(InputAction.CallbackContext context)
    {
        MovementInput = context.ReadValue<Vector2>();
        print(MovementInput);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        LookInput = context.ReadValue<Vector2>();
    }

    public void OnToggleUI(InputAction.CallbackContext context)
    {
        UI_Input = context.ReadValue<bool>();
    }
}

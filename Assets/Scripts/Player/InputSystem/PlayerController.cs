using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    [Header ("Components")]
    [SerializeField] private CharacterController m_CharacterController;
    [SerializeField] private Camera m_playerCamera;
    
    [Header("Player Base Movement")]
    public float runAcceleration = 0.25f;
    public float runSpeed = 5.0f;
    public float drag = 0.1f;

    [Header("Camera")]

    public float lookSensitivityH = 0.5f;
    public float lookSensitivityV = 0.5f;
    public float lookLimitV = 60f;
    public Vector2 CamRotation = Vector2.zero;
    public Vector2 PlayerTargetRotation = Vector2.zero;

    private PlayerMovementInput m_MovementInput;
    
    public void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;

        m_MovementInput = GetComponent<PlayerMovementInput>();
    }

    public void Update()
    {
        //Two Vector3s for the direction the camera is facing, projected into the X|Z - Plane 
        Vector3 CamForwardXZ    = new Vector3(m_playerCamera.transform.forward.x, 0f, m_playerCamera.transform.forward.z).normalized;
        Vector3 CamRightXZ      = new Vector3(m_playerCamera.transform.right.x, 0f, m_playerCamera.transform.right.z).normalized;
        Vector3 moveDirection   = CamRightXZ * m_MovementInput.MovementInput.x + CamForwardXZ * m_MovementInput.MovementInput.y; //-> this must be y bc the movementInput is a vector2!
       
        Vector3 moveDelta       = moveDirection * runAcceleration * Time.deltaTime;
        Vector3 newVelocity     = m_CharacterController.velocity + moveDelta;

        //This adds drag to the player (stops him from sliding endlessly)
        Vector3 currentDrag     = drag * newVelocity.normalized * Time.deltaTime;
        //If the new Velocity is greater than the drag, subtract drag from it.
        newVelocity             = (newVelocity.magnitude > drag * Time.deltaTime) ? newVelocity - currentDrag : Vector3.zero;
        newVelocity = Vector3.ClampMagnitude(newVelocity, runSpeed);
        //This method actually moves the player
        MoveServerRpc(newVelocity);
        //m_CharacterController.Move(newVelocity * Time.deltaTime);
    }
    public void LateUpdate()
    {
        //Calculating Camera's rotation:
        CamRotation.x += lookSensitivityH * m_MovementInput.LookInput.x;
        CamRotation.y = Mathf.Clamp(CamRotation.y - lookSensitivityV * m_MovementInput.LookInput.y, -lookLimitV, lookLimitV);

        //Calculating Player's rotation:
        PlayerTargetRotation.x += transform.eulerAngles.x + lookSensitivityH * m_MovementInput.LookInput.x;
        transform.rotation = Quaternion.Euler(0f, PlayerTargetRotation.x, 0f);

        m_playerCamera.transform.rotation = Quaternion.Euler(CamRotation.y, CamRotation.x, 0f); 
    }

    [ServerRpc]
    public void MoveServerRpc(Vector3 newVelocity)
    {
        m_CharacterController.Move(newVelocity * Time.deltaTime);
        MoveClientRpc(newVelocity);
    }

    [ClientRpc]
    public void MoveClientRpc(Vector3 newVelocity)
    {
        m_CharacterController.Move(newVelocity * Time.deltaTime);
    }

}

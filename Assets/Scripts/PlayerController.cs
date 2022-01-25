using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    //
    Controlls controlls;
    CharacterController characterController;
    [SerializeField]Camera cam;
    [SerializeField]GameObject charModel;

    Vector2 movingInput;
    Vector3 movingDirection;

    Vector2 lookDirectionInput;
    Vector3 mouseWorldPosition;
    Vector3 direction;
    Quaternion lookDirection;

    //Camera control
    Vector3 cameraDirection;
    Quaternion cameraLookDirection;
    Vector3 cameraOffsetPosition;

    bool isAttacking;

    [SerializeField]float cameraHeight;
    [SerializeField]float cameraOffset;

    private void Awake()
    {
        controlls = new Controlls();
        //Geting character controller
        characterController = GetComponent<CharacterController>();
        //Subscribe on moving
        controlls.Character.Moving.performed += onMoving;
        controlls.Character.Moving.canceled += onMoving;
        //Subscribe on looking at pos
        controlls.Character.LookAtPos.performed += onLooking;
        //Subscribe on rotation
        controlls.Character.Rotation.performed += onRotation;
        //Subscribe on attack
        controlls.Character.Attack.performed += onAttack;
        controlls.Character.Attack.canceled += onAttack;

        cameraOffsetPosition = new Vector3(0f, cameraHeight, cameraOffset);
    }

    void onMoving(InputAction.CallbackContext context)
    {
        movingInput = context.ReadValue<Vector2>();
    }

    void onLooking(InputAction.CallbackContext context)
    {
        Ray ray = cam.ScreenPointToRay(context.ReadValue<Vector2>());
        if (Physics.Raycast(ray, out RaycastHit raycast))
        {
            mouseWorldPosition = raycast.point;

            direction = (charModel.transform.position - mouseWorldPosition).normalized;
            direction.y = 0;
            lookDirection = Quaternion.LookRotation(direction);
        }
    }

    void onRotation(InputAction.CallbackContext context)
    {
        lookDirectionInput = context.ReadValue<Vector2>();

        float angle = Mathf.Atan2(-lookDirectionInput.x, -lookDirectionInput.y) * Mathf.Rad2Deg;
        lookDirection = Quaternion.Euler(0f, angle, 0f);

    }

    void onAttack(InputAction.CallbackContext context)
    {
        isAttacking = context.ReadValueAsButton();
    }

    private void Update()
    {

        movingDirection.x = movingInput.x;
        movingDirection.z = movingInput.y;

        if (characterController.isGrounded)
        {
            movingDirection.y = -0.01f;
        }
        else
        {
            movingDirection.y = -9.8f;
        }

        if (isAttacking)
        {
            //Debug.Log("Ratatatat!");
        }

        characterController.Move(10f * Time.deltaTime * movingDirection);


        charModel.transform.rotation = Quaternion.Slerp(charModel.transform.rotation, lookDirection, 10f * Time.deltaTime);

        CameraMovement();

    }

    void CameraMovement()
    {
        cameraDirection = (charModel.transform.position - cam.transform.position).normalized;
        cameraLookDirection = Quaternion.LookRotation(cameraDirection);
        cam.transform.rotation = Quaternion.Slerp(cam.transform.rotation, cameraLookDirection, 10f * Time.deltaTime);

        cam.transform.position = Vector3.Slerp(cam.transform.position, charModel.transform.position + cameraOffsetPosition, 15f * Time.deltaTime);

    }

    private void OnEnable()
    {
        controlls.Character.Moving.Enable();
        controlls.Character.Rotation.Enable();
        controlls.Character.LookAtPos.Enable();
        controlls.Character.Attack.Enable();
    }
    private void OnDisable()
    {
        controlls.Character.Moving.Disable();
        controlls.Character.Rotation.Disable();
        controlls.Character.LookAtPos.Disable();
        controlls.Character.Attack.Disable();
    }
}

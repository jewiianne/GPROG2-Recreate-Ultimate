using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public Camera player;

    public float moveSpeed = 5f;
    public float rotationSpeed = 700f;

    private CharacterController characterController;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0, vertical);
        movement.Normalize();

        // Move the player using CharacterController
        characterController.Move(movement * moveSpeed * Time.deltaTime);

        if (movement != Vector3.zero)
        {
            animator.SetBool("run", true);
            animator.SetBool("Idle", false);

            // Rotate towards the movement direction
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("run", false);
            animator.SetBool("Idle", true);
        }
    }
}

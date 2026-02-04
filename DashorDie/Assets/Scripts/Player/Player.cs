using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float sprintSpeed = 9f;
    private float currentSpeed;

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    PlayerController controller;
    [SerializeField] private TrailRenderer trailRenderer;

    [Header("Inputs")]
    [SerializeField] Vector2 moveInput;
    [SerializeField] bool isSprinting;
   
    [Header("DashSettings")]
    [SerializeField] private float dashSpeed = 7f;
    [SerializeField] private float dashDuration = 0.05f;
    [SerializeField] private float dashCooldown = 3f;
    [SerializeField] bool dashPressed;
    [SerializeField] bool isDashing = false;
    [SerializeField] bool canDash = true;

    void Awake()
    {
        controller = new PlayerController();
        Move();
        Dash();
        Sprint();
        currentSpeed = speed;
    }

    void Move()
    {
        controller.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controller.Player.Move.canceled += ctx => moveInput = Vector2.zero;
    }

    void Dash()
    {
        controller.Player.Dash.performed += ctx => dashPressed = true;
        {
            Debug.Log("Dash pressed");
        }
    }

    void Sprint()
    {
        controller.Player.Sprint.performed += ctx => isSprinting = true;
        controller.Player.Sprint.canceled += ctx => isSprinting = false;
    }

    private void OnEnable()
    {
        controller.Player.Enable();
    }

    private void OnDisable()
    {
        controller.Player.Disable();
    }

    void FixedUpdate()
    {
        if(isDashing)
        {
            return;
        }

        Movement();

        if(canDash == true && dashPressed == true)
        {
            dashPressed = false;
            StartCoroutine(Dashing());
            Debug.Log("Dashing");
        }
    }

    private void Movement()
    {
        if(isSprinting)
        {
            currentSpeed = sprintSpeed;
        }
        else
        {
            currentSpeed = speed;
        }

            Vector2 move = (rb.position + moveInput * speed * Time.deltaTime);
        rb.MovePosition(move);
    }

    IEnumerator Dashing()
    {
        isDashing = true;
        canDash = false;
        rb.AddForce(moveInput * dashSpeed, ForceMode2D.Impulse);
        trailRenderer.emitting = true;
        yield return new WaitForSeconds(dashDuration);
        rb.linearVelocity = Vector2.zero;
        trailRenderer.emitting = false;
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
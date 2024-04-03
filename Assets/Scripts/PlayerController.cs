using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour

{
    public float sprintSpeedMultiplier = 2f; // Multiplicador de velocidad para el sprint
    private bool isSprinting; // Variable para rastrear si el jugador está sprintando
    public float speed;
    public float rotationSpeed;
    public float jumpSpeed;

    public float jumpHorizontalSpeed;
    public float jumpButtonGracePeriod;

    public Transform cameraTransform;

    private Animator animator;
    public CharacterController characterController;
    private float ySpeed;
    private float originalStepOffset;
    private float? lastGroundedTime;
    private float? jumpButtonPressedTime;

    private bool isJumping;
    private bool isGrounded;
    public float doubleJumpSpeed; // Velocidad del doble salto
    private bool doubleJumped;
  

    private void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        originalStepOffset = characterController.stepOffset;
        SwordScript.OnAtaque += Attack;
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude) * speed;

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            isSprinting = true;
            animator.SetBool("Running", true);
            //animacion de correr 
        }
        else
        {
            isSprinting = false;
            animator.SetBool("Running", false);
        }

     

        // Multiplicamos la magnitud de entrada por el multiplicador de sprint si el jugador está sprintando
        if (isSprinting)
        {
            inputMagnitude *= sprintSpeedMultiplier;
        }

    

        animator.SetFloat("InputMagnitude", inputMagnitude, 0.05f, Time.deltaTime);

        // Resto de tu código aquí


        animator.SetFloat("InputMagnitude", inputMagnitude, 0.05f, Time.deltaTime);
        movementDirection = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) * movementDirection;
        movementDirection.Normalize();

      



        ySpeed += Physics.gravity.y * Time.deltaTime;

        if (characterController.isGrounded)
        {
            lastGroundedTime = Time.time;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded || (Time.time - lastGroundedTime <= jumpButtonGracePeriod))
            {
                // Si estamos en el suelo o hemos presionado el botón de salto dentro del periodo de gracia
                // Realizamos un salto normal
                Jump();
            }
            else if (!doubleJumped) // Si no estamos en el suelo y no hemos realizado un doble salto aún
            {
                // Realizamos el doble salto
                DoubleJump();
            }
        }
        if (Time.time - lastGroundedTime <= jumpButtonGracePeriod)
        {
            characterController.stepOffset = originalStepOffset;
            ySpeed = -0.5f;
            animator.SetBool("IsGrounded", true);
            isGrounded = true;
            animator.SetBool("IsJumping", false);
            isJumping = false;
            animator.SetBool("IsFalling", false);
            animator.SetBool("DobleJump", false);
            doubleJumped = false;
            if (Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod)
            {
                ySpeed = jumpSpeed;
                animator.SetBool("IsJumping", true);
                isJumping = true;
                doubleJumped = false;
                jumpButtonPressedTime = null;
                lastGroundedTime = null;
            }
        }
        else
        {
            characterController.stepOffset = 0;
            animator.SetBool("IsGrounded", false);
            isGrounded = false;
            if ((isJumping && ySpeed < 0) || ySpeed < -2)
            {
                animator.SetBool("IsFalling", true);
            }
        }
        Vector3 velocity = movementDirection * inputMagnitude;
        velocity.y = ySpeed;

        characterController.Move(velocity * Time.deltaTime);


        if (movementDirection != Vector3.zero)
        {
            animator.SetBool("isRunning", true);
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
        if (isGrounded == false)
        {

            velocity.y = ySpeed;

            characterController.Move(velocity * Time.deltaTime);
        }
        if (isGrounded)
        {
            doubleJumped = false;
        }
     
        if (Input.GetKey(KeyCode.Mouse1))
        {

            // Activar la animación de ataque
            animator.SetBool("Linterna", true);
        }


        else
        {

            animator.SetBool("Linterna", false);
        }


    }

    private void OnAnimatorMove()
    {
        if (isGrounded)
        {
            Vector3 velocity = animator.deltaPosition;
            velocity.y = ySpeed * Time.deltaTime;
            characterController.Move(velocity);
        }
    }
    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {

        }
    }
    private void Jump()
    {
        ySpeed = jumpSpeed;
        animator.SetBool("IsJumping", true);
        isJumping = true;
        jumpButtonPressedTime = null;
        lastGroundedTime = null;
    }
    private void DoubleJump()
    {
        ySpeed = doubleJumpSpeed; // Utilizamos la velocidad de doble salto
        animator.SetBool("DobleJump", true);
        isJumping = false;
        doubleJumped = true; // Marcamos que se ha realizado un doble salto
    }
    void Attack()
    {
        // Activar la animación de ataque
        animator.SetBool("IsAttacking", true);

        // Aquí puedes agregar código adicional para manejar la lógica del ataque, como daño a enemigos, efectos visuales, etc.
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trampolin")) // Verifica si el objeto que colisiona es el jugador
        {
            // Ejecuta la animación de salto del jugador
            ySpeed = jumpSpeed;
            animator.SetBool("IsJumping", true);
            isJumping = true;
            jumpButtonPressedTime = null;
            lastGroundedTime = null;

        }
    }
}





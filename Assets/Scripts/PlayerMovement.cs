using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public AudioSource footsteps;
    public AudioSource running;
    float defaultVolume = 0.75f;

    public float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;

    public float playerHeight;

    public float crouchSpeed;
    public float crouchYScale;
    private float startYScale;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;

    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;
    private bool readyToJump;
    private bool exitingSlope;

    public LayerMask ground;
    public float groundDrag;
    private bool grounded;
    public bool onStairs;
    public LayerMask stairs;

    public float maxSlopeAngle;
    private RaycastHit slopeHit;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;
    private Vector2 walkInput;

    Vector3 moveDirection;
    Vector3 velocity;
    Rigidbody rb;

    public GameObject capsule;
    public GameObject weaponHolder;

    public bool allowMovement;
    private bool tired;
    public float stamina = 5;
    public float maxStamina = 5;
    public float minStamina = 0;
    public bool sprint = false;
    public bool rest = false;
    public bool stopInput = false;

    public MovementState state;

    public enum MovementState
    {
        walking,
        sprinting,
        air,
        crouching
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        AudioListener.volume = 1;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
        startYScale = capsule.transform.localScale.y;
        allowMovement = true;
        tired = false;
        stamina = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (allowMovement)
        {
            grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, ground);
            onStairs = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, stairs);
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");
            moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
            if (grounded && velocity.y < 0f)
            { velocity.y = -2f; }
            //MovePlayer();

            MyInput();
            StateHandler();
            //SpeedControl();
            PlaySound();
            BobOffset();
            BobRotation();


            controller.Move(velocity * Time.deltaTime);
            velocity.y += -9.81f * Time.deltaTime;
            controller.Move(moveDirection.normalized * moveSpeed * Time.deltaTime);
            if (grounded)
                rb.drag = groundDrag;
            else
                rb.drag = 1;



            if (state == MovementState.sprinting && rb.velocity.magnitude > 5)
            {
                if (stamina > minStamina)
                {
                    stamina -= Time.deltaTime;

                    if (stamina <= 0) tired = true;
                }

            }
            else
            {
                if (stamina < maxStamina)
                {
                    stamina += Time.deltaTime * 2f;

                    if (stamina >= maxStamina) tired = false;
                }
            }

        }
    }

    private void MyInput()
    {

        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
        }
        if ((Input.GetKeyUp(jumpKey) || !Input.GetKey(jumpKey)) && grounded)
        {

            Invoke(nameof(ResetJump), jumpCooldown);
        }
        if (Input.GetKeyDown(crouchKey))
        {
            capsule.transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }
        if (Input.GetKeyUp(crouchKey))
        {
            capsule.transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        }
    }
    private void StateHandler()
    {

        if (grounded && Input.GetKey(crouchKey))
        {
            state = MovementState.crouching;
            moveSpeed = crouchSpeed;
        }
        else if ((grounded || onStairs) && Input.GetKey(sprintKey) && tired == false)
        {
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;

        }
        else if (grounded || onStairs)
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }
        else
        {
            state = MovementState.air;
        }
    }

    private void MovePlayer()
    {

        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        //controller.Move(moveDirection);
        /*if (OnSlope() || onStairs)
        {
            rb.AddForce(GetSlopeMoveDirection() * moveSpeed * 20f, ForceMode.Force);
            if (rb.velocity.y > 0)
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }
        else */
        if (grounded)
            // rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
            controller.Move(moveDirection.normalized * moveSpeed * Time.deltaTime);
        //else if (!grounded)
        //rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        // rb.useGravity = !OnSlope();
    }
    private void SpeedControl()
    {
        if ((OnSlope() && !exitingSlope) || onStairs)
        {
            if (rb.velocity.magnitude > moveSpeed)
                rb.velocity = rb.velocity.normalized * moveSpeed;
        }
        else
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }
        if (!readyToJump)
        {
            Vector3 flatVel = new Vector3(0, rb.velocity.y, 0);
            if (flatVel.magnitude > jumpForce)
            {
                Vector3 limitedVel = flatVel.normalized * jumpForce;
                rb.velocity = new Vector3(rb.velocity.x, limitedVel.y, rb.velocity.z);
            }
        }

    }
    private void Jump()
    {
        exitingSlope = true;
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
        exitingSlope = false;
    }
    private bool OnSlope()
    {
        if (Physics.Raycast(orientation.transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }
        return false;
    }
    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }
    private void PlaySound()
    {
        if (((horizontalInput != 0 || verticalInput != 0) && rb.velocity.magnitude > 0) && (state == MovementState.walking))
        {
            StartCoroutine(Transition(running, footsteps));


        }
        else if (state == MovementState.sprinting && rb.velocity.magnitude > 5)
        {
            StartCoroutine(Transition(footsteps, running));

        }
        else
        {
            StartCoroutine(Quiet(footsteps, running));
        }

    }
    IEnumerator Transition(AudioSource nowPlaying, AudioSource target)
    {

        float percentage = 0;
        float transitionTime = 0.25f;
        while (nowPlaying.volume > 0)
        {
            nowPlaying.volume = Mathf.Lerp(defaultVolume, 0, percentage);
            percentage += Time.deltaTime / transitionTime;
            yield return null;
        }
        nowPlaying.Pause();
        if (target.isPlaying == false)
            target.Play();
        target.UnPause();
        percentage = 0;
        while (target.volume < defaultVolume)
        {
            target.volume = Mathf.Lerp(0, defaultVolume, percentage);
            percentage += Time.deltaTime / transitionTime;
            yield return null;
        }



    }
    public IEnumerator Quiet(AudioSource A1, AudioSource A2)
    {
        float defaultVolume = 1;
        float percentage = 0;
        float transitionTime = 0.25f;

        while (A1.volume > 0)
        {
            A1.volume = Mathf.Lerp(defaultVolume, 0, percentage);
            percentage += Time.deltaTime / transitionTime;
            yield return null;
        }
        while (A2.volume > 0)
        {
            A2.volume = Mathf.Lerp(defaultVolume, 0, percentage);
            percentage += Time.deltaTime / transitionTime;
            yield return null;
        }
        A1.volume = 0;
        A2.volume = 0;
        A1.Pause();
        A2.Pause();
    }
    [Header("Bobbing")]
    public float speedCurve;
    float curveSin { get => Mathf.Sin(speedCurve); }
    float curveCos { get => Mathf.Cos(speedCurve); }
    public Vector3 travelLimit = Vector3.one * 0.025f;
    public Vector3 bobLimit = Vector3.one * 0.01f;
    public Vector3 bobPosition;

    void BobOffset()
    {
        speedCurve += Time.deltaTime * (grounded ? rb.velocity.magnitude : 1f) + 0.01f;
        if (speedCurve > 360) speedCurve = 0;
        bobPosition.x = (curveCos * bobLimit.x * (grounded ? 1 : 0)) - (walkInput.x * travelLimit.x);
        bobPosition.y = (curveSin * bobLimit.y) - (rb.velocity.y * travelLimit.y);
        bobPosition.z = -(walkInput.y * travelLimit.z);
    }
    public Vector3 multiplier;
    public Vector3 bobEulerRotation;

    void BobRotation()
    {
        bobEulerRotation.x = (walkInput != Vector2.zero ? multiplier.x * (Mathf.Sin(2 * speedCurve)) : multiplier.x * (Mathf.Sin(2 * speedCurve) / 2));
        bobEulerRotation.y = (walkInput != Vector2.zero ? multiplier.y * curveCos : 0);
        bobEulerRotation.z = (walkInput != Vector2.zero ? multiplier.z * curveCos * walkInput.x : 0);
    }
}


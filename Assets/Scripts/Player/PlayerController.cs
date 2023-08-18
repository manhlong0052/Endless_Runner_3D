using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Vector3 direction;
    [SerializeField] public float forwardSpeed;
    [SerializeField] public float maxSpeed = 25f;


    [SerializeField] protected bool isGround;
    [SerializeField] protected float groundDistance = 0.4f;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected LayerMask groundLayer;
    [SerializeField] protected bool isRoll = false;


    [SerializeField] protected float gravity = -20f;
    [SerializeField] public float jumpForce = 5f; 

    [SerializeField] private int desireLane = 1;
    [SerializeField] private float laneDistance = 3f;

    [SerializeField] public Animator animator;

    private void Awake()
    {
        this.characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (!PlayerManager.isGamestarted) return;
        this.moveToSide();

    }


    private void FixedUpdate()
    {
        if (!PlayerManager.isGamestarted) return;
        this.moving();

    }

    protected virtual void moving()
    {
        if (forwardSpeed > maxSpeed) forwardSpeed = maxSpeed;
        forwardSpeed += 0.5f * Time.deltaTime;

        this.direction.z = this.forwardSpeed;
        this.characterController.Move(this.direction * Time.fixedDeltaTime); 
    }

    protected virtual void moveToSide()
    {

        animator.SetBool("isGameStarted", true);

        isGround = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);

        animator.SetBool("isGrounded", isGround);

        if (isGround && direction.y < 0) { 
            direction.y = -2f;
            if (SwipeManager.swipeUp)
            {
                jump();
            }
        }
        else
        {
            direction.y += gravity * Time.deltaTime;
        }

        if (SwipeManager.swipeDown && !isRoll)
        {
            StartCoroutine(Roll());
        }

        if (SwipeManager.swipeRight) {
            this.desireLane++;
            if (this.desireLane == 3) this.desireLane = 2;
        }

        if (SwipeManager.swipeLeft)
        {
            this.desireLane--;
            if (this.desireLane == -1) this.desireLane = 0;
        }

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (this.desireLane == 0)
        {
            targetPosition += Vector3.left * laneDistance;
        } else if (this.desireLane == 2) {
            targetPosition += Vector3.right * laneDistance;
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, 10 * Time.deltaTime);
        characterController.center = characterController.center;
    }

    private IEnumerator Roll()
    {
        isRoll = true;
        animator.SetBool("isRoll", true);
        characterController.center = new Vector3(0, -0.5f, 0);
        characterController.height = 1;

        yield return new WaitForSeconds(0.8f);

        characterController.center = new Vector3(0, 0f, 0);
        characterController.height = 2;
        animator.SetBool("isRoll", false);
        isRoll = false;
    }

    private void jump()
    {
        direction.y = jumpForce;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Obstacle")
        {
            AudioManger.instance.playSound("gameOver");
            PlayerManager.gameOver = true;
        }
    }
}

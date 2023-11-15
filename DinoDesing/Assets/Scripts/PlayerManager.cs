using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    
    public List<AnimatorController> animatorControllers;
    private Rigidbody2D playerRb;
    private Animator animator;

    public LayerMask groundLayer;
   
    public float speed, jumpForce, groundRadiusCircle, maxDistanceCircle;
    private float input;
    private bool canJump = true;

    private int HCMove = Animator.StringToHash("Move");
    private int HCGrounded = Animator.StringToHash("IsGrounded");

    private int HCHurt = Animator.StringToHash("Hurt");
    private int HCAttack = Animator.StringToHash("Attack");
    private int HCDead = Animator.StringToHash("Dead");

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody2D>();
        animator.runtimeAnimatorController = animatorControllers[0];
    }

    void Update()
    {
        input = Input.GetAxisRaw("Horizontal");

        if (input < 0)
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        else if (input > 0) 
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);

        if (canJump && Input.GetButton("Jump") && IsGrounded())
        {
            canJump = false;
            playerRb.velocity = Vector2.up * jumpForce;
        }

        if (Input.GetButtonUp("Jump"))
            canJump = true;


        animator.SetBool(HCMove, input != 0);
        animator.SetBool(HCGrounded, IsGrounded());

    }

    private bool IsGrounded()
    {
        if (Physics2D.CircleCast(transform.position, groundRadiusCircle, -Vector2.up, maxDistanceCircle, groundLayer))
            return true;
        else
            return false;
    }

    void FixedUpdate()
    {
        playerRb.velocity = new Vector2(input * speed, playerRb.velocity.y);
    }

    private Vector3 center;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        center.y = -maxDistanceCircle;
        Gizmos.DrawWireSphere(transform.position + center, groundRadiusCircle);
    }
}

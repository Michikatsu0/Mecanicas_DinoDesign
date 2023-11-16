using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public List<AnimatorController> animatorControllers;
    public List<GameObject> dinoColliders = new List<GameObject>();
    public List<Transform> dinoEffectTransform = new List<Transform>();
    public List<ParticleSystem> particleSystems = new List<ParticleSystem>();
    public List<AudioClip> audioClips = new List<AudioClip>();
    public LayerMask groundLayer;

    private Rigidbody2D playerRb;
    private Animator animator;
    
    private AudioSource audioSource;
    
    public float speed, jumpForce, groundRadiusCircle, maxDistanceCircle;
    private float input;
    private int currentDinoIndex;
    private bool canJump = true, flag = true, deadScript;

    private int HCMove = Animator.StringToHash("Move");
    private int HCGrounded = Animator.StringToHash("IsGrounded");
    private int HCJump = Animator.StringToHash("Jump");
    private int HCHurt = Animator.StringToHash("Hurt");
    private int HCAttack = Animator.StringToHash("Attack");
    private int HCDead = Animator.StringToHash("Dead");

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        ChangeDino(currentDinoIndex);
    }

    void Update()
    {
        if (deadScript) return;

        input = Input.GetAxis("Horizontal");

        if (input != 0)
        {
            if (flag)
            {
                flag = false;
                particleSystems[1].Play();
            }
        }
        else
        {
            particleSystems[1].Stop();
            flag = true;
        }

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



        animator.SetBool(HCJump, !canJump);
        animator.SetBool(HCMove, input != 0);
        animator.SetBool(HCGrounded, IsGrounded());
    }

    public void Dead()
    {
        animator.SetBool(HCDead, true);
        deadScript = true;
        StartCoroutine(DisableDead());
    }

    private IEnumerator DisableDead()
    {
        yield return new WaitForSeconds(0.1f);
        animator.SetBool(HCDead, false);
    }

    public void Attack()
    {
        animator.SetBool(HCAttack, true);
        StartCoroutine(DisableAttack());
    }

    private IEnumerator DisableAttack()
    {
        yield return new WaitForSeconds(0.25f);
        animator.SetBool(HCAttack, false);
    }

    public void ChangeDino(int evolutionVar)
    {
        foreach (var colliders in dinoColliders)
            colliders.SetActive(false);

        animator.SetBool(HCAttack, true);
        currentDinoIndex -= evolutionVar;
        currentDinoIndex = Mathf.Clamp(currentDinoIndex, 0, animatorControllers.Count - 1);
        PlayAudioChange(currentDinoIndex);
        dinoColliders[currentDinoIndex].SetActive(true);
        animator.runtimeAnimatorController = animatorControllers[currentDinoIndex];
        animator.SetBool(HCAttack, false);

        StartCoroutine(ParticleChange());
    }

    private IEnumerator ParticleChange()
    {
        yield return new WaitForSeconds(0.25f);
        particleSystems[0].Play();
    }

    private bool IsGrounded()
    {
        if (Physics2D.CircleCast(transform.position, groundRadiusCircle, -Vector2.up, maxDistanceCircle, groundLayer))
            return true;
        else
            return false;
    }

    private void PlayAudioChange(int evolutionVar)
    {
        if (evolutionVar == 0)
            audioSource.PlayOneShot(audioClips[0]);
        else if (evolutionVar == 1)
            audioSource.PlayOneShot(audioClips[1]);
        else if (evolutionVar == 2)
            audioSource.PlayOneShot(audioClips[2]);
    }

    void FixedUpdate()
    {
        if (deadScript) return;

        playerRb.velocity = new Vector2(input * speed, playerRb.velocity.y);
    }

    private Vector3 center;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        center.y = -maxDistanceCircle;
        Gizmos.DrawWireSphere(transform.position + center, groundRadiusCircle);
    }
}

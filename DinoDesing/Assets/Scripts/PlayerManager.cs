using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public List<RuntimeAnimatorController> animatorControllers = new List<RuntimeAnimatorController>();
    public List<GameObject> dinoColliders = new List<GameObject>();
    public List<Transform> dinoEffectTransform = new List<Transform>();
    public List<ParticleSystem> particleSystems = new List<ParticleSystem>();
    public List<AudioClip> audioClips = new List<AudioClip>();
    public LayerMask groundLayer;

    private Rigidbody2D playerRgbd2D;
    private Animator animator;
    
    private AudioSource audioSource;
    
    public float speed, jumpForce, groundRadiusCircle, maxDistanceCircle;
    private float inputX, inputY;
    private int currentDinoIndex;
    private bool canJump = true, flag = true, isClimbing;
    public bool deadScript;
    private int HCMove = Animator.StringToHash("Move");
    private int HCGrounded = Animator.StringToHash("IsGrounded");
    private int HCJump = Animator.StringToHash("Jump");
    private int HCHurt = Animator.StringToHash("Hurt");
    private int HCAttack = Animator.StringToHash("Attack");
    private int HCDead = Animator.StringToHash("Dead");

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerRgbd2D = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        ChangeDino(currentDinoIndex);
    }

    void Update()
    {
        if (deadScript)
        {
            particleSystems[1].Stop();
            return;
        }

        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");

        if (inputX != 0)
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

        if (inputX < 0)
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        else if (inputX > 0)
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);

        if (canJump && Input.GetButton("Jump") && IsGrounded())
        {
            canJump = false;
            playerRgbd2D.velocity = Vector2.up * jumpForce;
        }

        if (Input.GetButtonUp("Jump"))
            canJump = true;

        animator.SetBool(HCJump, !canJump);
        animator.SetBool(HCMove, inputX != 0);
        animator.SetBool(HCGrounded, IsGrounded());
    }

    public void EnterClimb()
    {
        isClimbing = true;
        playerRgbd2D.gravityScale = 0;
    }


    public void OutClimb()
    {
        isClimbing = false;
        playerRgbd2D.gravityScale = 9f;
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
        particleSystems[1].transform.position = dinoEffectTransform[currentDinoIndex].position;
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

        playerRgbd2D.velocity = isClimbing ? new Vector2(inputX * speed, inputY * speed) : new Vector2(inputX * speed, playerRgbd2D.velocity.y);
    }

    private Vector3 center;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        center.y = -maxDistanceCircle;
        Gizmos.DrawWireSphere(transform.position + center, groundRadiusCircle);
    }
}

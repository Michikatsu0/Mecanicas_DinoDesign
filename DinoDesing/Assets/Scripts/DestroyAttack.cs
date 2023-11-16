using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAttack : MonoBehaviour
{
    private bool flag = true;
    public GameObject panelDead;
    public ParticleSystem ps;
    private AudioClip audioClip;
    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Collider2D col2D;
    public bool deadDamage;
    private int HCDead = Animator.StringToHash("Dead");

    private void Start()
    {
        col2D = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject target = collision.gameObject;

        var player = target.GetComponentInParent<PlayerManager>();

        if (player && flag)
        {
            flag = false;
            if (player.dinoColliders[0].activeSelf)
            {
                player.Attack();
                ps.Play();
                audioSource.PlayOneShot(audioClip);
                animator?.SetBool(HCDead, true);
                col2D.isTrigger = true;
                StartCoroutine(DeadEnemy());
            }
            else
            {
                if (deadDamage)
                {
                    player.Dead();
                    panelDead.SetActive(true);
                }
            }
        }
    }

    private IEnumerator DeadEnemy()
    {
        yield return new WaitForSeconds(0.5f);
        animator?.SetBool(HCDead, false);
        yield return new WaitForSeconds(1f);
        spriteRenderer.enabled = false;
        Destroy(this);
    }
}

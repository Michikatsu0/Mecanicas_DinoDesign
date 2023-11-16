using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PickUpsBase : MonoBehaviour
{
    public int evolutionVar;
    public AudioClip audioClip;
    private SpriteRenderer spriteRenderer;
    private bool flag = true;
    public Collider2D collider;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();  
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject target = collision.gameObject;

        var player = target.GetComponentInParent<PlayerManager>();
        
        if (player && flag)
        {
            flag = false;
            player.ChangeDino(evolutionVar);
            collider.isTrigger = true;
            spriteRenderer.enabled = false;
            Destroy(gameObject, 1f);
        }
    }
}

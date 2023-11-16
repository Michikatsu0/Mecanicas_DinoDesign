using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PickUpsBase : MonoBehaviour
{
    public int evolutionVar;
    private SpriteRenderer spriteRenderer;
    private bool flag = true;
    public Collider2D colRgbd;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject target = collision.gameObject;

        var player = target.GetComponentInParent<PlayerManager>();

        if (player && flag)
        {
            flag = false;
            player.Attack();
            StartCoroutine(DinoChange(player));
        }
    }

    private IEnumerator DinoChange(PlayerManager player)
    {
        yield return new WaitForSeconds(0.5f);
        player.ChangeDino(evolutionVar);
        colRgbd.isTrigger = true;
        spriteRenderer.enabled = false;
        Destroy(gameObject, 1f);
    }
}

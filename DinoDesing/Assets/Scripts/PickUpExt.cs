using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpExt : MonoBehaviour
{
    private bool flag = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject target = collision.gameObject;

        var player = target.GetComponentInParent<PlayerManager>();

        if (player && flag)
        {
            flag = false;
            player.Attack();
        }
    }
}

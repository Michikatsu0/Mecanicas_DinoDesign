using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vine : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        GameObject target = other.gameObject;

        var player = target.GetComponentInParent<PlayerManager>();

        if (player)
        {
            if (player.dinoColliders[1].activeSelf)
                player.EnterClimb();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject target = collision.gameObject;

        var player = target.GetComponentInParent<PlayerManager>();

        if (player)
            player.OutClimb();
    }
}
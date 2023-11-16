using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NubeDeadNuke : MonoBehaviour
{
    public float explosionForce = 5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject target = collision.gameObject;

        var player = target.GetComponentInParent<PlayerManager>();
        var rgbd = target.GetComponentInParent<Rigidbody2D>();

        if (player && rgbd)
        {
            if (player.transform.rotation.y == 0)
                rgbd.AddForce(Vector2.right * explosionForce, ForceMode2D.Force);
            else
                rgbd.AddForce(-Vector2.right * explosionForce, ForceMode2D.Force);

            rgbd.constraints = RigidbodyConstraints2D.None;
            player.Dead();
        }
    }

}
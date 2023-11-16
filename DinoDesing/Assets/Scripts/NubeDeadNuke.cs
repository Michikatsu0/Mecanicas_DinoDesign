using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NubeDeadNuke : MonoBehaviour
{
    public float nubeSpeed = 3f;
    public GameObject panelDead;

    private void Start()
    {
        panelDead.SetActive(false);
    }

    private void Update()
    {
        transform.Translate(Vector2.right * nubeSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject target = collision.gameObject;

        var player = target.GetComponentInParent<PlayerManager>();

        if (player)
        {
            player.Dead();
            panelDead.SetActive(true);
        }
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NubeDeadNuke : MonoBehaviour
{
    public float nubeSpeed = 3f;
    public GameObject panelDead;
    public bool deadScript;
    private Collider2D col2d;
    private void Start()
    {
        panelDead.SetActive(false);
        col2d = GetComponent<Collider2D>();
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
            if (deadScript) return;
            player.Dead();
            panelDead.SetActive(true);
        }
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Escalar : MonoBehaviour
{
    private Rigidbody2D rb2d;

    private Vector2 input;

    private Animator animator;

    [Header("Escalar")]
    [SerializeField] private float velocidadEscalar;

    private BoxCollider2D boxcolider2d;

    private float gravedad;
    private bool escalando;
  
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxcolider2d = GetComponent<BoxCollider2D>();
        gravedad = rb2d.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        Escalarr();
    }


    private void Escalarr()
    {
        if((input.y !=0 || escalando ) && (boxcolider2d.IsTouchingLayers(LayerMask.GetMask("Escaleras"))))
        {
            Vector2 velocidadSubida = new Vector2(rb2d.velocity.x, input.y * velocidadEscalar);
            rb2d.velocity = velocidadSubida;
            rb2d.gravityScale =0;
            escalando = true;
        }
        else 
        {
            rb2d.gravityScale = gravedad;
            escalando = false;
        }
//        if (enSuelo){
//            escalando = false;
//        }
    }
}

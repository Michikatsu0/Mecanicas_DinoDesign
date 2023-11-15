using UnityEngine;
using UnityEngine.UI;

public class mapa : MonoBehaviour
{
   public Transform jugador; // Referencia al objeto del jugador
    public Transform otroObjeto; // Referencia al otro objeto que se mueve

    public Transform inicio; // Posición inicial
    public Transform meta; // Posición de la meta

    public Transform spriteJugador; // Sprite para representar al jugador
    public Transform spriteOtroObjeto; // Sprite para representar al otro objeto

    void Update()
    {
        float distanciaTotal = Vector3.Distance(inicio.position, meta.position);
        float distanciaJugador = Vector3.Distance(inicio.position, jugador.position);
        float distanciaOtroObjeto = Vector3.Distance(inicio.position, otroObjeto.position);

        float porcentajeJugador = distanciaJugador / distanciaTotal;
        float porcentajeOtroObjeto = distanciaOtroObjeto / distanciaTotal;

        float posXJugador = Mathf.Lerp(0f, meta.position.x - inicio.position.x, porcentajeJugador);
        float posXOtroObjeto = Mathf.Lerp(0f, meta.position.x - inicio.position.x, porcentajeOtroObjeto);

        spriteJugador.position = new Vector3(inicio.position.x + posXJugador, spriteJugador.position.y, spriteJugador.position.z);
        spriteOtroObjeto.position = new Vector3(inicio.position.x + posXOtroObjeto, spriteOtroObjeto.position.y, spriteOtroObjeto.position.z);
    }
}
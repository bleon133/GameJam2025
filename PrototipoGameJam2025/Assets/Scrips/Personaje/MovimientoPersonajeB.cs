using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPersonajeB : MonoBehaviour
{
    [Header("Parámetros de Movimiento")]
    [SerializeField] private float moveSpeed = 5f; // Velocidad de movimiento

    private Rigidbody2D rb2D;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Flip(true);
    }

    private void FixedUpdate()
    {
        // Variable que contendrá la dirección de movimiento
        float movement = 0f;

        // Si se mantiene presionada la flecha Izquierda
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            movement = -1f;
            Flip(true);
        }
        // Si se mantiene presionada la flecha Derecha
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            movement = 1f;
            Flip(false);
        }

        // Aplicamos la velocidad al Rigidbody2D
        rb2D.velocity = new Vector2(movement * moveSpeed, rb2D.velocity.y);
    }

    private void Flip(bool isFacingLeft)
    {
        Vector3 localScale = transform.localScale;
        if (isFacingLeft && localScale.x > 0)
        {
            localScale.x *= -1;
            transform.localScale = localScale;
        }
        else if (!isFacingLeft && localScale.x < 0)
        {
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }
}

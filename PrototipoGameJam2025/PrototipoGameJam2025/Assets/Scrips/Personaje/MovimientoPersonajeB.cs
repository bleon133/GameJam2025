using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPersonajeB : MonoBehaviour
{
    [Header("Parámetros de Movimiento")]
    [SerializeField] private float moveSpeed = 5f; // Velocidad de movimiento

    private Rigidbody2D rb2D;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // Variable que contendrá la dirección de movimiento
        float movement = 0f;

        // Si se mantiene presionada la flecha Izquierda
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            movement = -1f; 
        }
        // Si se mantiene presionada la flecha Derecha
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            movement = 1f; 
        }

        // Aplicamos la velocidad al Rigidbody2D
        rb2D.velocity = new Vector2(movement * moveSpeed, rb2D.velocity.y);
    }
}

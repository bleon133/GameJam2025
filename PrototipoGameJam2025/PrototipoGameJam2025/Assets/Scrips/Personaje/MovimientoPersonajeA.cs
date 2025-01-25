using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovimientoPersonaje : MonoBehaviour
{
    [Header("Parámetros de Movimiento")]
    [SerializeField] private float moveSpeed = 5f; // Velocidad de movimiento

    private Rigidbody2D _rb2D;

    private void Awake()
    {
        _rb2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float movement = 0f;

        // Si se mantiene presionada la tecla A
        if (Input.GetKey(KeyCode.A))
        {
            movement = -1f; 
        }
        // Si se mantiene presionada la tecla D
        else if (Input.GetKey(KeyCode.D))
        {
            movement = 1f; 
        }

        // Aplicamos la velocidad al Rigidbody2D
        _rb2D.velocity = new Vector2(movement * moveSpeed, _rb2D.velocity.y);
    }
}

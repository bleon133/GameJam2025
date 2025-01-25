using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPersonajeB : MonoBehaviour
{
    [Header("Parámetros de Movimiento")]
    [SerializeField] private float moveSpeed = 5f; // Velocidad de movimiento

    private Rigidbody2D _rb2D;

    private void Awake()
    {
        // Obtenemos el componente Rigidbody2D
        _rb2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // Variable que contendrá la dirección de movimiento
        float movement = 0f;

        // Si se mantiene presionada la flecha Izquierda
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            movement = -1f; // Mover hacia la izquierda
        }
        // Si se mantiene presionada la flecha Derecha
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            movement = 1f; // Mover hacia la derecha
        }

        // Aplicamos la velocidad al Rigidbody2D
        _rb2D.velocity = new Vector2(movement * moveSpeed, _rb2D.velocity.y);
    }
}

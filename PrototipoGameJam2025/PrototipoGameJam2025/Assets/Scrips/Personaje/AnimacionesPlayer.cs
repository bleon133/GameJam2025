using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacionesPlayer : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb2D;

    // Umbral para considerar el movimiento en una dirección
    private float movementThreshold = 0.1f;

    private void Awake()
    {
        // Obtiene los componentes necesarios
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2D = GetComponent<Rigidbody2D>();

        if (spriteRenderer == null)
        {
            Debug.LogError("FlipCharacter requiere un componente SpriteRenderer.");
        }

        if (rb2D == null)
        {
            Debug.LogError("FlipCharacter requiere un componente Rigidbody2D.");
        }
    }

    private void Update()
    {
        // Asegura que los componentes estén asignados
        if (spriteRenderer == null || rb2D == null) return;

        float velocityX = rb2D.velocity.x;

        // Verifica la dirección del movimiento y voltea el sprite en consecuencia
        if (velocityX > movementThreshold)
        {
            spriteRenderer.flipX = false; // Mirar a la derecha
        }
        else if (velocityX < -movementThreshold)
        {
            spriteRenderer.flipX = true; // Mirar a la izquierda
        }
    }
}

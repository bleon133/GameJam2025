using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rbPlayer;

    [SerializeField] private float newGravityScale = 5f; //Nueva gravedad
    [SerializeField] private float newMass = 2f; //Nueva masa

    // Almacena la velocidad cuando se pausa
    private Vector2 velocidadGuardada;

    // Para cuando el globo del player se destruye.
    public void DropFaster()
    {
        rbPlayer.mass = newMass;
        rbPlayer.gravityScale = newGravityScale;
    }

    public void PausarMovimiento()
    {
        // Guardamos la velocidad en el momento de la pausa
        velocidadGuardada = rbPlayer.velocity;
        // Desactivamos el Rigidbody2D para que no le afecte la física
        rbPlayer.simulated = false;
    }

    public void ReanuarMovimiento()
    {
        // Reactivamos la simulación
        rbPlayer.simulated = true;
        // Restauramos la velocidad que tenía antes de pausar
        rbPlayer.velocity = velocidadGuardada;
    }
}

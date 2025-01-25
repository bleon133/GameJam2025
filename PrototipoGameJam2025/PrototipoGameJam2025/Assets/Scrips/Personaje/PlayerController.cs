using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Referencia al Rigidbody2D principal del Player
    [SerializeField] private Rigidbody2D rbPlayer;

    // Ajustes para "caer rápido"
    [SerializeField] private float newGravityScale = 5f;
    [SerializeField] private float newMass = 2f;

    // Este método se llama cuando el globo del player se destruye.
    public void DropFaster()
    {
        // Aumenta la gravedad y/o la masa
        rbPlayer.mass = newMass;
        rbPlayer.gravityScale = newGravityScale;
    }

}

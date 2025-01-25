using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rbPlayer;

    [SerializeField] private float newGravityScale = 5f; //Nueva gravedad
    [SerializeField] private float newMass = 2f; //Nueva masa

    // Para cuando el globo del player se destruye.
    public void DropFaster()
    {
        rbPlayer.mass = newMass;
        rbPlayer.gravityScale = newGravityScale;
    }

}
